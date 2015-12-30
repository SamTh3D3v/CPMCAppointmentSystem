using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Diagnostics;
using System.Globalization;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using CPMCAppointmentSystem.Helpers;
using DataLayer;
using DataLayer.Model;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using GsmManager;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using GsmHelper = CPMCAppointmentSystem.Helpers.GsmHelper;

namespace CPMCAppointmentSystem.ViewModel
{
    public class NotificationViewModel : NavigableViewModelBase
    {
        #region Fields
        private GsmConnection _gsmConnection;
        private bool _allDataLoaded = false;
        private bool _stillInView;
        private DateTime _selectedDateDepo = DateTime.Now;
        private bool _isSimActive;
        private GsmHelper _gsmHelper;
        private CpmcContext _dbContext = new CpmcContext();
        private ObservableCollection<RendezVous> _rdvCollectionList;
        private RendezVous _selectedRdv;
        private bool _isFilterCheckActivated;
        private bool _isProgressRingActive;
        #endregion
        #region Properties
        public GsmConnection GsmConnection
        {
            get
            {
                return _gsmConnection;
            }

            set
            {
                if (_gsmConnection == value)
                {
                    return;
                }

                _gsmConnection = value;
                RaisePropertyChanged();
            }
        }
        public ConnectionSettings ConnectionSettings
        {
            get
            {
                return _connectionSettings;
            }

            set
            {
                if (_connectionSettings == value)
                {
                    return;
                }

                _connectionSettings = value;
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
        public DateTime SelectedDateDepo
        {
            get
            {
                return _selectedDateDepo; ;
            }

            set
            {
                if (_selectedDateDepo == value)
                {
                    return;
                }

                _selectedDateDepo = value;
                RaisePropertyChanged();
            }
        }

        public bool IsFilterCheckActivated
        {
            get
            {
                return _isFilterCheckActivated;
            }

            set
            {
                if (_isFilterCheckActivated == value)
                {
                    return;
                }

                _isFilterCheckActivated = value;
                RaisePropertyChanged();
            }
        }
        public bool IsSimActive
        {
            get
            {
                return _isSimActive;
            }

            set
            {
                if (_isSimActive == value)
                {
                    return;
                }

                _isSimActive = value;
                RaisePropertyChanged();
            }
        }
        public GsmHelper GsmHelper
        {
            get
            {
                return _gsmHelper;
            }

            set
            {
                if (_gsmHelper == value)
                {
                    return;
                }

                _gsmHelper = value;
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
        public ObservableCollection<RendezVous> RdvCollectionList
        {
            get
            {
                return _rdvCollectionList;
            }

            set
            {
                if (_rdvCollectionList == value)
                {
                    return;
                }

                _rdvCollectionList = value;
                RaisePropertyChanged();
            }
        }
        public String SmsMessageTemplate { get; set; }
        #endregion
        #region Commands
        private ConnectionSettings _connectionSettings;
        private RelayCommand _refreshGsmDeviceCommand;
        public RelayCommand RefreshGsmDeviceCommand
        {
            get
            {
                return _refreshGsmDeviceCommand
                    ?? (_refreshGsmDeviceCommand = new RelayCommand(async () =>
                    {
                        _stillInView = true;
                        _allDataLoaded = false;
                        try
                        {
                            IsSimActive = false;
                            IsProgressRingActive = true;
                            Messenger.Default.Send<String>("détection du Sim en cours", "enableLoading");

                            //GsmHelper = new GsmHelper(9600);
                            //SmsMessageTemplate = ParameterManager.GetValue<string>(ParameterNames.SMSBodyTemplate);
                            //await GsmHelper.InitGsmDevice();
                            await AutoConnectToGsmDevice();

                            IsSimActive = true;
                            IsProgressRingActive = false;
                            Messenger.Default.Send<String>("", "desableLoading");
                        }
                        catch (Exception ex)
                        {
                            if (_stillInView)
                                Application.Current.Dispatcher.BeginInvoke(new Action(async () =>
                                {
                                    var exceptionDialog = await ((Application.Current.MainWindow as MetroWindow).ShowMessageAsync("Echec de COM", "check the gsm device ... "));
                                }));
                            IsSimActive = false;
                            IsProgressRingActive = false;
                            Messenger.Default.Send<String>("Sim non reconnue", "desableLoading");
                        }
                        _allDataLoaded = true;
                    }));
            }
        }
        private RelayCommand _notificationViewLoadedCommand;
        public RelayCommand NotificationViewLoadedCommand
        {
            get
            {
                return _notificationViewLoadedCommand
                    ?? (_notificationViewLoadedCommand = new RelayCommand(async () =>
                    {
                        _allDataLoaded = false;
                        _stillInView = true;
                        _dbContext = new CpmcContext();
                        await LoadRdvs();
                        try
                        {
                            IsSimActive = false;
                            IsProgressRingActive = true;
                            Messenger.Default.Send<String>("détection du Sim en cours", "enableLoading");




                            //GsmHelper = new GsmHelper(9600);
                            SmsMessageTemplate = ParameterManager.GetValue<string>(ParameterNames.SMSBodyTemplate);
                            //await GsmHelper.InitGsmDevice();
                            await AutoConnectToGsmDevice();



                            IsSimActive = true;
                            IsProgressRingActive = false;
                            Messenger.Default.Send<String>("", "desableLoading");
                        }
                        catch (Exception ex)
                        {
                            if (_stillInView)
                                Application.Current.Dispatcher.BeginInvoke(new Action(async () =>
                                {
                                    var exceptionDialog = await ((Application.Current.MainWindow as MetroWindow).ShowMessageAsync("Echec de COM", "check the gsm device ... "));
                                }));
                            IsSimActive = false;
                            IsProgressRingActive = false;
                            Messenger.Default.Send<String>("Sim non reconnue", "desableLoading");
                        }
                        _allDataLoaded = true;
                    }));
            }
        }

        private async Task AutoConnectToGsmDevice()
        {
                        
            await Task.Run(() =>
            {
                ConnectionSettings = new ConnectionSettings
                {
                    BaudRate = 9600,
                    TimeOut = 300
                };
                if (!GsmManager.GsmHelper.GetAvailablePortNamesInDevice().Any())
                {
                    throw new Exception("\n No availale ports, connection failed");
                }
                foreach (var port in GsmManager.GsmHelper.GetAvailablePortNamesInDevice())
                {
                    ConnectionSettings.PortName = port;

                    try
                    {
                        if (!GsmManager.GsmHelper.TestConnection(ConnectionSettings))
                        {
                            ConnectionSettings.PortName = "";
                        }
                        else
                        {
                            break;
                        }
                    }
                    catch (Exception)
                    {

                        ConnectionSettings.PortName = "";
                    }
                }
                if (ConnectionSettings.PortName != "")
                {
                    //All set succesfully                            
                    GsmConnection = new GsmConnection(ConnectionSettings);
                }
                else
                {
                    throw new Exception("\n Can't detect the Gsm device");
                }
            }).ContinueWith((d)=>d.Dispose());
            
        }


        private RelayCommand _notificationViewUnLoadedCommand;
        public RelayCommand NotificationViewUnLoadedCommand
        {
            get
            {
                return _notificationViewUnLoadedCommand
                    ?? (_notificationViewUnLoadedCommand = new RelayCommand(
                    () =>
                    {
                        _dbContext.SaveChanges();
                        Task.Run(() =>
                        {
                            while (!_allDataLoaded) { }
                            _dbContext.Dispose();

                        });
                        _stillInView = false;
                        IsProgressRingActive = false;

                    }));
            }
        }
        private RelayCommand _callPhoneCommand;
        public RelayCommand CallPhoneCommand
        {
            get
            {
                return _callPhoneCommand
                    ?? (_callPhoneCommand = new RelayCommand(
                    () =>
                    {
                        if (SelectedRdv != null)
                        {
                            GsmHelper.Callphone("+" + SelectedRdv.Patient.TelephoneMobile1);
                        }

                    }));
            }
        }
        private RelayCommand _sendsmsCommand;
        public RelayCommand SendSmsCommand
        {
            get
            {
                return _sendsmsCommand
                    ?? (_sendsmsCommand = new RelayCommand(
                    () =>
                    {
                        GsmHelper.SendSms("+" + SelectedRdv.Patient.TelephoneMobile1, ApplySmsTemplateToSelectedRdv());
                    }));
            }
        }

        private string ApplySmsTemplateToSelectedRdv()
        {
            return SmsMessageTemplate.Replace(App.DpNomPatientId, SelectedRdv.Patient.Nom).Replace(App.DpPrenomPatientId, SelectedRdv.Patient.Prenom).
                Replace(App.DpNomMedecinId, SelectedRdv.Medecin.User.UserNom).Replace(App.DpPrenomMedecinId, SelectedRdv.Medecin.User.UserPrenom).Replace(App.DpDateRdvId, SelectedRdv.DateTimeRdv.Date.ToString("dd/MM/yyyy"))
                .Replace(App.DpLieuRdvId, SelectedRdv.LieuRdv);
        }

        private RelayCommand _callFixCommand;
        public RelayCommand CallFixCommand
        {
            get
            {
                return _callFixCommand
                    ?? (_callFixCommand = new RelayCommand(
                    () =>
                    {
                        GsmHelper.Callphone("+" + SelectedRdv.Patient.TelephoneFixe);
                    }));
            }
        }

        private async Task LoadRdvs()
        {
            var date = SelectedDateDepo.Date;
            RdvCollectionList = IsFilterCheckActivated ? new ObservableCollection<RendezVous>(await Task.Run(() => _dbContext.RendezVouses.Where(rdv => DbFunctions.TruncateTime(rdv.DateTimeRdv) == date))) : new ObservableCollection<RendezVous>(await Task.Run(() => _dbContext.RendezVouses));
        }
        private RelayCommand _reloadRdvsCommand;
        public RelayCommand ReloadRdvsCommand
        {
            get
            {
                return _reloadRdvsCommand
                    ?? (_reloadRdvsCommand = new RelayCommand(async () =>
                    {
                        await LoadRdvs();
                    }));
            }
        }

        #endregion
        #region Ctors and methods
        public NotificationViewModel(IFrameNavigationService mainFrameNavigationService, IInnerFrameNavigationService innerFrameNavigationService)
            : base(mainFrameNavigationService, innerFrameNavigationService)
        {
            Messenger.Default.Register<NotificationMessage>(this, (m) =>
            {
                try
                {
                    switch (m.Notification)
                    {
                        case "SendSmsToPatient":
                            GsmHelper.SendSms("+" + SelectedRdv.Patient.TelephoneMobile1, ApplySmsTemplateToSelectedRdv());
                            SelectedRdv.NotificationSent = true;
                            break;
                        case "SendSmsToAccom":
                            GsmHelper.SendSms("+" + SelectedRdv.Patient.TelephoneDaccompagnant, ApplySmsTemplateToSelectedRdv());
                            SelectedRdv.NotificationSent = true;
                            break;
                        case "CallPatient":
                            GsmHelper.Callphone(SelectedRdv.Patient.TelephoneMobile1);
                            break;
                        case "CallAccompagnant":
                            GsmHelper.Callphone(SelectedRdv.Patient.TelephoneDaccompagnant);
                            break;
                        case "CallFix":
                            GsmHelper.Callphone(SelectedRdv.Patient.TelephoneFixe);
                            break;

                    }
                }
                catch (Exception ex)
                {

                    Application.Current.Dispatcher.BeginInvoke(new Action(async () =>
                    {
                        var ctontroller = await ((Application.Current.MainWindow as MetroWindow).ShowMessageAsync(
                            "Echec de com", ex.Message));
                    }));
                }
            });
        }
        #endregion
    }
}
