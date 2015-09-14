using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CPMCAppointmentSystem.Helpers;
using DataLayer.Model;
using GalaSoft.MvvmLight.Command;

namespace CPMCAppointmentSystem.ViewModel
{
    public class SettingsViewModel:NavigableViewModelBase
    {
        #region Fields
        private CpmcContext _dbContext=new CpmcContext();
        private ObservableCollection<User> _usersList;
        private User _selectedUser;
        #endregion
        #region Properties                
        public ObservableCollection<User> UsersList
        {
            get
            {
                return _usersList;
            }

            set
            {
                if (_usersList == value)
                {
                    return;
                }

                _usersList = value;
                RaisePropertyChanged();
            }
        }
        public User SelectedUser
        {
            get
            {
                return _selectedUser;
            }

            set
            {
                if (_selectedUser == value)
                {
                    return;
                }

                _selectedUser = value;
                RaisePropertyChanged();
            }
        }
        #endregion
        #region Commands
        private RelayCommand _settingsViewLoadedCommand;
        public RelayCommand SettingsViewLoadedCommand
        {
            get
            {
                return _settingsViewLoadedCommand
                    ?? (_settingsViewLoadedCommand = new RelayCommand(
                    () =>
                    {
                        LoadSettingsRelatedData();

                    }));
            }
        }
        
        #endregion
        #region Ctors and Methods
        public SettingsViewModel(IFrameNavigationService mainFrameNavigationService, IInnerFrameNavigationService innerFrameNavigationService)
            : base(mainFrameNavigationService, innerFrameNavigationService)
        {
        }

        private async Task LoadSettingsRelatedData()
        {
           UsersList=new ObservableCollection<User>(await Task.Run(()=>_dbContext.Users));
            
        }
        #endregion        
    }
}
