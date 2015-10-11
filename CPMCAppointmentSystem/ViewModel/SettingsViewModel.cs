using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CPMCAppointmentSystem.Helpers;
using CPMCAppointmentSystem.SubModel;
using DataLayer.Model;
using GalaSoft.MvvmLight.Command;
using Xceed.Wpf.Toolkit;

namespace CPMCAppointmentSystem.ViewModel
{
    public class SettingsViewModel : NavigableViewModelBase
    {
        #region Fields
        private CpmcContext _dbContext = new CpmcContext();
        private ObservableCollection<User> _usersList;
        private User _selectedUser;
        private ObservableCollection<TreeViewModel> _treeViewRollCollection = new ObservableCollection<TreeViewModel>();
        private ObservableCollection<UserTypeToAdd> _userTypeCollection;
        private ObservableCollection<PieceJointeType> _typePieceJointeCollection;
        private PieceJointeType _selectedTypePieceJointe;
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
                LoadRollCollectionForSelectedUser();
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
        public ObservableCollection<PieceJointeType> TypePieceJointeCollection
        {
            get
            {
                return _typePieceJointeCollection;
            }

            set
            {
                if (_typePieceJointeCollection == value)
                {
                    return;
                }

                _typePieceJointeCollection = value;
                RaisePropertyChanged();
            }
        }
        public PieceJointeType SelectedTypePieceJointe
        {
            get
            {
                return _selectedTypePieceJointe;
            }

            set
            {
                if (_selectedTypePieceJointe == value)
                {
                    return;
                }

                _selectedTypePieceJointe = value;
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
                        await LoadUserTypeCollection();
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
            UsersList = new ObservableCollection<User>(await Task.Run(() => _dbContext.Users));
        }

        private async Task LoadUserTypeCollection()
        {
            UserTypeCollection = new ObservableCollection<UserTypeToAdd>(await Task.Run(() => _dbContext.UserTypes.Select(x => new UserTypeToAdd()
            {
                UserTypeId = x.UserTypeId,
                UserTypeName = x.UserTypeName,
                Users = x.Users,
                IsAdded = true
            })));
        }

        private void LoadRollCollectionForSelectedUser()
        {

            if (SelectedUser != null)
            {
                TreeViewRollCollection = new ObservableCollection<TreeViewModel>()
             {
                new TreeViewModel()
                {
                    Content = "CalendarView", TreeViewModelCollection = new ObservableCollection<TreeViewModel>()
                    {
                        new TreeViewModel(){ Content = "AppointementViewAllow", IsChecked = SelectedUser.RolesCollection.AppointementViewAllow},
                        new TreeViewModel(){ Content = "AppointementEditAllow", IsChecked = SelectedUser.RolesCollection.AppointementEditAllow}
                    }
                }, 
                new TreeViewModel()
                {
                    Content = "Doctors View", TreeViewModelCollection = new ObservableCollection<TreeViewModel>()
                    {
                        new TreeViewModel(){ Content = "DoctorsViewAllow", IsChecked = SelectedUser.RolesCollection.DoctorsViewAllow},
                        new TreeViewModel(){ Content = "DoctorsAddAllow", IsChecked = SelectedUser.RolesCollection.DoctorsAddAllow}
                    }
                },
                new TreeViewModel()
                {
                    Content = "Patient View", TreeViewModelCollection = new ObservableCollection<TreeViewModel>()
                    {
                        new TreeViewModel(){ Content = "PatientsViewAllow", IsChecked = SelectedUser.RolesCollection.PatientsViewAllow},
                        new TreeViewModel(){ Content = "PatientsEditAllow", IsChecked = SelectedUser.RolesCollection.PatientsEditAllow},
                        new TreeViewModel(){ Content = "PatientsEditAppointementAllow", IsChecked = SelectedUser.RolesCollection.PatientsEditAppointementAllow}
                    }
                },
                new TreeViewModel()
                {
                    Content = "Speciality View", TreeViewModelCollection = new ObservableCollection<TreeViewModel>()
                    {
                        new TreeViewModel(){ Content = "SpecialitiesViewAllow", IsChecked = SelectedUser.RolesCollection.SpecialitiesViewAllow},
                        new TreeViewModel(){ Content = "SpecialitiesEditAllow", IsChecked = SelectedUser.RolesCollection.SpecialitiesEditAllow}
                    }
                },
                new TreeViewModel()
                {
                    Content = "Pathology View", TreeViewModelCollection = new ObservableCollection<TreeViewModel>()
                    {
                        new TreeViewModel(){ Content = "PathologiesViewAllow", IsChecked = SelectedUser.RolesCollection.PathologiesViewAllow},
                        new TreeViewModel(){ Content = "PathologiesEditAllow", IsChecked = SelectedUser.RolesCollection.PathologiesEditAllow}
                    }
                },
                new TreeViewModel()
                {
                    Content = "MyPatients View", TreeViewModelCollection = new ObservableCollection<TreeViewModel>()
                    {
                        new TreeViewModel(){ Content = "MyPatientsViewAllow", IsChecked = SelectedUser.RolesCollection.MyPatientsViewAllow},
                        new TreeViewModel(){ Content = "MyPatientsEditAllow", IsChecked = SelectedUser.RolesCollection.MyPatientsEditAllow},
                        new TreeViewModel(){ Content = "MyPatientsEditAppointementAllow", IsChecked = SelectedUser.RolesCollection.MyPatientsEditAppointementAllow}
                    }
                },
                new TreeViewModel()
                {
                    Content = "Sms Notifications View", TreeViewModelCollection = new ObservableCollection<TreeViewModel>()
                    {
                        new TreeViewModel(){ Content = "SmsNotificationViewAllow", IsChecked = SelectedUser.RolesCollection.SmsNotificationViewAllow},
                        new TreeViewModel(){ Content = "SmsNotificationEditAllow", IsChecked = SelectedUser.RolesCollection.SmsNotificationEditAllow}
                      
                    }
                },
                new TreeViewModel()
                {
                    Content = "Settings View", TreeViewModelCollection = new ObservableCollection<TreeViewModel>()
                    {
                        new TreeViewModel(){ Content = "SettingsViewUsersAllow", IsChecked = SelectedUser.RolesCollection.SettingsViewUsersAllow},
                        new TreeViewModel(){ Content = "SettingsEditUsersAllow", IsChecked = SelectedUser.RolesCollection.SettingsEditUsersAllow},
                        new TreeViewModel(){ Content = "SettingsMangeThemeAllow", IsChecked = SelectedUser.RolesCollection.SettingsMangeThemeAllow}
                    }
                }
                
            };
            }
        }
        #endregion
    }
}
