using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using CPMCAppointmentSystem.Helpers;
using CPMCAppointmentSystem.SubModel;
using DataLayer.Model;
using DataLayer.Notifications;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace CPMCAppointmentSystem.ViewModel
{

    public class MainWindowViewModel : NavigableViewModelBase
    {
        #region Fields
        private DataBaseSettings _dataBaseSettings;
        private User _currentUser;
        private bool _isCurrentUserFlayoutOpen;
        private ObservableCollection<Notification> _notificationCollection;
        private bool _isNotificationFlayoutOpen;
        #endregion
        #region Properties         
        public DataBaseSettings DataBaseSttings
        {
            get
            {
                return _dataBaseSettings;
            }

            set
            {
                if (_dataBaseSettings == value)
                {
                    return;
                }

                _dataBaseSettings = value;
                RaisePropertyChanged();
            }
        }
        public bool IsNotificationFlayoutOpen
        {
            get
            {
                return _isNotificationFlayoutOpen;
            }

            set
            {
                if (_isNotificationFlayoutOpen == value)
                {
                    return;
                }

                _isNotificationFlayoutOpen = value;
                if(_isNotificationFlayoutOpen)
                    Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Seen"));
                RaisePropertyChanged();
            }
        }
        public ObservableCollection<Notification> NotificationsCollection
        {
            get
            {
                return _notificationCollection;
            }

            set
            {
                if (_notificationCollection == value)
                {
                    return;
                }

                _notificationCollection = value;
                RaisePropertyChanged();
            }
        }
        public User CurrentUser
        {
            get
            {
                return _currentUser;
            }

            set
            {
                if (_currentUser == value)
                {
                    return;
                }

                _currentUser = value;
                RaisePropertyChanged();
            }
        }
        public bool IsCurrentUserFlayoutOpen
        {
            get
            {
                return _isCurrentUserFlayoutOpen;
            }

            set
            {
                if (_isCurrentUserFlayoutOpen == value)
                {
                    return;
                }

                _isCurrentUserFlayoutOpen = value;
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
                        MainFrameNavigationService.NavigateTo(App.LoginViewKey);
                        DataBaseSttings = GetDataBaseSettings();

                    }));
            }
        }

        private DataBaseSettings GetDataBaseSettings()
        {
            //get DataBase Connexion Setting from the app.config using the app.xaml.cs
            return new DataBaseSettings();            
        }
        private RelayCommand<object> _saveDataBaseSettingsCommand;
        public RelayCommand<object> SaveDataBaseSettingsCommand
        {
            get
            {
                return _saveDataBaseSettingsCommand
                    ?? (_saveDataBaseSettingsCommand = new RelayCommand<object>(
                    (passBox) =>
                    {
                        if (passBox==null) return;                      
                        App.SaveDateBaseSettings(DataBaseSttings, passBox);
                        System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
                        Application.Current.Shutdown();
                        //Disconnect 
                        
                    }));
            }
        }
        private RelayCommand _cancelSaveDataBaseSettingsCommand;
        public RelayCommand CancelSaveDataBaseSettingsCommand
        {
            get
            {
                return _cancelSaveDataBaseSettingsCommand
                    ?? (_cancelSaveDataBaseSettingsCommand = new RelayCommand(
                    () =>
                    {
                        DataBaseSttings=GetDataBaseSettings();
                    }));
            }
        }
        private RelayCommand _restoreDataBaseSettingsCommand;
        public RelayCommand RestoreDataBaseSettingsCommand
        {
            get
            {
                return _restoreDataBaseSettingsCommand
                    ?? (_restoreDataBaseSettingsCommand = new RelayCommand(
                    () =>
                    {
                        DataBaseSttings = GetDefaultDataBaseSettings();
                        //Disconnect then reconnect
                    }));
            }
        }

        private DataBaseSettings GetDefaultDataBaseSettings()
        {
            //get the default settings from the settings xml file
            return new DataBaseSettings();
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
        private RelayCommand _logOutCommand;
        public RelayCommand LogOutCommand
        {
            get
            {
                return _logOutCommand
                    ?? (_logOutCommand = new RelayCommand(
                    () =>
                    {
                        IsCurrentUserFlayoutOpen = false;
                        CurrentUser = null;
                        MainFrameNavigationService.NavigateTo(App.LoginViewKey);
                        App.NotificationHelper.NotificationsChange -= NotificationHelper_NotificationsChange; 
                        App.NotificationHelper.Stop();
                        System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
                        Application.Current.Shutdown();
                    }));
            }
        }
        private RelayCommand _userConnectedCommand;
        public RelayCommand UserConnectedCommand
        {
            get
            {
                return _userConnectedCommand
                    ?? (_userConnectedCommand = new RelayCommand(
                    () =>
                    {
                        CurrentUser = MainFrameNavigationService.Parameter as User;
                        if (CurrentUser != null)
                        {
                            App.NotificationHelper.NotificationsChange += NotificationHelper_NotificationsChange; 
                            App.NotificationHelper.Start(CurrentUser.UserId);                            
                        }
                    }));
            }
        }
        void NotificationHelper_NotificationsChange(object sender, DataLayer.Notifications.NotificationEventArgs<DataLayer.Model.Notification> args)
        {
            //Get Valide notifications
            var notifications = args.NewResult;
            Application.Current.Dispatcher.BeginInvoke(new Action(() => NotificationsCollection = new ObservableCollection<Notification>(notifications)));

            // MessageBox.Show(notifications.Count.ToString(), notifications.Count > 0 ? notifications[0].NotificationTitle : "");
        }
        #endregion
        #region Ctors and Methods

        #endregion
        public MainWindowViewModel(IFrameNavigationService mainFrameNavigationService, IInnerFrameNavigationService innerFrameNavigationService)
            : base(mainFrameNavigationService, innerFrameNavigationService)
        {
            NotificationsCollection = new ObservableCollection<Notification>();
            Messenger.Default.Register<Notification>(this, "AddNotification", (notification) => NotificationsCollection.Add(notification));
            Messenger.Default.Register<Notification>(this, "RemoveNotification", (notification) => NotificationsCollection.Remove(notification));

        }
        public override void Cleanup()
        {
            // Clean up if needed

            base.Cleanup();
        }
    }
}