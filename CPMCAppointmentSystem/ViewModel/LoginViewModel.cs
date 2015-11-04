using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using CPMCAppointmentSystem.Helpers;
using DataLayer.Model;
using GalaSoft.MvvmLight.Command;

namespace CPMCAppointmentSystem.ViewModel
{
    public class LoginViewModel:NavigableViewModelBase
    {
        #region Fields
        private readonly CpmcContext _dbContext=new CpmcContext();        
        #endregion
        #region Properties        
        
       private String _userName  ;
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
                    ?? (_loginCommand = new RelayCommand<object>(
                    (pass) =>
                    {
                        if (pass != null)
                        {                            
                            if (pass != null) Login(UserName, pass);
                        }
                    }));
            }
        }
        
        #endregion
        #region Ctors and Methods

        public LoginViewModel(IFrameNavigationService mainNavigationService, IInnerFrameNavigationService innerNavigationService)
            : base(mainNavigationService, innerNavigationService)
        {            
        }

        void Login(string userName,object pass)
        {
            var passwordBox = pass as PasswordBox;
            if (passwordBox != null)
            {
                var passwrd = passwordBox.Password;
                //Use the autentification service 
                if (_dbContext.Users.Any(u => u.UserName == userName && u.UserPass == passwrd))
                {
                    System.Threading.Thread.CurrentPrincipal = new CPMCAppointmentSystem.Helpers.CustomPrincipal(new CustomIdentity(true, userName));

                    MainFrameNavigationService.NavigateTo(App.MainViewKey, _dbContext.Users.First(u => u.UserName == userName));                
                }
                else
                {
                    UserName = "";
                    passwordBox.Clear();
                
                }
            }
        }

        #endregion        
    }
}
