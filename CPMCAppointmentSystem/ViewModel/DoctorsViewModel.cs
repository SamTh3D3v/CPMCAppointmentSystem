using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using CPMCAppointmentSystem.Helpers;
using CPMCAppointmentSystem.SubModel;
using CPMCAppointmentSystem.View;
using CPMCAppointmentSystem.View.DoctorsViews;
using DataLayer;
using DataLayer.Model;
using GalaSoft.MvvmLight.Command;
using Syncfusion.Data.Extensions;
using Syncfusion.Windows.Forms.Tools;

namespace CPMCAppointmentSystem.ViewModel
{
    public class DoctorsViewModel : NavigableViewModelBase
    {
        #region Fields
        private ObservableCollection<EntityToAdd<Pathology>> _pathologiesToDoctorListAdds;
        private ObservableCollection<EntityToAdd<Specialite>> _specialityToDoctorList;
        private CpmcContext _dbContext = new CpmcContext();
        private AddSpecialitiesToDoctorView _addSpecialitiesToDoctorView;
        private AddPathologiesToDoctorView _addPathologiesToDoctorView;
        private AddRdvFromDoctorsView _addPatientsToDoctorView;
        private ObservableCollection<Medecin> _doctorsList;
        private Medecin _seletedDoctor;
        private ObservableCollection<Specialite> _specialitiesList;
        private Pathology _selectedPathologyInDoctorsViewPathology;
        private bool _isFormEnabled;
        private Patient _selectedPatientInDoctorView;
        private ObservableCollection<Patient> _patientsList;                
        private RendezVous _selectedAppointement  ; 
        #endregion
        #region Properties
        public RendezVous SelectedAppointement
        {
            get
            {
                return _selectedAppointement;
            }

            set
            {
                if (_selectedAppointement == value)
                {
                    return;
                }

                _selectedAppointement = value;
                RaisePropertyChanged();
            }
        }
        
        public ObservableCollection<EntityToAdd<Pathology>> PathologiesToDoctorList
        {
            get
            {
                return _pathologiesToDoctorListAdds;
            }

            set
            {
                if (_pathologiesToDoctorListAdds == value)
                {
                    return;
                }

                _pathologiesToDoctorListAdds = value;
                RaisePropertyChanged();
            }
        }
        public ObservableCollection<EntityToAdd<Specialite>> SpecialitiesToDoctorList
        {
            get
            {
                return _specialityToDoctorList;
            }

            set
            {
                if (_specialityToDoctorList == value)
                {
                    return;
                }

                _specialityToDoctorList = value;
                RaisePropertyChanged();
            }
        }
        public ObservableCollection<Medecin> DoctorsList
        {
            get
            {
                return _doctorsList;
            }

            set
            {
                if (_doctorsList == value)
                {
                    return;
                }

                _doctorsList = value;
                RaisePropertyChanged();
            }
        }
        public Medecin SelectedDoctor
        {
            get
            {
                return _seletedDoctor;
            }

            set
            {
                if (_seletedDoctor == value)
                {
                    return;
                }
                IsFormEnabled = value != null;
                _seletedDoctor = value;
                RaisePropertyChanged();
            }
        }
        public Pathology SelectedPathologyInDoctorsView
        {
            get
            {
                return _selectedPathologyInDoctorsViewPathology;
            }

            set
            {
                if (_selectedPathologyInDoctorsViewPathology == value)
                {
                    return;
                }

                _selectedPathologyInDoctorsViewPathology = value;
                RaisePropertyChanged();
            }
        }
        public ObservableCollection<Specialite> SpecialitiesList
        {
            get
            {
                return _specialitiesList;
            }

            set
            {
                if (_specialitiesList == value)
                {
                    return;
                }

                _specialitiesList = value;
                RaisePropertyChanged();
            }
        }
        public bool IsFormEnabled
        {
            get
            {
                return _isFormEnabled;
            }

            set
            {
                if (_isFormEnabled == value)
                {
                    return;
                }

                _isFormEnabled = value;
                RaisePropertyChanged();
            }
        }
        public Patient SelectedPatientInDoctorView
        {
            get
            {
                return _selectedPatientInDoctorView;
            }

            set
            {
                if (_selectedPatientInDoctorView == value)
                {
                    return;
                }

                _selectedPatientInDoctorView = value;
                RaisePropertyChanged();
            }
        }
        public ObservableCollection<Patient> PatientsList
        {
            get
            {
                return _patientsList;
            }

            set
            {
                if (_patientsList == value)
                {
                    return;
                }

                _patientsList = value;
                RaisePropertyChanged();
            }
        }     
        #endregion
        #region Commands
        private RelayCommand _cancelAppointementChangesCommand;
        public RelayCommand CancelAppointementChangesCommand
        {
            get
            {
                return _cancelAppointementChangesCommand
                    ?? (_cancelAppointementChangesCommand = new RelayCommand(
                    () =>
                    {
                        _addPatientsToDoctorView.Close();
                    }));
            }
        }
        private RelayCommand _deleteAppointementCommand;
        public RelayCommand DeleteAppointementCommand
        {
            get
            {
                return _deleteAppointementCommand
                    ?? (_deleteAppointementCommand = new RelayCommand(async () =>
                    {
                        //todo Logical suppression 
                        if (SelectedAppointement != null)
                        {
                            if (SelectedAppointement.RendezVousId != Guid.Empty)
                            {
                                _dbContext.RendezVouses.Remove(SelectedAppointement);
                                _dbContext.SaveChanges();
                                SelectedAppointement = null;
                                _addPatientsToDoctorView.Close();                                
                            }
                        }

                    }));
            }
        }
        private RelayCommand _appointementDoubleClickCommand;
        public RelayCommand AppointementDoubleClickCommand
        {
            get
            {
                return _appointementDoubleClickCommand
                    ?? (_appointementDoubleClickCommand = new RelayCommand(
                    () =>
                    {
                        _addPatientsToDoctorView = new AddRdvFromDoctorsView();
                        _addPatientsToDoctorView.ShowDialog();
                    }));
            }
        }
        private RelayCommand _saveAppointementCommand;
        public RelayCommand SaveAppointementCommand
        {
            get
            {
                return _saveAppointementCommand
                    ?? (_saveAppointementCommand = new RelayCommand(async () =>
                    {

                        if (SelectedAppointement.RendezVousId == Guid.Empty)
                        {
                            AddNewAppointement();
                            NotficationManager.AddNotification(new Notification()
                            {
                                NotificationId = Guid.NewGuid(),
                                NotificationTitle = "New",
                                NotificationMessage = "Rendez vous du patient  " + SelectedAppointement.Patient.Nom + " " + SelectedAppointement.Patient.Prenom,
                                NotificationType = TypeNotification.Information
                            });
                        }
                        else
                        {
                            NotficationManager.AddNotification(new Notification()
                            {
                                NotificationId = Guid.NewGuid(),
                                NotificationTitle = "Update",
                                NotificationMessage = "Rendez vous du patient  " + SelectedAppointement.Patient.Nom + " " + SelectedAppointement.Patient.Prenom,
                                NotificationType = TypeNotification.Information
                            });
                        }
                        _dbContext.SaveChanges();
                        _addPatientsToDoctorView.Close();                       
                        SelectedAppointement = null;

                    }));
            }
        }

        private void AddNewAppointement()
        {
            //Added by Farouk for Audit purpose
            SelectedAppointement.RendezVousId = Guid.NewGuid();
            _dbContext.RendezVouses.Add(SelectedAppointement);
        }
        //private async Task LoadPatientAppointementList()
        //{
        //    SelectedDoctor.RendezVouses = new ObservableCollection<RendezVous>(await Task.Run(() =>
        //        _dbContext.RendezVouses.Where(x => x.D == SelectedPatient.PatientId)
        //    ));
        //}
        private RelayCommand _savePathologyWhithDoctorsCommand;
        public RelayCommand SavePathologyWhithDoctorsCommand
        {
            get
            {
                return _savePathologyWhithDoctorsCommand
                    ?? (_savePathologyWhithDoctorsCommand = new RelayCommand(async () =>
                    {
                        await SavePathologiesAddedToDoctor();
                        _dbContext.SaveChanges();
                        _addPathologiesToDoctorView.Close();

                    }));
            }
        }
        private async Task SavePathologiesAddedToDoctor()
        {
            await Task.Run(() =>
            {
                if (SelectedDoctor.Pathologies == null)
                    SelectedDoctor.Pathologies = new ObservableCollection<Pathology>();
                PathologiesToDoctorList.ForEach(pToAdd =>
                {
                    if (pToAdd.IsAdded)
                    {
                        if (SelectedDoctor.Pathologies.All(p => p.PathologyId != pToAdd.Entity.PathologyId))
                        {
                            SelectedDoctor.Pathologies.Add(_dbContext.Pathologies.Find(pToAdd.Entity.PathologyId));
                        }
                    }
                    else
                    {
                        if (SelectedDoctor.Pathologies.Any(pp => pp.PathologyId == pToAdd.Entity.PathologyId))
                        {
                            SelectedDoctor.Pathologies.Remove(_dbContext.Pathologies.Find(pToAdd.Entity.PathologyId));
                        }
                    }
                });

            });
        }
        //private RelayCommand _savePatientsToDoctorsViewCommand;
        //public RelayCommand SavePatientsToDoctorsViewCommand
        //{
        //    get
        //    {
        //        return _savePatientsToDoctorsViewCommand
        //            ?? (_savePatientsToDoctorsViewCommand = new RelayCommand(async () =>
        //            {                        
        //                await SavePatientsAddedToDoctor();
        //                _dbContext.SaveChanges();
        //                _addPatientsToDoctorView.Close();

        //            }));
        //    }
        //}

        //private async Task SavePatientsAddedToDoctor()
        //{
        //    await Task.Run(() =>
        //    {
        //        if (SelectedDoctor.Patients == null)
        //            SelectedDoctor.Patients = new ObservableCollection<Patient>();
        //        PatientsList.ForEach(pToAdd =>
        //        {
        //            if (pToAdd.IsAdded)
        //            {
        //                if (SelectedDoctor.Patients.All(p => p.PatientId != pToAdd.Entity.PatientId))
        //                {
        //                    SelectedDoctor.Patients.Add(_dbContext.Patients.Find(pToAdd.Entity.PatientId));
        //                }
        //            }
        //            else
        //            {
        //                if (SelectedDoctor.Patients.Any(p => p.PatientId == pToAdd.Entity.PatientId))
        //                {
        //                    SelectedDoctor.Patients.Remove(_dbContext.Patients.Find(pToAdd.Entity.PatientId));
        //                }
        //            }
        //        });
        //    });
        //}

        private RelayCommand _saveSpecialityWithDoctorsCommand;    
        public RelayCommand SaveSpecialityWithDoctorsCommand
        {
            get
            {
                return _saveSpecialityWithDoctorsCommand
                    ?? (_saveSpecialityWithDoctorsCommand = new RelayCommand(async () =>
                    {
                         await SaveSpecialitiesAddedToDoctor();
                        _dbContext.SaveChanges();
                        _addSpecialitiesToDoctorView.Close();
                    }));
            }
        }


        private async Task SaveSpecialitiesAddedToDoctor()
        {
            await Task.Run(() =>
            {
                if (SelectedDoctor.Specialities == null)
                    SelectedDoctor.Specialities = new ObservableCollection<Specialite>();
                SpecialitiesToDoctorList.ForEach(sToAdd =>
                {
                    if (sToAdd.IsAdded)
                    {
                        if (SelectedDoctor.Specialities.All(s => s.SpecialiteId != sToAdd.Entity.SpecialiteId))
                        {
                            SelectedDoctor.Specialities.Add(_dbContext.Specialites.Find(sToAdd.Entity.SpecialiteId));
                        }
                    }
                    else
                    {
                        if (SelectedDoctor.Specialities.Any(s => s.SpecialiteId == sToAdd.Entity.SpecialiteId))
                        {
                            SelectedDoctor.Specialities.Remove(_dbContext.Specialites.Find(sToAdd.Entity.SpecialiteId));
                        }
                    }
                });

            });
        }
       
        private RelayCommand _cancelPathologyWhithDoctorsCommand;
        public RelayCommand CancelPathologyWhithDoctorsCommand
        {
            get
            {
                return _cancelPathologyWhithDoctorsCommand
                    ?? (_cancelPathologyWhithDoctorsCommand = new RelayCommand(async () =>
                    {
                        _addPathologiesToDoctorView.Close();
                        await LoadDoctorsPathologies();
                    }));
            }
        }
        private RelayCommand _cancelSpecialityWhithDoctorsCommand;
        public RelayCommand CancelSpecialityWhithDoctorsCommand
        {
            get
            {
                return _cancelSpecialityWhithDoctorsCommand
                    ?? (_cancelSpecialityWhithDoctorsCommand = new RelayCommand(async () =>
                    {
                        _addSpecialitiesToDoctorView.Close();
                        await LoadDoctorsSpecialities();
                        
                    }));
            }
        }

        private RelayCommand _addDoctorToPathologyLoadedCommand;
        public RelayCommand AddPathologiesToDoctorLoadedCommand
        {
            get
            {
                return _addDoctorToPathologyLoadedCommand
                    ?? (_addDoctorToPathologyLoadedCommand = new RelayCommand(async () =>
                    {
                        await LoadDoctorsPathologies();
                    }));
            }
        }

        private async Task LoadDoctorsPathologies()
        {
            PathologiesToDoctorList = new ObservableCollection<EntityToAdd<Pathology>>(await Task.Run(() => _dbContext.Pathologies.Select(p => new EntityToAdd<Pathology>()
            {               
                Entity = p
                //IsAdded = SelectedDoctor.Pathologies.Any(dp=>p.PathologyId==dp.PathologyId)       //throw [Only primitive types or enumeration types are supported in this context] exception     

            })));
            foreach (var pathToAdd in PathologiesToDoctorList)
            {
                pathToAdd.IsAdded = SelectedDoctor.Pathologies.Any(dp => pathToAdd.Entity.PathologyId == dp.PathologyId);
            }
        }
        private RelayCommand _addPatientsToDoctorLoadedCommand;
        public RelayCommand AddPatientsToDoctorLoadedCommand
        {
            get
            {
                return _addPatientsToDoctorLoadedCommand
                    ?? (_addPatientsToDoctorLoadedCommand = new RelayCommand(async () =>
                    {
                       await LoadPatientsToAddList();
                    }));
            }
        }

        private async Task LoadPatientsToAddList()
        {
            PatientsList = new ObservableCollection<Patient>(await Task.Run(() => _dbContext.Patients));                    
        }
        private RelayCommand _addSpecialitiesToDoctorCommand;
        public RelayCommand AddSpecialitiesToDoctorLoadedCommand
        {
            get
            {
                return _addSpecialitiesToDoctorCommand
                    ?? (_addSpecialitiesToDoctorCommand = new RelayCommand(async () =>
                    {
                        await LoadDoctorsSpecialities();                       
                    }));
            }
        }

        private async Task LoadDoctorsSpecialities()
        {
            SpecialitiesToDoctorList = new ObservableCollection<EntityToAdd<Specialite>>(await Task.Run(() => _dbContext.Specialites.Select(s => new EntityToAdd<Specialite>()
            {
                Entity = s                
            })));
            foreach (var speToAdd in SpecialitiesToDoctorList)
            {
                speToAdd.IsAdded = SelectedDoctor.Specialities.Any(ds => speToAdd.Entity.SpecialiteId == ds.SpecialiteId);
            }
        }

        private RelayCommand _doctorsViewLoadedCommand;
        public RelayCommand DoctorsViewLoadedCommand
        {
            get
            {
                return _doctorsViewLoadedCommand
                    ?? (_doctorsViewLoadedCommand = new RelayCommand(async () =>
                    {
                        _dbContext=new CpmcContext();
                        await LoadDoctorsList();
                        await LoadSpacialities();
                    }));
            }
        }
        private RelayCommand _doctorsViewUnLoadedCommand;
        public RelayCommand DoctorsViewUnLoadedCommand
        {
            get
            {
                return _doctorsViewUnLoadedCommand
                    ?? (_doctorsViewUnLoadedCommand = new RelayCommand(
                    () =>
                    {
                        _dbContext.Dispose();
                        
                    }));
            }
        }
        private RelayCommand _addDoctorCommand;
        public RelayCommand AddDoctorCommand
        {
            get
            {
                return _addDoctorCommand
                    ?? (_addDoctorCommand = new RelayCommand(
                    () =>
                    {
                        SelectedDoctor = new Medecin()
                        {
                            User = new User()
                            {
                                 //Added by Farouk for Audit purpose
                                UserId = Guid.NewGuid(),

                                RolesCollection = new RolesCollection()
                                {
                                    //get the default medecin rolls from the xml settings file
                                },
                                UserTypeId = _dbContext.UserTypes.First(x => x.UserTypeName == "Medecin").UserTypeId
                            },
                            Patients = new ObservableCollection<Patient>(),
                            Pathologies = new ObservableCollection<Pathology>(),
                            Specialities = new ObservableCollection<Specialite>()
                        };
                    }));
            }
        }
        private RelayCommand<object> _saveDoctorCommand;
        public RelayCommand<object> SaveDoctorCommand
        {
            get
            {
                return _saveDoctorCommand
                    ?? (_saveDoctorCommand = new RelayCommand<object>(async (obj) =>
                    {
                        var passwordBox = obj as PasswordBox;
                        if (passwordBox != null)
                        {
                            SelectedDoctor.User.UserPass = passwordBox.Password;  //to be hashed
                        }
                        if (SelectedDoctor.MedecinId == Guid.Empty)
                        {
                            await AddNewDoctor();
                            _dbContext.Notifications.Add(new Notification()
                            {
                                NotificationId = Guid.NewGuid(),
                                NotificationTitle = "Nouveau medecin",
                                NotificationMessage = "Le medecin :" + SelectedDoctor.User.UserNom+" "+SelectedDoctor.User.UserPrenom + " a été inserer",
                                NotificationType = TypeNotification.Information,
                                IsActive = true,
                                TypeUser = TypeUserUtility.WhichTypeUser(true, false, true),
                                CreatedOn = DateTime.Now,
                                ModifiedOn = DateTime.Now
                            });
                        }
                        
                            _dbContext.SaveChanges();
                        //}
                        //catch (DbEntityValidationException e)
                        //{
                        //    foreach (var eve in e.EntityValidationErrors)
                        //    {
                        //        Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        //            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        //        foreach (var ve in eve.ValidationErrors)
                        //        {
                        //            Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                        //                ve.PropertyName, ve.ErrorMessage);
                        //        }
                        //    }
                        //    throw;
                        //}
                        await LoadDoctorsList();
                        SelectedDoctor = null;

                    }));
            }
        }
        private RelayCommand _deleteDoctorCommand;
        public RelayCommand DeleteDoctorCommand
        {
            get
            {
                return _deleteDoctorCommand
                    ?? (_deleteDoctorCommand = new RelayCommand(
                    () =>
                    {
                        //todo Logical suppression 
                        if (SelectedDoctor != null)
                        {
                            if (SelectedDoctor.MedecinId != Guid.Empty)
                            {
                                _dbContext.Notifications.Add(new Notification()
                                {
                                    NotificationId = Guid.NewGuid(),
                                    NotificationTitle = "Medecin supprimer",
                                    NotificationMessage = "Le medecin :" + SelectedDoctor.User.UserNom + " " + SelectedDoctor.User.UserPrenom + " a été supprimer",
                                    NotificationType = TypeNotification.Information,
                                    IsActive = true,
                                    TypeUser = TypeUserUtility.WhichTypeUser(true, false, true),
                                    CreatedOn = DateTime.Now,
                                    ModifiedOn = DateTime.Now
                                });
                                _dbContext.Medecins.Remove(SelectedDoctor);                                                                
                                DoctorsList.Remove(SelectedDoctor);
                                _dbContext.SaveChanges();
                                SelectedDoctor = null;
                            }
                        }
                    }));
            }
        }
        private RelayCommand _cancelChangesToDoctorCommand;
        public RelayCommand CancelChangesToDoctorCommand
        {
            get
            {
                return _cancelChangesToDoctorCommand
                    ?? (_cancelChangesToDoctorCommand = new RelayCommand(
                    () =>
                    {                       
                        if (SelectedDoctor != null)
                        {
                            if (SelectedDoctor.MedecinId != Guid.Empty)
                                _dbContext.Entry(SelectedDoctor).Reload();
                        }
                        SelectedDoctor = null;

                    }));
            }
        }
        private RelayCommand _addPatientsToSelectedDoctorCommand;
        public RelayCommand AddPatientsToSelectedDoctorCommand
        {
            get
            {
                return _addPatientsToSelectedDoctorCommand
                    ?? (_addPatientsToSelectedDoctorCommand = new RelayCommand(async () =>
                    {
                        if (SelectedDoctor.MedecinId == Guid.Empty)
                        {
                            AddNewDoctor();
                        }
                        SelectedAppointement = new RendezVous()
                        {
                            Medecin=SelectedDoctor
                        };
                        await LoadPatientsToAddList();
                        _addPatientsToDoctorView = new AddRdvFromDoctorsView();
                        _addPatientsToDoctorView.ShowDialog();

                    }));
            }
        }
        private RelayCommand _addSpecialitiesToSelectedDoctorCommand;
        public RelayCommand AddSpecialitiesToSelectedDoctorCommand
        {
            get
            {
                return _addSpecialitiesToSelectedDoctorCommand
                    ?? (_addSpecialitiesToSelectedDoctorCommand = new RelayCommand(
                    () =>
                    {
                        if (SelectedDoctor.MedecinId == Guid.Empty)
                        {
                            AddNewDoctor();
                        }
                        _addSpecialitiesToDoctorView = new AddSpecialitiesToDoctorView();
                        _addSpecialitiesToDoctorView.ShowDialog();

                    }));
            }
        }
        private RelayCommand _addPathologiesToSelectedDoctorCommand;
        public RelayCommand AddPathologiesToSelectedDoctorCommand
        {
            get
            {
                return _addPathologiesToSelectedDoctorCommand
                    ?? (_addPathologiesToSelectedDoctorCommand = new RelayCommand(
                    () =>
                    {

                        if (SelectedDoctor.MedecinId == Guid.Empty)
                        {
                            AddNewDoctor();
                        }
                        _addPathologiesToDoctorView = new AddPathologiesToDoctorView();
                        _addPathologiesToDoctorView.ShowDialog();

                    }));
            }
        }             
        private RelayCommand _cancelPatientsToDoctorsViewCommand;
        public RelayCommand CancelPatientsToDoctorsViewCommand
        {
            get
            {
                return _cancelPatientsToDoctorsViewCommand
                    ?? (_cancelPatientsToDoctorsViewCommand = new RelayCommand(async () =>
                    {
                        _addPatientsToDoctorView.Close();
                        await LoadPatientsToAddList();

                    }));
            }
        }
        private RelayCommand _deleteDoctorImageCommand;
        public RelayCommand DeleteDoctorImageCommand
        {
            get
            {
                return _deleteDoctorImageCommand
                    ?? (_deleteDoctorImageCommand = new RelayCommand(
                    () =>
                    {
                        SelectedDoctor.ProfilePicture = null;
                    }));
            }
        }

        private RelayCommand _loadDoctorImageCommand;
        public RelayCommand LoadDoctorImageCommand
        {
            get
            {
                return _loadDoctorImageCommand
                    ?? (_loadDoctorImageCommand = new RelayCommand(
                    () =>
                    {
                        var openFileDialog = new OpenFileDialog
                        {
                            ReadOnlyChecked = true,
                            Filter = "Image Files (*.bmp, *.png, *.jpg)|*.bmp;*.png;*.jpg"
                        };
                        var result = openFileDialog.ShowDialog();
                        if (result != DialogResult.OK) return;
                        var imagePath = openFileDialog.FileName;

                        try
                        {
                            var imageFileStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read);
                            var imageStreamReader = new BinaryReader(imageFileStream);
                            byte[] pic = imageStreamReader.ReadBytes((int)imageFileStream.Length);
                            imageStreamReader.Close();
                            imageFileStream.Close();
                            SelectedDoctor.ProfilePicture = pic;

                        }
                        catch (Exception exception)
                        {
                            MessageBox.Show(exception.Message);
                        }

                    }));
            }
        }
        #endregion
        #region Ctors and Methods
        public DoctorsViewModel(IFrameNavigationService mainFrameNavigationService, IInnerFrameNavigationService innerFrameNavigationService)
            : base(mainFrameNavigationService, innerFrameNavigationService)
        {
        }
        private async Task LoadDoctorsList()
        {
            DoctorsList = new ObservableCollection<Medecin>(await Task.Run(() => _dbContext.Medecins));
        }

        private async Task LoadSpacialities()
        {
            SpecialitiesList = new ObservableCollection<Specialite>(await Task.Run(() => _dbContext.Specialites));
        }
        private async Task AddNewDoctor()
        {
            //Added by Farouk for Audit purpose
            SelectedDoctor.MedecinId = Guid.NewGuid();

            await Task.Run(() =>
            {
                _dbContext.Medecins.Add(SelectedDoctor);
            });
        }
        #endregion
    }
}
