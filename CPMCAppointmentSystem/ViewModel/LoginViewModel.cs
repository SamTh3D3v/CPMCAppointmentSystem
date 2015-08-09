using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using CPMCAppointmentSystem.Helpers;
using GalaSoft.MvvmLight.Command;

namespace CPMCAppointmentSystem.ViewModel
{
    public class LoginViewModel:NavigableViewModelBase
    {
        #region Fields
        
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
                            Login(UserName, pass);
                        }
                        
                    }));
            }
        }
        
        #endregion
        #region Ctors and Methods

        public LoginViewModel(IFrameNavigationService mainNavigationService,IFrameNavigationService innerNavigationService)
            : base(mainNavigationService, innerNavigationService)
        {
            
        }

        void Login(string userName,object passwordBox)
        {
            var pass=(passwordBox as PasswordBox).Password;
            MainFrameNavigationService.NavigateTo(App.MainViewKey);

        }
        
        #endregion        
    }
}
