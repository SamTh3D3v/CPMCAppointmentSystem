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
        private CpmcContext _dbContext=new CpmcContext();
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
