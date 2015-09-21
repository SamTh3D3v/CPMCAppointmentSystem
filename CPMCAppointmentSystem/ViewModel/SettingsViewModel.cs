using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CPMCAppointmentSystem.Helpers;
using CPMCAppointmentSystem.SubModel;
using DataLayer.Model;
using GalaSoft.MvvmLight.Command;

namespace CPMCAppointmentSystem.ViewModel
{
    public class SettingsViewModel:NavigableViewModelBase
    {
        #region Fields
        private CpmcContext _dbContext=new CpmcContext();       
        private ObservableCollection<User> _usersList  ;        
        private User _selectedUser  ;       
        private ObservableCollection<TreeViewModel> _treeViewRollCollection;
        private ObservableCollection<UserTypeToAdd> _userTypeCollection;   
       
        #endregion
        #region Properties   
        public ObservableCollection<UserTypeToAdd> UserTypeCollection
        {
            get
            {
                return _userTypeCollection;
            }

            set
            {
                if (_userTypeCollection == value)
                {
                    return;
                }

                _userTypeCollection = value;
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
        public ObservableCollection<TreeViewModel> TreeViewRollCollection
        {
            get
            {
                return _treeViewRollCollection;
            }

            set
            {
                if (_treeViewRollCollection == value)
                {
                    return;
                }

                _treeViewRollCollection = value;
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
                    ?? (_settingsViewLoadedCommand = new RelayCommand(async () =>
                    {
                        await LoadUserTaskCollection();
                        await LoadUsersList();                       
                    }));
            }
        }
        
        #endregion
        #region Ctors and Methods
        public SettingsViewModel(IFrameNavigationService mainFrameNavigationService, IInnerFrameNavigationService innerFrameNavigationService)
            : base(mainFrameNavigationService, innerFrameNavigationService)
        {
        }

        private async Task LoadUsersList()
        {
           UsersList=new ObservableCollection<User>(await Task.Run(()=>_dbContext.Users));
            
        }

        private async Task LoadUserTaskCollection()
        {
            UserTypeCollection = new ObservableCollection<UserTypeToAdd>(await Task.Run(() => _dbContext.UserTypes.Select(x=>new UserTypeToAdd()
            {
                UserTypeId=x.UserTypeId,
                UserTypeName=x.UserTypeName,
                Users = x.Users,
                IsAdded = true
            })));
        }
        #endregion        
    }
}
