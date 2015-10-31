using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using DataLayer.Model;
using GalaSoft.MvvmLight.Command;
using Syncfusion.Windows.Forms.Tools;

namespace CPMCAppointmentSystem.ViewModel
{
    public class DoctorsViewModel : NavigableViewModelBase
    {
        #region Fields
        private readonly CpmcContext _dbContext = new CpmcContext();
        private AddSpecialitiesToDoctorView _addSpecialitiesToDoctorView;
        private AddPathologiesToDoctorView _addPathologiesToDoctorView;
        private AddPatientsToDoctorView _addPatientsToDoctorView;
        private ObservableCollection<Medecin> _doctorsList;
        private Medecin _seletedDoctor;
        private ObservableCollection<Specialite> _specialitiesList;
        private Pathology _selectedPathologyInDoctorsViewPathology;
        private bool _isFormEnabled;
        private Patient _selectedPatientInDoctorView;
        private ObservableCollection<PatientToAdd> _patientsToAddList;
        private PatientToAdd _selectedPatientToAdd;
        #endregion
        #region Properties

        private ObservableCollection<PathologyToAdd> _pathologiesToDoctorListAdds;


        public ObservableCollection<PathologyToAdd> PathologiesToDoctorList
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
        public ObservableCollection<PatientToAdd> PatientsToAddList
        {
            get
            {
                return _patientsToAddList;
            }

            set
            {
                if (_patientsToAddList == value)
                {
                    return;
                }

                _patientsToAddList = value;
                RaisePropertyChanged();
            }
        }
        public PatientToAdd SelectedPatientToAdd
        {
            get
            {
                return _selectedPatientToAdd;
            }

            set
            {
                if (_selectedPatientToAdd == value)
                {
                    return;
                }

                _selectedPatientToAdd = value;
                RaisePropertyChanged();
            }
        }
        #endregion
        #region Commands
        private RelayCommand _savePathologyWhithDoctorsCommand;
        public RelayCommand SavePathologyWhithDoctorsCommand
        {
            get
            {
                return _savePathologyWhithDoctorsCommand
                    ?? (_savePathologyWhithDoctorsCommand = new RelayCommand(
                    () =>
                    {
                        _addPathologiesToDoctorView.Close();

                    }));
            }
        }
        private RelayCommand _AddDoctorToPathologyLoadedCommand;
        public RelayCommand AddDoctorToPathologyLoadedCommand
        {
            get
            {
                return _AddDoctorToPathologyLoadedCommand
                    ?? (_AddDoctorToPathologyLoadedCommand = new RelayCommand(async () =>
                    {
                        PathologiesToDoctorList = new ObservableCollection<PathologyToAdd>(await Task.Run(() => _dbContext.Pathologies.Select(p => new PathologyToAdd())));

                    }));
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
                        await LoadDoctorsList();
                        await LoadSpacialities();
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
                                RolesCollection = new RolesCollection()
                                {
                                     //get the default medecin rolls from the xml settings file
                                },
                                UserTypeId = _dbContext.UserTypes.First(x => x.UserTypeName == "Medecin").UserTypeId
                            },
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
                        }
                        _dbContext.SaveChanges();
                        await LoadDoctorsList();

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
                    ?? (_addPatientsToSelectedDoctorCommand = new RelayCommand(
                    () =>
                    {
                        if (SelectedDoctor.MedecinId == Guid.Empty)
                        {
                            AddNewDoctor();
                        }
                        _addPatientsToDoctorView = new AddPatientsToDoctorView();
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
        private RelayCommand _addPatientsToDoctorLoadedCommand;
        public RelayCommand AddPatientsToDoctorLoadedCommand
        {
            get
            {
                return _addPatientsToDoctorLoadedCommand
                    ?? (_addPatientsToDoctorLoadedCommand = new RelayCommand(
                    () =>
                    {
                        LoadPatientsToAddCommand();

                    }));
            }
        }

        private async Task LoadPatientsToAddCommand()
        {
            await Task.Run(() =>
            {
                try
                {
                    var patientsList = _dbContext.Patients;
                    PatientsToAddList = new ObservableCollection<PatientToAdd>();
                    foreach (var patient in patientsList)
                    {
                        PatientsToAddList.Add(new PatientToAdd()
                        {
                            PatientId = patient.PatientId,
                            Nom = patient.Nom,
                            Prenom = patient.Prenom,
                            DateDeNaissance = patient.DateDeNaissance,
                            TelephoneFixe = patient.TelephoneFixe,
                            TelephoneMobile1 = patient.TelephoneMobile1,
                            TelephoneDaccompagnant = patient.TelephoneDaccompagnant,
                            AdressId = patient.AdressId,
                            Adresse = patient.Adresse,
                            DateDeDepot = patient.DateDeDepot,
                            Medecins = patient.Medecins,
                            NumeroDordre = patient.NumeroDordre,
                            RendezVouses = patient.RendezVouses,
                            Sexe = patient.Sexe,
                            SexeId = patient.SexeId,
                            IsAdded = (SelectedDoctor.Patients.Contains(patient)) ? true : false
                        });
                    }
                }
                catch (Exception)
                {

                    var exception = true;
                }
            });
        }

        //Add Patients To Doctor View
        private RelayCommand _savePatientsToDoctorsViewCommand;
        public RelayCommand SavePatientsToDoctorsViewCommand
        {
            get
            {
                return _savePatientsToDoctorsViewCommand
                    ?? (_savePatientsToDoctorsViewCommand = new RelayCommand(
                    () =>
                    {

                    }));
            }
        }
        private RelayCommand _deletePatientsToDoctorsViewCommand;
        public RelayCommand DeletePatientsToDoctorsViewCommand
        {
            get
            {
                return _deletePatientsToDoctorsViewCommand
                    ?? (_deletePatientsToDoctorsViewCommand = new RelayCommand(
                    () =>
                    {

                    }));
            }
        }
        private RelayCommand _cancelPatientsToDoctorsViewCommand;
        public RelayCommand CancelPatientsToDoctorsViewCommand
        {
            get
            {
                return _cancelPatientsToDoctorsViewCommand
                    ?? (_cancelPatientsToDoctorsViewCommand = new RelayCommand(
                    () =>
                    {

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
            await Task.Run(() =>
            {
                _dbContext.Medecins.Add(SelectedDoctor);
            });
        }
        #endregion
    }
}
