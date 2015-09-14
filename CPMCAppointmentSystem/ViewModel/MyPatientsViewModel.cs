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
    public class MyPatientsViewModel:NavigableViewModelBase
    {
        #region Fields
        private CpmcContext _dbContext=new CpmcContext();

        private ObservableCollection<Patient> _patientList;
        private Patient _selectedPatient;
        private Medecin _selectedDoctor;
        private RendezVous _selectedAppointement;
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
        #endregion
        #region Properties            
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
        #endregion
        #region Commands
        private RelayCommand _patientsViewLoadedCommand;  
        public RelayCommand PatientsViewLoadedCommand
        {
            get
            {
                return _patientsViewLoadedCommand
                    ?? (_patientsViewLoadedCommand = new RelayCommand(
                    () =>
                    {
                        LoadPatientList();
                    }));
            }
        }
        #endregion
        #region Ctors and Methods
        public MyPatientsViewModel(IFrameNavigationService mainFrameNavigationService, IInnerFrameNavigationService innerFrameNavigationService)
            : base(mainFrameNavigationService, innerFrameNavigationService)
        {
        }

        public async Task LoadPatientList()
        {
            if (SelectedDoctor!=null)          
            PatientList = new ObservableCollection<Patient>(await Task.Run(()=>SelectedDoctor.Patients));
        }
        #endregion        
    }
}
