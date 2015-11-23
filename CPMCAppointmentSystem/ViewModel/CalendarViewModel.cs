using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
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
        public bool Blink { get; set; }
    }

    public class CalendarViewModel : NavigableViewModelBase
    {
        #region Fields
        private ObservableCollection<JourFerie> _restDaysCollection;
        private bool _showSpesificDayDoctors =true ;               
        private bool _carteProFilterIsEnabled  ;             
        private bool _trancheDageIsChecked;
        private bool _carteProIsChecked;
        private SettingsCollection _settingsCollection;
        private bool _isProgressRingActive;
        private CpmcContext _dbContext = new CpmcContext();
        private ScheduleType _scheduleType = ScheduleType.Month;
        private ScheduleAppointmentCollection _patientsScheduleAppointmentCollection;
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
        private int _lowerValue  ;
        private int _ageUpperValue;     
        #endregion
        #region Properties
        public ObservableCollection<JourFerie> RestDaysCollection
        {
            get
            {
                return _restDaysCollection;
            }

            set
            {
                if (_restDaysCollection == value)
                {
                    return;
                }

                _restDaysCollection = value;
                RaisePropertyChanged();
            }
        }
        public bool ShowSpesificDayDoctors
        {
            get
            {
                return _showSpesificDayDoctors;
            }

            set
            {
                if (_showSpesificDayDoctors == value)
                {
                    return;
                }

                _showSpesificDayDoctors = value;
                RaisePropertyChanged();
            }
        }
        public bool CarteProFilterIsEnabled
        {
            get
            {
                return _carteProFilterIsEnabled;
            }

            set
            {
                if (_carteProFilterIsEnabled == value)
                {
                    return;
                }

                _carteProFilterIsEnabled = value;
                RaisePropertyChanged();
            }
        }
        public int AgeLowerValue
        {
            get
            {
                return _lowerValue;
            }

            set
            {
                if (_lowerValue == value)
                {
                    return;
                }

                _lowerValue = value;
                RaisePropertyChanged();
            }
        }
        public int AgeUpperValue
        {
            get
            {
                return _ageUpperValue;
            }

            set
            {
                if (_ageUpperValue == value)
                {
                    return;
                }

                _ageUpperValue = value;
                RaisePropertyChanged();
            }
        }
        public bool CarteProIsChecked
        {
            get
            {
                return _carteProIsChecked;
            }

            set
            {
                if (_carteProIsChecked == value)
                {
                    return;
                }

                _carteProIsChecked = value;
                RaisePropertyChanged();
            }
        }
        public bool TrancheDageIsChecked
        {
            get
            {
                return _trancheDageIsChecked;
            }

            set
            {
                if (_trancheDageIsChecked == value)
                {
                    return;
                }

                _trancheDageIsChecked = value;
                RaisePropertyChanged();
            }
        }
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
                    ?? (_applyMedecinFilterCommand = new RelayCommand(async () =>
                    {
                        DoctorsInFilter = new ObservableCollection<Medecin>(AddDoctorsToFilterList.Where(x => x.IsAdded).Select(x => x.Entity));
                        _listMedecinToAddView.Close();
                        await LoadRendezVous();
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

        private User _connectedUser;

        public User ConnectedUser
        {
            get
            {
                return _connectedUser;
            }

            set
            {
                if (_connectedUser == value)
                {
                    return;
                }

                _connectedUser = value;
                RaisePropertyChanged();
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
                        _dbContext=new CpmcContext();
                        IsProgressRingActive = true;
                        await LoadRendezVous();
                        DoctorsInFilter=new ObservableCollection<Medecin>(await Task.Run(()=>_dbContext.Medecins));
                        IsProgressRingActive = false;                        

                        try
                        {
                            var user = MainFrameNavigationService.Parameter as User;
                            if (user != null)                           //todo 
                                ConnectedUser = _dbContext.Users.Find(user.UserId);

                        }
                        catch (Exception)
                        {

                        }
                    }));
            }
        }

        private RelayCommand _calendarViewUnLoadedCommand;
        public RelayCommand CalendarViewUnLoadedCommand
        {
            get
            {
                return _calendarViewUnLoadedCommand
                    ?? (_calendarViewUnLoadedCommand = new RelayCommand(
                    () =>
                    {
                        _dbContext.Dispose();
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
                        ShowSpesificDayDoctors = true;
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
            })));   
            AddDoctorsToFilterList.ForEach(d =>
            {
                d.IsAdded = DoctorsInFilter.Select(df => df.MedecinId).Contains(d.Entity.MedecinId);
            });
        }

        private async Task LoadAllDoctorsList()
        {
            var day = (Days)Math.Pow(2, (((double)(SelectedRdv.DateTimeRdv.DayOfWeek)) + 1) % 7);
            AllDoctorsCollection = ShowSpesificDayDoctors ? new ObservableCollection<Medecin>(await Task.Run(() => _dbContext.Medecins.Where(m => m.JoursDeTravail.HasFlag(day)))) : new ObservableCollection<Medecin>(await Task.Run(() => _dbContext.Medecins));
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
            _dbContext.Dispose();
            _dbContext = new CpmcContext();
            await LoadScheduleSettings();
            RdvousCollection = new ObservableCollection<RendezVous>(await Task.Run(() => _dbContext.RendezVouses));
            _patientsScheduleAppointmentCollection = new ScheduleAppointmentCollection();
            RdvousCollection.ForEach((rdv) =>
            {
                var brStBl = GetBrushFromSettings(rdv.DateTimeRdv, rdv.Patient.DateDeNaissance,
                    rdv.Patient.Sexe.Designation, rdv.Patient.CarteProfessionel);
                //Update the rdv status based on rdv date
                rdv.Status = new ScheduleAppointmentStatus()
                {
                    Brush =
                        brStBl.Brush,
                    Status =
                        brStBl.Status
                };
                rdv.Blink = brStBl.Blink;
                if (FilterByMedecinIsChecked)
                {
                    if (DoctorsInFilter.Select(d => d.MedecinId).Contains(rdv.Medecin.MedecinId))
                    {
                        if (RespectTranchDageFilters(rdv) && RespectCarteProFilters(rdv))
                            PatientsScheduleAppointmentCollection.Add(rdv);
                    }
                }
                else
                {
                    if (RespectTranchDageFilters(rdv) && RespectCarteProFilters(rdv))
                        PatientsScheduleAppointmentCollection.Add(rdv); 
                }      
            });
            await LoadRestDays();
            RestDaysCollection.ForEach(rd =>
            {                
                PatientsScheduleAppointmentCollection.Add(rd); 
            });
            RaisePropertyChanged("PatientsScheduleAppointmentCollection");
        }

        private async Task LoadRestDays()
        {
            RestDaysCollection=new ObservableCollection<JourFerie>(await Task.Run(()=>_dbContext.JourFeries));
        }

        private bool RespectTranchDageFilters(RendezVous rdv)
        {
            return TrancheDageIsChecked && (DateTime.Now - rdv.Patient.DateDeNaissance).TotalDays/365 >= AgeLowerValue &&
                   (DateTime.Now - rdv.Patient.DateDeNaissance).TotalDays / 365 <= AgeUpperValue || !TrancheDageIsChecked;
        } 
        private bool RespectCarteProFilters(RendezVous rdv)
        {
            return CarteProFilterIsEnabled && rdv.Patient.CarteProfessionel == CarteProIsChecked || !CarteProFilterIsEnabled ;
        }

        private async Task LoadScheduleSettings()
        {
            SettingsCollection = new SettingsCollection();
            await SettingsCollection.LoadSchedulerSettings();
        }


        private BrushStatus GetBrushFromSettings(DateTime dateTimeRdv, DateTime dateDeNaissance, string sexe, bool carteProfessionel)
        {
            var brushStatus = new BrushStatus();
            var ageMax = SettingsCollection["EnfantSetting"].Information;

            if (dateTimeRdv.Date < DateTime.Now.Date)
            {
                brushStatus.Brush = new SolidColorBrush(Colors.LightGray);
                if ((DateTime.Now.Year - dateDeNaissance.Year) < int.Parse(ageMax))
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
                if (ageMax != null)
                {
                    if ((DateTime.Now.Year - dateDeNaissance.Year) < int.Parse(ageMax))
                    {
                        brushStatus.Status = sexe == "Male" ? "Boy" : "Girl";
                        brushStatus.Brush =
                            new SolidColorBrush(
                                (Color)ColorConverter.ConvertFromString(SettingsCollection["EnfantSetting"].Color));
                        brushStatus.Blink = SettingsCollection["EnfantSetting"].Blink;
                    }
                    else
                    {
                        brushStatus.Status = sexe == "Male" ? "Man" : "Woman";
                        if (carteProfessionel)
                        {
                            brushStatus.Brush = new SolidColorBrush(
                                (Color)ColorConverter.ConvertFromString(SettingsCollection["ProSetting"].Color));
                            brushStatus.Blink = SettingsCollection["ProSetting"].Blink;
                        }
                        else
                        {
                            brushStatus.Brush = sexe == "Male" ? new SolidColorBrush(
                         (Color)ColorConverter.ConvertFromString(SettingsCollection["HommeSetting"].Color)) : new SolidColorBrush(
                         (Color)ColorConverter.ConvertFromString(SettingsCollection["FemmeSetting"].Color));
                            brushStatus.Blink = sexe == "Male" ? SettingsCollection["HommeSetting"].Blink : SettingsCollection["FemmeSetting"].Blink;
                        }
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
                        var d = SelectedDateInScedule.Date;
                        if (RestDaysCollection.Any(r => r.DateJourFerie == d)) return;
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
                        var result = await ((Application.Current.MainWindow as MetroWindow).ShowMessageAsync("Confirmation", "etes vous sure de vouloire faire deplacer ce rendez-vous", MessageDialogStyle.AffirmativeAndNegative));
                        if (result == MessageDialogResult.Affirmative)
                        {
                            var rdv = args.Appointment as RendezVous;
                            _dbContext.RendezVouses.Find(rdv.RendezVousId).DateTimeRdv = args.To;
                            _dbContext.SaveChanges();
                        }
                        else
                        {
                            args.Cancel = true;
                        }
                        await LoadRendezVous();
                        Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Refresh"));

                    }));
            }
        }


        //RadialGradientBrush menu commands
        private RelayCommand<object> _addAppointementFromRadialMenuCommand;
        public RelayCommand<object> AddAppointementFromRadialMenuCommand
        {
            get
            {
                return _addAppointementFromRadialMenuCommand
                    ?? (_addAppointementFromRadialMenuCommand = new RelayCommand<object>(
                    (obj) =>
                    {
                        _addAppointementView = new AddAppointementView();

                        var sfSchedule = obj as SfSchedule;
                        if (sfSchedule != null)
                        {
                            SelectedRdv = new RendezVous()
                            {
                                DateTimeRdv = SelectedDateInScedule
                            };
                            _addAppointementView.ShowDialog();
                            sfSchedule.Refresh();
                        }
                    }));
            }
        }
        private RelayCommand<object> _editAppointementFromRadialMenuCommand;
        public RelayCommand<object> EditAppointementFromRadialMenuCommand
        {
            get
            {
                return _editAppointementFromRadialMenuCommand
                    ?? (_editAppointementFromRadialMenuCommand = new RelayCommand<object>(
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
                                _addAppointementView.ShowDialog();
                                sfSchedule.Refresh();
                            }
                        }

                    }));
            }
        }

        private RendezVous _cutedAppointement = null;
        private RelayCommand<object> _cutAppointementFromRadialMenuCommand;
        public RelayCommand<object> CutAppointementFromRadialMenuCommand
        {
            get
            {
                return _cutAppointementFromRadialMenuCommand
                    ?? (_cutAppointementFromRadialMenuCommand = new RelayCommand<object>(
                    (obj) =>
                    {
                        var sfSchedule = obj as SfSchedule;
                        if (sfSchedule != null)
                        {
                            var selectedAppointement = sfSchedule.SelectedAppointment;
                            if (selectedAppointement != null)
                            {
                                _cutedAppointement = (RendezVous)selectedAppointement;
                            }
                        }

                    }));
            }
        }

        private RelayCommand<object> _pastAppointementFromRadialMenuCommand;
        public RelayCommand<object> PastAppointementFromRadialMenuCommand
        {
            get
            {
                return _pastAppointementFromRadialMenuCommand
                    ?? (_pastAppointementFromRadialMenuCommand = new RelayCommand<object>(async (obj) =>
                    {
                        var sfSchedule = obj as SfSchedule;
                        if (sfSchedule != null && _cutedAppointement != null && SelectedDateInScedule != null)
                        {
                            _dbContext.RendezVouses.Find(_cutedAppointement.RendezVousId).DateTimeRdv = SelectedDateInScedule;
                            _dbContext.SaveChanges();
                            await LoadRendezVous();
                            sfSchedule.Refresh();
                        }
                    }));
            }
        }
        private RelayCommand<object> _deleteAppointementFromRadialMenuCommand;
        public RelayCommand<object> DeleteAppointementFromRadialMenuCommand
        {
            get
            {
                return _deleteAppointementFromRadialMenuCommand
                    ?? (_deleteAppointementFromRadialMenuCommand = new RelayCommand<object>(async (obj) =>
                    {
                        var sfSchedule = obj as SfSchedule;
                        if (sfSchedule != null)
                        {
                            var selectedAppointement = sfSchedule.SelectedAppointment;
                            if (selectedAppointement != null)
                            {
                                var result = await ((Application.Current.MainWindow as MetroWindow).ShowMessageAsync("Confirmation", "etes vous sure de vouloire supprimer ce rendez-vous", MessageDialogStyle.AffirmativeAndNegative));
                                if (result == MessageDialogResult.Affirmative)
                                {
                                    _dbContext.RendezVouses.Remove((RendezVous)selectedAppointement);
                                    _dbContext.SaveChanges();
                                }
                                await LoadRendezVous();
                                sfSchedule.Refresh();
                            }
                        }
                    }));
            }
        }

        private RelayCommand _filterCalendarPerMedecinCheckedCommand;
        public RelayCommand FilterCalendarReloadCommand
        {
            get
            {
                return _filterCalendarPerMedecinCheckedCommand
                    ?? (_filterCalendarPerMedecinCheckedCommand = new RelayCommand(async () =>
                    {
                        await LoadRendezVous();
                    }));
            }
        }
        private RelayCommand _filterAgeRangeCheckedCommand;
        public RelayCommand FilterAgeRangeCheckedCommand
        {
            get
            {
                return _filterAgeRangeCheckedCommand
                    ?? (_filterAgeRangeCheckedCommand = new RelayCommand(
                    () =>
                    {
                        
                    }));
            }
        }
        private RelayCommand _filterCartePrCheckedCommand;
        public RelayCommand FilterCartePrCheckedCommand
        {
            get
            {
                return _filterCartePrCheckedCommand
                    ?? (_filterCartePrCheckedCommand = new RelayCommand(
                    () =>
                    {                        
                    }));
            }
        }
        private RelayCommand _dtrDaysSelectionChangedCommand;
        public RelayCommand DtrDaysSelectionChangedCommand
        {
            get
            {
                return _dtrDaysSelectionChangedCommand
                    ?? (_dtrDaysSelectionChangedCommand = new RelayCommand(async () =>
                    {
                        await LoadAllDoctorsList();
                    }));
            }
        }
        #region Notification Realted Commands
        private RelayCommand _searchForExistingRdvsCommand;
        public RelayCommand SearchForExistingRdvsCommand
        {
            get
            {
                return _searchForExistingRdvsCommand
                    ?? (_searchForExistingRdvsCommand = new RelayCommand(async () =>
                    {
                         if (SelectedRdv.Patient == null) return;
                        var res = await Task.Run(() =>_dbContext.RendezVouses.Where(rdv => rdv.PatientId == SelectedRdv.Patient.PatientId ));
                        if (res.Any())
                        {
                            res.ForEach(
                                rr =>
                                    NotficationManager.AddNotification(new Notification()
                                    {
                                        NotificationTitle = "#Patient déjat pris un rdv",
                                        Image = rr.Patient.ProfilePicture,
                                        NotificationMessage =
                                            "ce patient a déjat pris un rdv de radiothérapie le  : " +rr.DateTimeRdv
                                             +" effectuer une recherche pour plus de détails."
                                    }));
                        }
                    }));
            }
        }
        #endregion
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
