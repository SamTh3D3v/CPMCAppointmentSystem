using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using CPMCAppointmentSystem.Helpers;
using DataLayer.Model;
using GalaSoft.MvvmLight;
using CPMCAppointmentSystem.Model;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace CPMCAppointmentSystem.ViewModel
{

    public class MainWindowViewModel : NavigableViewModelBase
    {
        #region Fields
        private User _currentUser;
        private bool _isCurrentUserFlayoutOpen;
        private ObservableCollection<Notification> _notificationCollection;
        #endregion
        #region Properties
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
                        using (var context = new CpmcContext())
                        {                            
                            var res=context.Willayas.FirstOrDefault();
                        }
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
                        MainFrameNavigationService.NavigateTo(App.LoginViewKey);
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
                    }));
            }
        }
        #endregion
        #region Ctors and Methods

        #endregion       
        public MainWindowViewModel(IFrameNavigationService mainFrameNavigationService, IInnerFrameNavigationService innerFrameNavigationService)
            : base(mainFrameNavigationService,innerFrameNavigationService)
        {
            NotificationsCollection=new ObservableCollection<Notification>();
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