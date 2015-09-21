using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CPMCAppointmentSystem.Helpers;
using CPMCAppointmentSystem.SubModel;
using CPMCAppointmentSystem.View;
using CPMCAppointmentSystem.View.PathologiesViews;
using DataLayer.Model;
using GalaSoft.MvvmLight.Command;


namespace CPMCAppointmentSystem.ViewModel
{
    public class PathologiesViewModel : NavigableViewModelBase
    {
        #region Fields
      
        private MedecinToAdd _selectedDoctorsToPathologyList  ;           
        private AddDoctorsToPathologyView _addDoctorsToPathologyView;
        private bool _isFormEnabled;
        private readonly CpmcContext _dbContext = new CpmcContext();
        private ObservableCollection<Pathology> _pathologiesList;
        private Pathology _selectedPathology;
        private Medecin _selectedDoctorWithinPathology;
        private ObservableCollection<MedecinToAdd> _doctorsToPathlogyList;
        #endregion
        #region Properties
        public MedecinToAdd SelectedDoctorsToPathologyList
        {
            get
            {
                return _selectedDoctorsToPathologyList;
            }

            set
            {
                if (_selectedDoctorsToPathologyList == value)
                {
                    return;
                }

                _selectedDoctorsToPathologyList = value;
                RaisePropertyChanged();
            }
        }
        public ObservableCollection<MedecinToAdd> DoctorsToPathlogyList
        {
            get
            {
                return _doctorsToPathlogyList;
            }

            set
            {
                if (_doctorsToPathlogyList == value)
                {
                    return;
                }

                _doctorsToPathlogyList = value;
                RaisePropertyChanged();
            }
        }
        public ObservableCollection<Pathology> PathologiesList
        {
            get
            {
                return _pathologiesList;
            }

            set
            {
                if (_pathologiesList == value)
                {
                    return;
                }

                _pathologiesList = value;
                RaisePropertyChanged();
            }
        }
        public Pathology SelectedPathology
        {
            get
            {
                return _selectedPathology;
            }

            set
            {
                if (_selectedPathology == value)
                {
                    return;
                }
                IsFormEnabled = true;
                _selectedPathology = value;
                RaisePropertyChanged();
            }
        }
        public Medecin SelectedDoctorWithinPathology
        {
            get
            {
                return _selectedDoctorWithinPathology;
            }

            set
            {
                if (_selectedDoctorWithinPathology == value)
                {
                    return;
                }

                _selectedDoctorWithinPathology = value;
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
        private RelayCommand _pathologyViewLoadedCommand;
        public RelayCommand PathologyViewLoadedCommand
        {
            get
            {
                return _pathologyViewLoadedCommand
                    ?? (_pathologyViewLoadedCommand = new RelayCommand(
                    () =>
                    {
                        LoadPathologies();

                    }));
            }
        }
        private RelayCommand _addDoctorToPathologyLoadedCommand;
        public RelayCommand AddDoctorToPathologyLoadedCommand
        {
            get
            {
                return _addDoctorToPathologyLoadedCommand
                    ?? (_addDoctorToPathologyLoadedCommand = new RelayCommand(
                    () =>
                    {
                        LoadDoctorsList();

                    }));
            }
        }
        private RelayCommand _addPathologyCommand;
        public RelayCommand AddPathologyCommand
        {
            get
            {
                return _addPathologyCommand
                    ?? (_addPathologyCommand = new RelayCommand(
                    () =>
                    {
                        SelectedPathology = new Pathology();
                        IsFormEnabled = true;

                    }));
            }
        }
        private RelayCommand _addDoctorToPathologyCommand;
        public RelayCommand AddDoctorToPathologyCommand
        {
            get
            {
                return _addDoctorToPathologyCommand
                    ?? (_addDoctorToPathologyCommand = new RelayCommand(
                    () =>
                    {                        
                        if (SelectedPathology.PathologyId == Guid.Empty)
                        {
                            AddNewPathology();
                        }                                                                  
                        _addDoctorsToPathologyView = new AddDoctorsToPathologyView();
                        _addDoctorsToPathologyView.ShowDialog();

                    }));
            }
        }
        private RelayCommand _savePathologyCommand;
        public RelayCommand SavePathologyCommand
        {
            get
            {
                return _savePathologyCommand
                    ?? (_savePathologyCommand = new RelayCommand(
                    () =>
                    {
                        if (SelectedPathology.PathologyId == Guid.Empty)
                        {
                            AddNewPathology();
                        }
                        _dbContext.SaveChanges();
                        LoadPathologies();

                    }));
            }
        }
        private RelayCommand _savePathologyWhithDoctorsCommand;     
        public RelayCommand SavePathologyWhithDoctorsCommand
        {
            get
            {
                return _savePathologyWhithDoctorsCommand
                    ?? (_savePathologyWhithDoctorsCommand = new RelayCommand(
                    () =>
                    {
                        
                    }));
            }
        }
        private RelayCommand _deletePathologyWhithDoctorsCommand;        
        public RelayCommand DeletePathologyWhithDoctorsCommand
        {
            get
            {
                return _deletePathologyWhithDoctorsCommand
                    ?? (_deletePathologyWhithDoctorsCommand = new RelayCommand(
                    () =>
                    {
                        
                    }));
            }
        }
        private RelayCommand _cancelPathologyWhithDoctorsCommand;   
        public RelayCommand CancelPathologyWhithDoctorsCommand
        {
            get
            {
                return _cancelPathologyWhithDoctorsCommand
                    ?? (_cancelPathologyWhithDoctorsCommand = new RelayCommand(
                    () => _addDoctorsToPathologyView.Close()));
            }
        }      
        private RelayCommand _deletePathologyCommand;
        public RelayCommand DeletePathologyCommand
        {
            get
            {
                return _deletePathologyCommand
                    ?? (_deletePathologyCommand = new RelayCommand(
                    () =>
                    {

                    }));
            }
        }
        private RelayCommand _cancelChangesToPathologyCommand;
        public RelayCommand CancelChangesToPathologyCommand
        {
            get
            {
                return _cancelChangesToPathologyCommand
                    ?? (_cancelChangesToPathologyCommand = new RelayCommand(
                    () =>
                    {

                    }));
            }
        }

        #endregion
        #region Ctors and Methods
        public PathologiesViewModel(IFrameNavigationService mainFrameNavigationService, IInnerFrameNavigationService innerFrameNavigationService)
            : base(mainFrameNavigationService, innerFrameNavigationService)
        {
        }
        private async void LoadPathologies()
        {
            PathologiesList = new ObservableCollection<Pathology>(await Task.Run(() => _dbContext.Pathologies));
        }
        private async void LoadDoctorsList()
        {
            await Task.Run(() =>
            {
                var doctorsList = _dbContext.Medecins;
                DoctorsToPathlogyList = new ObservableCollection<MedecinToAdd>();
                foreach (var medecin in doctorsList)
                {
                    DoctorsToPathlogyList.Add(new MedecinToAdd()
                    {
                        MedecinId = medecin.MedecinId,                       
                        DateDeNaissance = medecin.DateDeNaissance,
                        TelephoneFixe = medecin.TelephoneFixe,
                        TelephoneMobile = medecin.TelephoneMobile,
                        SpecialiteId = medecin.SpecialiteId,
                        UserId = medecin.UserId,
                        Speciality = medecin.Speciality,
                        User = medecin.User,
                        Pathologies = medecin.Pathologies,
                        Patients = medecin.Patients,
                        IsAdded = (SelectedPathology.Medecins.Contains(medecin)) ? true : false
                    });
                }
            });
        }
        private void AddNewPathology()
        {
            _dbContext.Pathologies.Add(SelectedPathology);
            IsFormEnabled = false;
        }
        #endregion
    }
}
