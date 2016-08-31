using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CPMCAppointmentSystem.Helpers;
using DataLayer.Model;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace CPMCAppointmentSystem.ViewModel
{
    public class MainViewModel : NavigableViewModelBase
    {
        #region Fields
        private RolesCollection _connectedUserRollsCollection;
        private CpmcContext _dbContext = new CpmcContext();
        private User _connectedUser;
        private bool _unseenNotification;
        #endregion
        #region Properties
        public bool UnseenNotification
        {
            get
            {
                return _unseenNotification;
            }

            set
            {
                if (_unseenNotification == value)
                {
                    return;
                }

                _unseenNotification = value;
                RaisePropertyChanged();
            }
        }
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

        public RolesCollection ConnectedUserRollsCollection
        {
            get
            {
                return _connectedUserRollsCollection;
            }

            set
            {
                if (_connectedUserRollsCollection == value)
                {
                    return;
                }

                _connectedUserRollsCollection = value;
                RaisePropertyChanged();
            }
        }

        #endregion
        #region Commands
        private RelayCommand _mainViewLoadedCommand;
        public RelayCommand MainViewLoadedCommand
        {
            get
            {
                return _mainViewLoadedCommand
                    ?? (_mainViewLoadedCommand = new RelayCommand(
                        () =>
                        {
                            _dbContext = new CpmcContext();
                            var user = MainFrameNavigationService.Parameter as User;
                            if (user == null) return;                          //todo 
                            ConnectedUser = _dbContext.Users.Find(user.UserId);
                            IEnumerable<RolesCollection> rols = ConnectedUser.UserTypes.AsEnumerable().Select(u => u.RolesCollection).AsEnumerable();
                            ConnectedUserRollsCollection = RollsCollectionHelper.MergeRolls(rols);

                            NavigateToAnAllowedView();
                            App.NotificationHelper.NotificationsChange += (s, e) =>
                            {
                                UnseenNotification = true;
                            };
                        }));
            }
        }

        private void NavigateToAnAllowedView()
        {
            if (ConnectedUserRollsCollection.PatientsViewAllow) InnerFrameNavigationService.NavigateTo(App.PatientsViewKey);
            else if (ConnectedUserRollsCollection.AppointementViewAllow) InnerFrameNavigationService.NavigateTo(App.CalendarViewKey);
            else if (ConnectedUserRollsCollection.DoctorsViewAllow) InnerFrameNavigationService.NavigateTo(App.DoctorsViewKey);
            else if (ConnectedUserRollsCollection.PathologiesViewAllow) InnerFrameNavigationService.NavigateTo(App.PathologiesViewKey);
            else if (ConnectedUserRollsCollection.SpecialitiesViewAllow) InnerFrameNavigationService.NavigateTo(App.SpecialityViewKey);
            else if (ConnectedUserRollsCollection.SmsNotificationViewAllow) InnerFrameNavigationService.NavigateTo(App.NotificationViewKey);
            else if (ConnectedUserRollsCollection.StatisticsViewAllow) InnerFrameNavigationService.NavigateTo(App.StatisticsViewKey);
            else if (ConnectedUserRollsCollection.LogViewAllow) InnerFrameNavigationService.NavigateTo(App.LogViewKey);
            else if (ConnectedUserRollsCollection.AccountSettingsEditAllow) InnerFrameNavigationService.NavigateTo(App.SettingsViewKey);

        }
        private RelayCommand _mainViewUnloadedCommand;
        public RelayCommand MainViewUnloadedCommand
        {
            get
            {
                return _mainViewUnloadedCommand
                    ?? (_mainViewUnloadedCommand = new RelayCommand(
                    () =>
                    {

                    }));
            }
        }
        private RelayCommand _calendarCommand;
        public RelayCommand CalendarCommand
        {
            get
            {
                return _calendarCommand
                    ?? (_calendarCommand = new RelayCommand(
                    () => InnerFrameNavigationService.NavigateTo(App.CalendarViewKey)));
            }
        }
        private RelayCommand _patientsCommand;
        public RelayCommand PatientsCommand
        {
            get
            {
                return _patientsCommand
                    ?? (_patientsCommand = new RelayCommand(
                    () => InnerFrameNavigationService.NavigateTo(App.PatientsViewKey)));
            }
        }
        private RelayCommand _myPatientsCommand;
        public RelayCommand MyPatientsCommand
        {
            get
            {
                return _myPatientsCommand
                    ?? (_myPatientsCommand = new RelayCommand(
                    () => InnerFrameNavigationService.NavigateTo(App.MyPatientsViewKey)));
            }
        }
        private RelayCommand _doctorsCommand;
        public RelayCommand DoctorsCommand
        {
            get
            {
                return _doctorsCommand
                    ?? (_doctorsCommand = new RelayCommand(
                    () => InnerFrameNavigationService.NavigateTo(App.DoctorsViewKey)));
            }
        }
        private RelayCommand _specialitiesCommand;
        public RelayCommand SpecialitiesCommand
        {
            get
            {
                return _specialitiesCommand
                    ?? (_specialitiesCommand = new RelayCommand(
                    () => InnerFrameNavigationService.NavigateTo(App.SpecialityViewKey)));
            }
        }
        private RelayCommand _settingsCommand;
        public RelayCommand SettingsCommand
        {
            get
            {
                return _settingsCommand
                    ?? (_settingsCommand = new RelayCommand(
                    () => InnerFrameNavigationService.NavigateTo(App.SettingsViewKey)));
            }
        }
        private RelayCommand _pathologiesCommand;
        public RelayCommand PathologiesCommand
        {
            get
            {
                return _pathologiesCommand
                    ?? (_pathologiesCommand = new RelayCommand(
                    () => InnerFrameNavigationService.NavigateTo(App.PathologiesViewKey)));
            }
        }
        private RelayCommand _statisticsCommand;
        public RelayCommand StatisticsCommand
        {
            get
            {
                return _statisticsCommand
                    ?? (_statisticsCommand = new RelayCommand(
                    () => InnerFrameNavigationService.NavigateTo(App.StatisticsViewKey)));
            }
        }
        private RelayCommand _notificationsCommand;
        public RelayCommand NotificationsCommand
        {
            get
            {
                return _notificationsCommand
                    ?? (_notificationsCommand = new RelayCommand(
                    () => InnerFrameNavigationService.NavigateTo(App.NotificationViewKey)));
            }
        }
        private RelayCommand _centrageCommand;
        public RelayCommand CentrageCommand
        {
            get
            {
                return _centrageCommand
                    ?? (_centrageCommand = new RelayCommand(
                    () => InnerFrameNavigationService.NavigateTo(App.CentrageViewKey)));
            }
        }
        private RelayCommand _rcpCommand;
        public RelayCommand RcpCommand
        {
            get
            {
                return _rcpCommand
                    ?? (_rcpCommand = new RelayCommand(
                    () => InnerFrameNavigationService.NavigateTo(App.RcpViewKey)));
            }
        }
        private RelayCommand _logCommand;
        public RelayCommand LogCommand
        {
            get
            {
                return _logCommand
                    ?? (_logCommand = new RelayCommand(
                    () => InnerFrameNavigationService.NavigateTo(App.LogViewKey)));
            }
        }
        private RelayCommand _showNotificationFlayoutCommand;
        public RelayCommand ShowNotificationFlayoutCommand
        {
            get
            {
                return _showNotificationFlayoutCommand
                    ?? (_showNotificationFlayoutCommand = new RelayCommand(
                    () => Messenger.Default.Send<NotificationMessage>(new NotificationMessage("OpenNotificationFlayout"))));
            }
        }
        private RelayCommand _showCurrentUserFlayoutCommand;
        public RelayCommand ShowCurrentUserFlayoutCommand
        {
            get
            {
                return _showCurrentUserFlayoutCommand
                    ?? (_showCurrentUserFlayoutCommand = new RelayCommand(
                    () => Messenger.Default.Send<NotificationMessage>(new NotificationMessage("ShowCurrentUserFlayout"))));
            }
        }

        #endregion
        #region Ctors and Methods
        public MainViewModel(IFrameNavigationService mainFrameNavigationService, IInnerFrameNavigationService innerFrameNavigationService)
            : base(mainFrameNavigationService, innerFrameNavigationService)
        {
            Messenger.Default.Register<NotificationMessage>(this, (m) =>
            {
                switch (m.Notification)
                {
                    case "Seen":
                        UnseenNotification = false;
                        break;
                }
            });
        }
        #endregion
    }
}
