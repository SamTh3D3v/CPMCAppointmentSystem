using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using CPMCAppointmentSystem.Helpers;
using DataLayer.Model;
using GalaSoft.MvvmLight.Command;

namespace CPMCAppointmentSystem.ViewModel
{
    public class LoginViewModel : NavigableViewModelBase
    {
        #region Fields
        private bool _isLogInProgressRingOn;
        private readonly CpmcContext _dbContext = new CpmcContext();
        #endregion
        #region Properties
        public bool IsLogInProgressRingOn
        {
            get
            {
                return _isLogInProgressRingOn;
            }

            set
            {
                if (_isLogInProgressRingOn == value)
                {
                    return;
                }

                _isLogInProgressRingOn = value;
                RaisePropertyChanged();
            }
        }

        private String _userName;
        public String UserName
        {
            get
            {
                return _userName;
            }

            set
            {
                if (_userName == value)
                {
                    return;
                }

                _userName = value;
                RaisePropertyChanged();
            }
        }

        #endregion
        #region Commands
        private RelayCommand _loginViewLoadedCommand;
        public RelayCommand LoginViewLoadedCommand
        {
            get
            {
                return _loginViewLoadedCommand
                    ?? (_loginViewLoadedCommand = new RelayCommand(
                    () =>
                    {
                    }));
            }
        }
        private RelayCommand _loginViewUnLoadedCommand;
        public RelayCommand LoginViewUnLoadedCommand
        {
            get
            {
                return _loginViewUnLoadedCommand
                    ?? (_loginViewUnLoadedCommand = new RelayCommand(
                    () =>
                    {
                        //_dbContext.Dispose();                        
                    }));
            }
        }
        private RelayCommand<object> _loginCommand;
        public RelayCommand<object> LoginCommand
        {
            get
            {
                return _loginCommand
                    ?? (_loginCommand = new RelayCommand<object>(async (pass) =>
                    {
                        if (pass == null) return;
                        await Task.Run(() =>
                        {
                            IsLogInProgressRingOn = true;
                            Login(UserName, pass);
                            IsLogInProgressRingOn = false;
                        });

                    }));
            }
        }

        #endregion
        #region Ctors and Methods

        public LoginViewModel(IFrameNavigationService mainNavigationService, IInnerFrameNavigationService innerNavigationService)
            : base(mainNavigationService, innerNavigationService)
        {
        }

        void Login(string userName, object pass)
        {
            var passwordBox = pass as PasswordBox;
            if (passwordBox != null)
            {
                var passwrd = passwordBox.Password;
                //Use the autentification service 
                if (_dbContext.Users.Any(u => u.UserName == userName && u.UserPass == passwrd))
                {
                    System.Threading.Thread.CurrentPrincipal = new CPMCAppointmentSystem.Helpers.CustomPrincipal(new CustomIdentity(true, userName));
                    Application.Current.Dispatcher.BeginInvoke(new Action(async () =>
                    {
                        MainFrameNavigationService.NavigateTo(App.MainViewKey,
                            _dbContext.Users.First(u => u.UserName == userName));
                    }));

                }
                else
                {
                    UserName = "";
                    Application.Current.Dispatcher.BeginInvoke(new Action(async () =>
                    {
                        passwordBox.Clear();
                    }));
                }
            }
        }

        #endregion
    }
}
