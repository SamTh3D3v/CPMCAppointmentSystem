using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CPMCAppointmentSystem.Helpers;
using DataLayer.Model;
using GalaSoft.MvvmLight.Command;
using Syncfusion.Windows.Forms.Tools.Navigation;

namespace CPMCAppointmentSystem.ViewModel
{
    public class PatientsViewModel:NavigableViewModelBase
    {
        #region Fields
        private ObservableCollection<Patient> _patientList;
        private Patient _selectedPatient;
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
