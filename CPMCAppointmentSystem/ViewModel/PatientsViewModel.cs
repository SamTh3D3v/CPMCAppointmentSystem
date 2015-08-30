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
    public class PatientsViewModel:NavigableViewModelBase
    {
        #region Fields
        private ObservableCollection<Patient> _patientList;
        private Patient _selectedPatient;
        private ObservableCollection<Sexe> _sexeList;
        private readonly CpmcContext _dbContext=new CpmcContext();
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
                        PatientList = new ObservableCollection<Patient>(await Task.Run(()=>_dbContext.Patients));
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
                    }));
            }
        }
        private RelayCommand _saveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                return _saveCommand
                    ?? (_saveCommand = new RelayCommand(
                    () =>
                    {
                        if (SelectedPatient.PatientId==Guid.Empty)                        
                            _dbContext.Patients.Add(SelectedPatient);                        
                        _dbContext.SaveChanges();
                    }));
            }
        }
        private RelayCommand _deleteCommand;
        public RelayCommand DeleteCommand
        {
            get
            {
                return _deleteCommand
                    ?? (_deleteCommand = new RelayCommand(
                    () =>
                    {
                        
                    }));
            }
        }
        private RelayCommand _cancelCommand;
        public RelayCommand CancelCommand
        {
            get
            {
                return _cancelCommand
                    ?? (_cancelCommand = new RelayCommand(
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
        #endregion        
    }
}
