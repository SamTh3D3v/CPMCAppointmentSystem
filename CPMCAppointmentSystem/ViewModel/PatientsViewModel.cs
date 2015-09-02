using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CPMCAppointmentSystem.Helpers;
using CPMCAppointmentSystem.View;
using DataLayer.Model;
using GalaSoft.MvvmLight.Command;

namespace CPMCAppointmentSystem.ViewModel
{
    public class PatientsViewModel:NavigableViewModelBase
    {
        #region Fields
        private ObservableCollection<Patient> _patientList;
        private Patient _selectedPatient;
        private ObservableCollection<Sexe> _sexeList;
        private readonly CpmcContext _dbContext=new CpmcContext();
        private ObservableCollection<Medecin> _doctorsList;
        private Medecin _selectedDoctor;
        private RendezVous _selectedAppointement;
        private ObservableCollection<Willaya> _willayasList;
        private bool _isFormEnabled;
        #endregion
        #region Properties        
        public ObservableCollection<Patient> PatientList
        {
            get
            {
                return _patientList;
            }

            set
            {
                if (_patientList == value)
                {
                    return;
                }

                _patientList = value;
                RaisePropertyChanged();
            }
        }               
        public Patient SelectedPatient
        {
            get
            {
                return _selectedPatient;
            }

            set
            {
                if (_selectedPatient == value)
                {                    
                    return;
                }
                IsFormEnabled = true;
                _selectedPatient = value;
                RaisePropertyChanged();
            }
        }
        public ObservableCollection<Sexe> SexeList
        {
            get
            {
                return _sexeList;
            }

            set
            {
                if (_sexeList == value)
                {
                    return;
                }

                _sexeList = value;
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
                return _selectedDoctor;
            }

            set
            {
                if (_selectedDoctor == value)
                {
                    return;
                }

                _selectedDoctor = value;
                RaisePropertyChanged();
            }
        }             
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
        
        public ObservableCollection<Willaya> WillayasList
        {
            get
            {
                return _willayasList;
            }

            set
            {
                if (_willayasList == value)
                {
                    return;
                }

                _willayasList = value;
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
        #endregion
        #region Commands
        private RelayCommand _patientsViewLoadedCommand;
        public RelayCommand PatientsViewLoadedCommand
        {
            get
            {
                return _patientsViewLoadedCommand
                    ?? (_patientsViewLoadedCommand = new RelayCommand(async () =>
                    { 
                        SexeList=new ObservableCollection<Sexe>(await Task.Run(()=>_dbContext.Sexes));
                        WillayasList=new ObservableCollection<Willaya>(await  Task.Run(()=>_dbContext.Willayas));
                        LoadPatienstList();
                        LoadDoctorsList();
                    }));
            }
        }
        private RelayCommand _addPatientCommand;
        public RelayCommand AddPatientCommand
        {
            get
            {
                return _addPatientCommand
                    ?? (_addPatientCommand = new RelayCommand(
                    () =>
                    {
                        SelectedPatient=new Patient()
                        {
                            Adresse = new Adresse()
                        };
                        IsFormEnabled = true;
                    }));
            }
        }
        private RelayCommand _savePatientCommand;
        public RelayCommand SavePatientCommand
        {
            get
            {
                return _savePatientCommand
                    ?? (_savePatientCommand = new RelayCommand(
                    () =>
                    {
                        if (SelectedPatient.PatientId == Guid.Empty)
                        {
                            _dbContext.Patients.Add(SelectedPatient);     
                            IsFormEnabled = false;
                        }                                                                     
                        _dbContext.SaveChanges();
                        LoadPatienstList();
                    }));
            }
        }
        private RelayCommand _deletePatientCommand;
        public RelayCommand DeletePatientCommand
        {
            get
            {
                return _deletePatientCommand
                    ?? (_deletePatientCommand = new RelayCommand(
                    () =>
                    {
                        //
                    }));
            }
        }
        private RelayCommand _cancelPatientChangesCommand;
        public RelayCommand CancelPatientChangesCommand
        {
            get
            {
                return _cancelPatientChangesCommand
                    ?? (_cancelPatientChangesCommand = new RelayCommand(
                    () =>
                    {
                        
                    }));
            }
        }

        private RelayCommand _addAppointementCommand;   
        public RelayCommand AddAppointementCommand
        {
            get
            {
                return _addAppointementCommand
                    ?? (_addAppointementCommand = new RelayCommand(
                    () =>
                    {                        
                        var addAppointementWindow = new AddPatientAppointment();
                        addAppointementWindow.ShowDialog();

                    }));
            }
        }
        private RelayCommand _addAppointementLoadedCommand;
        public RelayCommand AddAppointementLoadedCommand
        {
            get
            {
                return _addAppointementLoadedCommand
                    ?? (_addAppointementLoadedCommand = new RelayCommand(
                    () =>
                    {
                                                
                        
                    }));
            }
        }

        private RelayCommand _saveAppointementCommand;
        public RelayCommand SaveAppointementCommand
        {
            get
            {
                return _saveAppointementCommand
                    ?? (_saveAppointementCommand = new RelayCommand(
                    () =>
                    {
                        
                    }));
            }
        }
        private RelayCommand _deleteAppointementCommand;
        public RelayCommand DeleteAppointementCommand
        {
            get
            {
                return _deleteAppointementCommand
                    ?? (_deleteAppointementCommand = new RelayCommand(
                    () =>
                    {
                        
                    }));
            }
        }
        private RelayCommand _cancelAppointementChangesCommand;
        public RelayCommand CancelAppointementChangesCommand
        {
            get
            {
                return _cancelAppointementChangesCommand
                    ?? (_cancelAppointementChangesCommand = new RelayCommand(
                    () =>
                    {
                        
                    }));
            }
        }
        #endregion
        #region Ctors and Methods
        public PatientsViewModel(IFrameNavigationService mainFrameNavigationService, IInnerFrameNavigationService innerFrameNavigationService)
            : base(mainFrameNavigationService, innerFrameNavigationService)
        {
        }
        private async void LoadDoctorsList()
        {
            DoctorsList = new ObservableCollection<Medecin>(await Task.Run(() => _dbContext.Medecins));
        }
        private async void LoadPatienstList()
        {
            PatientList = new ObservableCollection<Patient>(await Task.Run(() => _dbContext.Patients));
        }
        #endregion        
    }
}
