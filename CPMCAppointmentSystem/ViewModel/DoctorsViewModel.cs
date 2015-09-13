using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CPMCAppointmentSystem.Helpers;
using DataLayer.Model;
using GalaSoft.MvvmLight.Command;

namespace CPMCAppointmentSystem.ViewModel
{
    public class DoctorsViewModel:NavigableViewModelBase
    {
        #region Fields
        private CpmcContext _dbContext=new CpmcContext();
        private ObservableCollection<Medecin> _doctorsList;
        private Medecin _seletedDoctor;
        private ObservableCollection<Specialite> _specialitiesList;
        private bool _isFormEnabled;
        private Specialite _selectedDoctorSpeciality;
        #endregion
        #region Properties              
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

                _seletedDoctor = value;
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
        public Specialite SelectedDoctoSpeciality
        {
            get
            {
                return _selectedDoctorSpeciality;
            }

            set
            {
                if (_selectedDoctorSpeciality == value)
                {
                    return;
                }

                _selectedDoctorSpeciality = value;
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
        private RelayCommand _doctorsViewLoadedCommand;
        public RelayCommand DoctorsViewLoadedCommand
        {
            get
            {
                return _doctorsViewLoadedCommand
                    ?? (_doctorsViewLoadedCommand = new RelayCommand(
                    () =>
                    {
                        LoadDoctorsList();
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
                        
                    }));
            }
        }

        private RelayCommand _saveDoctorCommand;
        public RelayCommand SaveDoctorCommand
        {
            get
            {
                return _saveDoctorCommand
                    ?? (_saveDoctorCommand = new RelayCommand(
                    () =>
                    {
                        
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
                        
                    }));
            }
        }
        #endregion
        #region Ctors and Methods
        public DoctorsViewModel(IFrameNavigationService mainFrameNavigationService, IInnerFrameNavigationService innerFrameNavigationService)
            : base(mainFrameNavigationService, innerFrameNavigationService)
        {
        }
        private async void LoadDoctorsList()
        {
            DoctorsList = new ObservableCollection<Medecin>(await Task.Run(() => _dbContext.Medecins));
        }
        #endregion        
    }
}
