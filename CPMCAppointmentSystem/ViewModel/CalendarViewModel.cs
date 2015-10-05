using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using CPMCAppointmentSystem.Helpers;
using DataLayer.Model;
using GalaSoft.MvvmLight.Command;
using Syncfusion.UI.Xaml.Schedule;

namespace CPMCAppointmentSystem.ViewModel
{
    public class CalendarViewModel : NavigableViewModelBase
    {
        #region Fields
        private CpmcContext _dbContext = new CpmcContext();
        private ScheduleAppointmentCollection _patientsScheduleAppointmentCollection = new ScheduleAppointmentCollection();
        private RendezVous _selectedRdv;
        private ObservableCollection<Patient> _allPatientsCollection;

        private ObservableCollection<Medecin> _allDoctorsCollection;

        private Patient _selectedPatientInAddAptView;
        private ObservableCollection<RendezVous> _rdvousCollaction;
        private Medecin _selectedMedecinInAddAptView;
        #endregion
        #region Properties
        public ObservableCollection<RendezVous> RdvousCollection
        {
            get
            {
                return _rdvousCollaction;
            }

            set
            {
                if (_rdvousCollaction == value)
                {
                    return;
                }

                _rdvousCollaction = value;
                RaisePropertyChanged();
            }
        }
        public Medecin SelectedMedecinInAddAptView
        {
            get
            {
                return _selectedMedecinInAddAptView;
            }

            set
            {
                if (_selectedMedecinInAddAptView == value)
                {
                    return;
                }

                _selectedMedecinInAddAptView = value;
                RaisePropertyChanged();
            }
        }
        public Patient SelectedPatientInAddAptView
        {
            get
            {
                return _selectedPatientInAddAptView;
            }

            set
            {
                if (_selectedPatientInAddAptView == value)
                {
                    return;
                }

                _selectedPatientInAddAptView = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<Medecin> AllDoctorsCollection
        {
            get
            {
                return _allDoctorsCollection;
            }

            set
            {
                if (_allDoctorsCollection == value)
                {
                    return;
                }

                _allDoctorsCollection = value;
                RaisePropertyChanged();
            }
        }
        public ObservableCollection<Patient> AllPatientsCollection
        {
            get
            {
                return _allPatientsCollection;
            }

            set
            {
                if (_allPatientsCollection == value)
                {
                    return;
                }

                _allPatientsCollection = value;
                RaisePropertyChanged();
            }
        }
        public RendezVous SelectedRdv
        {
            get
            {
                return _selectedRdv;
            }

            set
            {
                if (_selectedRdv == value)
                {
                    return;
                }

                _selectedRdv = value;
                RaisePropertyChanged();
            }
        }
        public ScheduleAppointmentCollection PatientsScheduleAppointmentCollection
        {
            get
            {
                return _patientsScheduleAppointmentCollection;
            }

            set
            {
                if (_patientsScheduleAppointmentCollection == value)
                {
                    return;
                }

                _patientsScheduleAppointmentCollection = value;
                RaisePropertyChanged();
            }
        }
        #endregion
        #region Commands
        private RelayCommand _calendarViewLoadedCommand;
        public RelayCommand CalendarViewLoadedCommand
        {
            get
            {
                return _calendarViewLoadedCommand
                    ?? (_calendarViewLoadedCommand = new RelayCommand(async () =>
                    {
                        await LoadRendezVous();

                    }));
            }
        }
        private RelayCommand _addAppointementViewLoadedCommand;
        public RelayCommand AddAppointementViewLoadedCommand
        {
            get
            {
                return _addAppointementViewLoadedCommand
                    ?? (_addAppointementViewLoadedCommand = new RelayCommand(async () =>
                    {
                        LoadAddAppointmentViewItemSources();
                        await LoadAllPatientsList();
                        await LoadAllDoctorsList();

                    }));
            }
        }

        private async Task LoadAllDoctorsList()
        {
            AllDoctorsCollection = new ObservableCollection<Medecin>(await Task.Run(() => _dbContext.Medecins));
        }

        private async Task LoadAllPatientsList()
        {
            AllPatientsCollection = new ObservableCollection<Patient>(await Task.Run(() => _dbContext.Patients));
        }

        private void LoadAddAppointmentViewItemSources()
        {
            //Load Other stuff
        }

        private async Task LoadRendezVous()
        {
            RdvousCollection = new ObservableCollection<RendezVous>(await Task.Run(() => _dbContext.RendezVouses));
            foreach (var rdv in RdvousCollection)
            {
                //Update the rdv status based on rdv date
                               
                PatientsScheduleAppointmentCollection.Add(rdv);
            }
            //PatientsScheduleAppointmentCollection.Add(new RendezVous() { Status = new ScheduleAppointmentStatus() { Brush = new SolidColorBrush(Colors.Green), Status = "Free" }, StartTime = new DateTime(2015, 10, 10, 5, 0, 0), Subject = "Meet the doc", Location = "Hutchison road", AllDay = false });

        }

        #endregion
        #region Ctors and Methods
        public CalendarViewModel(IFrameNavigationService mainFrameNavigationService, IInnerFrameNavigationService innerFrameNavigationService)
            : base(mainFrameNavigationService, innerFrameNavigationService)
        {
        }
        #endregion
    }
}
