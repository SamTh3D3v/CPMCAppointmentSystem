using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using CPMCAppointmentSystem.Helpers;
using CPMCAppointmentSystem.SubModel;
using CPMCAppointmentSystem.View.AppointementViews;
using DataLayer.Model;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Syncfusion.Data.Extensions;
using Syncfusion.UI.Xaml.Schedule;
using Syncfusion.Windows.Shared;

namespace CPMCAppointmentSystem.ViewModel
{
    public struct BrushStatus
    {
        public Brush Brush { get; set; }
        public String Status { get; set; }
    }

    public class CalendarViewModel : NavigableViewModelBase
    {
        #region Fields
        private SettingsCollection _settingsCollection;
        private bool _isProgressRingActive;
        private CpmcContext _dbContext = new CpmcContext();
        private ScheduleType _scheduleType = ScheduleType.Month;
        private ScheduleAppointmentCollection _patientsScheduleAppointmentCollection = new ScheduleAppointmentCollection();
        private RendezVous _selectedRdv;
        private ObservableCollection<Patient> _allPatientsCollection;
        private ObservableCollection<Medecin> _allDoctorsCollection;
        private Patient _selectedPatientInAddAptView;
        private ObservableCollection<RendezVous> _rdvousCollaction;
        private Medecin _selectedMedecinInAddAptView;
        private ObservableCollection<EntityToAdd<Medecin>> _addDoctorsToFilterListAdd;
        private ObservableCollection<Medecin> _doctorsInFilter = new ObservableCollection<Medecin>();
        private bool _filterByPatientIsChecked;
        private bool _filterByMedecinIsChecked;
        private ListMedecinToAddView _listMedecinToAddView;
        private AddAppointementView _addAppointementView;
        #endregion
        #region Properties
        public SettingsCollection SettingsCollection
        {
            get
            {
                return _settingsCollection;
            }

            set
            {
                if (_settingsCollection == value)
                {
                    return;
                }

                _settingsCollection = value;
                RaisePropertyChanged();
            }
        }
        public bool IsProgressRingActive
        {
            get
            {
                return _isProgressRingActive;
            }

            set
            {
                if (_isProgressRingActive == value)
                {
                    return;
                }

                _isProgressRingActive = value;
                RaisePropertyChanged();
            }
        }
        public ScheduleType SelectedScheduleType
        {
            get
            {
                return _scheduleType;
            }

            set
            {
                if (_scheduleType == value)
                {
                    return;
                }

                _scheduleType = value;
                RaisePropertyChanged();
            }
        }
        public DateTime SelectedDateInScedule { get; set; }
        public bool FilterByPatientIsChecked
        {
            get
            {
                return _filterByPatientIsChecked;
            }

            set
            {
                if (_filterByPatientIsChecked == value)
                {
                    return;
                }

                _filterByPatientIsChecked = value;
                RaisePropertyChanged();
            }
        }
        public bool FilterByMedecinIsChecked
        {
            get
            {
                return _filterByMedecinIsChecked;
            }

            set
            {
                if (_filterByMedecinIsChecked == value)
                {
                    return;
                }

                _filterByMedecinIsChecked = value;
                RaisePropertyChanged();
            }
        }
        public ObservableCollection<Medecin> DoctorsInFilter
        {
            get
            {
                return _doctorsInFilter;
            }

            set
            {
                if (_doctorsInFilter == value)
                {
                    return;
                }

                _doctorsInFilter = value;
                RaisePropertyChanged();
            }
        }
        public ObservableCollection<EntityToAdd<Medecin>> AddDoctorsToFilterList
        {
            get
            {
                return _addDoctorsToFilterListAdd;
            }

            set
            {
                if (_addDoctorsToFilterListAdd == value)
                {
                    return;
                }

                _addDoctorsToFilterListAdd = value;
                RaisePropertyChanged();
            }
        }
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
        private RelayCommand _cancelMedecinFilterCommand;
        public RelayCommand CancelMedecinFilterCommand
        {
            get
            {
                return _cancelMedecinFilterCommand
                    ?? (_cancelMedecinFilterCommand = new RelayCommand(
                    () => _listMedecinToAddView.Close()));
            }
        }
        private RelayCommand _applyMedecinFilterCommand;
        public RelayCommand ApplyMedecinFilterCommand
        {
            get
            {
                return _applyMedecinFilterCommand
                    ?? (_applyMedecinFilterCommand = new RelayCommand(
                    () =>
                    {
                        // DoctorsInFilter = new ObservableCollection<Medecin>(AddDoctorsToFilterList.Where(x => x.IsAdded));
                        _listMedecinToAddView.Close();
                    }));
            }
        }
        private RelayCommand _showListDesMedecinFilterCommand;
        public RelayCommand ShowListDesMedecinFilterCommand
        {
            get
            {
                return _showListDesMedecinFilterCommand
                    ?? (_showListDesMedecinFilterCommand = new RelayCommand(
                    () =>
                    {
                        //to be updated
                        _listMedecinToAddView = new ListMedecinToAddView();
                        _listMedecinToAddView.ShowDialog();

                    }));
            }
        }
        private RelayCommand _calendarViewLoadedCommand;
        public RelayCommand CalendarViewLoadedCommand
        {
            get
            {
                return _calendarViewLoadedCommand
                    ?? (_calendarViewLoadedCommand = new RelayCommand(async () =>
                    {
                        IsProgressRingActive = true;
                        await LoadRendezVous();
                        IsProgressRingActive = false;

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
        private RelayCommand _listMedecinToAddViewLoadedCommand;
        public RelayCommand ListMedecinToAddViewLoadedCommand
        {
            get
            {
                return _listMedecinToAddViewLoadedCommand
                    ?? (_listMedecinToAddViewLoadedCommand = new RelayCommand(async () =>
                    {
                        await LoadDoctorsToAddList();

                    }));
            }
        }

        private async Task LoadDoctorsToAddList()
        {
            AddDoctorsToFilterList = new ObservableCollection<EntityToAdd<Medecin>>(await Task.Run(() => _dbContext.Medecins.Select(s => new EntityToAdd<Medecin>()
            {
                Entity = s,
                IsAdded = true
            })));

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
            await LoadScheduleSettings();
            RdvousCollection = new ObservableCollection<RendezVous>(await Task.Run(() => _dbContext.RendezVouses));
            RdvousCollection.ForEach((rdv) =>
            {
                //Update the rdv status based on rdv date
                rdv.Status = new ScheduleAppointmentStatus()
                {
                    Brush =
                        (GetBrushFromSettings(rdv.DateTimeRdv, rdv.Patient.DateDeNaissance,
                            rdv.Patient.Sexe.Designation, rdv.Patient.CarteProfessionel)).Brush,
                    Status =
                        (GetBrushFromSettings(rdv.DateTimeRdv, rdv.Patient.DateDeNaissance,
                            rdv.Patient.Sexe.Designation, rdv.Patient.CarteProfessionel)).Status
                };
                PatientsScheduleAppointmentCollection.Add(rdv);
            });
            RaisePropertyChanged("PatientsScheduleAppointmentCollection");
        }

        private async Task LoadScheduleSettings()
        {
            SettingsCollection = new SettingsCollection();
            await SettingsCollection.LoadSchedulerSettings();
        }


        private BrushStatus GetBrushFromSettings(DateTime dateTimeRdv, DateTime dateDeNaissance, string sexe, bool carteProfessionel)
        {
            var brushStatus = new BrushStatus();
            if (dateTimeRdv.Date < DateTime.Now.Date)
            {
                brushStatus.Brush = new SolidColorBrush(Colors.LightGray); 
                if ((DateTime.Now.Year - dateDeNaissance.Year) < 18)
                {
                    brushStatus.Status = sexe == "Male" ? "Boy" : "Girl";
                }
                else
                {
                    brushStatus.Status = sexe == "Male" ? "Man" : "Woman";
                }
            }
            else
            {
                var ageMin = SettingsCollection["EnfantSetting"].Information;
                if (ageMin != null)
                {
                    if ((DateTime.Now.Year - dateDeNaissance.Year) < int.Parse(ageMin))
                    {
                        brushStatus.Status = sexe == "Male" ? "Boy" : "Girl";
                        brushStatus.Brush =
                            new SolidColorBrush(
                                (Color) ColorConverter.ConvertFromString(SettingsCollection["EnfantSetting"].Color));
                    }
                    else
                    {
                        brushStatus.Status = sexe == "Male" ? "Man" : "Woman";
                        brushStatus.Brush = sexe == "Male"
                            ? new SolidColorBrush(
                                (Color) ColorConverter.ConvertFromString(SettingsCollection["HommeSetting"].Color))
                            : new SolidColorBrush(
                                (Color) ColorConverter.ConvertFromString(SettingsCollection["FemmeSetting"].Color));
                    }
                }
            }
            return brushStatus;
        }
        private RelayCommand<object> _scheduleOnAppointmentEditorOpeningCommand;

        public RelayCommand<object> ScheduleOnAppointmentEditorOpeningCommand
        {
            get
            {
                return _scheduleOnAppointmentEditorOpeningCommand
                    ?? (_scheduleOnAppointmentEditorOpeningCommand = new RelayCommand<object>(
                    (obj) =>
                    {
                        _addAppointementView = new AddAppointementView();

                        var sfSchedule = obj as SfSchedule;
                        if (sfSchedule != null)
                        {
                            var selectedAppointement = sfSchedule.SelectedAppointment;                            
                            if (selectedAppointement != null)
                            {
                                SelectedRdv = (RendezVous)selectedAppointement;
                            }
                            else
                            {
                                SelectedRdv = new RendezVous()
                                {
                                    DateTimeRdv = SelectedDateInScedule
                                };

                            }
                        }
                        _addAppointementView.ShowDialog();

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
                    () => _addAppointementView.Close()));
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
                        if (SelectedRdv != null)
                        {
                            _dbContext.RendezVouses.Remove(SelectedRdv);
                        }
                        _dbContext.SaveChanges();
                        _addAppointementView.Close();
                        await LoadRendezVous();
                        

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
                        if (SelectedRdv.RendezVousId == Guid.Empty)
                        {
                            AddNewAppointement();
                        }
                        else
                        {
                            //Notification insertion
                        }
                        _dbContext.SaveChanges();
                        _addAppointementView.Close();
                        await LoadRendezVous();

                    }));
            }
        }
        private void AddNewAppointement()
        {
            //Added by Farouk for Audit purpose
            SelectedRdv.RendezVousId = Guid.NewGuid();
            _dbContext.RendezVouses.Add(SelectedRdv);
        }
        private RelayCommand<ScheduleClickEventArgs> _onScheduleClickCommand;
        public RelayCommand<ScheduleClickEventArgs> OnScheduleClickCommand
        {
            get
            {
                return _onScheduleClickCommand
                    ?? (_onScheduleClickCommand = new RelayCommand<ScheduleClickEventArgs>(
                    (args) =>
                    {
                        SelectedDateInScedule = (DateTime)args.SelectedDate;
                    }));
            }
        }
        private RelayCommand _dayScheduleTypeSelectedCommand;
        public RelayCommand DayScheduleTypeSelectedCommand
        {
            get
            {
                return _dayScheduleTypeSelectedCommand
                    ?? (_dayScheduleTypeSelectedCommand = new RelayCommand(async () =>
                    {
                        SelectedScheduleType = ScheduleType.Day;
                        await LoadRendezVous();
                    }));
            }
        }
        private RelayCommand _monthScheduleTypeSelectedCommand;
        public RelayCommand MonthScheduleTypeSelectedCommand
        {
            get
            {
                return _monthScheduleTypeSelectedCommand
                    ?? (_monthScheduleTypeSelectedCommand = new RelayCommand(async () =>
                    {
                        SelectedScheduleType = ScheduleType.Month;
                        await LoadRendezVous();
                    }));
            }
        }
        private RelayCommand _weekScheduleTypeSelectedCommand;
        public RelayCommand WeekScheduleTypeSelectedCommand
        {
            get
            {
                return _weekScheduleTypeSelectedCommand
                    ?? (_weekScheduleTypeSelectedCommand = new RelayCommand(async () =>
                    {
                        SelectedScheduleType = ScheduleType.Week;
                        await LoadRendezVous();
                    }));
            }
        }
        private RelayCommand _workWeekScheduleTypeSelectedCommand;
        public RelayCommand WorkWeekScheduleTypeSelectedCommand
        {
            get
            {
                return _workWeekScheduleTypeSelectedCommand
                    ?? (_workWeekScheduleTypeSelectedCommand = new RelayCommand(async () =>
                    {
                        SelectedScheduleType = ScheduleType.WorkWeek;
                        await LoadRendezVous();
                    }));
            }
        }
        private RelayCommand _timeLineScheduleTypeSelectedCommand;
        public RelayCommand TimeLineScheduleTypeSelectedCommand
        {
            get
            {
                return _timeLineScheduleTypeSelectedCommand
                    ?? (_timeLineScheduleTypeSelectedCommand = new RelayCommand(async () =>
                    {
                        SelectedScheduleType = ScheduleType.TimeLine;
                        await LoadRendezVous();
                    }));
            }
        }
        private RelayCommand<AppointmentEndDraggingEventArgs> _appointmentEndDraggingCommand;
        public RelayCommand<AppointmentEndDraggingEventArgs> AppointmentEndDraggingCommand
        {
            get
            {
                return _appointmentEndDraggingCommand
                    ?? (_appointmentEndDraggingCommand = new RelayCommand<AppointmentEndDraggingEventArgs>(async (args) =>
                    {
                        var result=await((Application.Current.MainWindow as MetroWindow).ShowMessageAsync("Confirmation", "etes vous sure de vouloire faire deplacer ce rendez-vous",MessageDialogStyle.AffirmativeAndNegative));
                        if (result == MessageDialogResult.Affirmative)
                        {
                            var rdv = args.Appointment as RendezVous;
                            _dbContext.RendezVouses.Find(rdv.RendezVousId).DateTimeRdv = args.To;
                            _dbContext.SaveChanges(); 
                        }
                        else
                        {
                            args.Cancel = true;
                            //await LoadRendezVous();
                            Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Refresh"));
                        }                                                                      
                    }));
            }
        }

        #endregion
        #region Ctors and Methods
        public CalendarViewModel(IFrameNavigationService mainFrameNavigationService, IInnerFrameNavigationService innerFrameNavigationService)
            : base(mainFrameNavigationService, innerFrameNavigationService)
        {
            Messenger.Default.Register<DateTime>(this, (d) =>
            {
                SelectedDateInScedule = d;
            });
        }
        #endregion
    }
}
