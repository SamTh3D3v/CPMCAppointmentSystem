using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CPMCAppointmentSystem.Helpers;
using CPMCAppointmentSystem.SubModel;
using CPMCAppointmentSystem.View.SettingsViews;
using DataLayer.Model;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Syncfusion.Data.Extensions;
using Xceed.Wpf.Toolkit;

namespace CPMCAppointmentSystem.ViewModel
{
    public class SettingsViewModel : NavigableViewModelBase
    {
        #region Fields
        private readonly CpmcContext _dbContext = new CpmcContext();
        private ObservableCollection<User> _usersList;
        private User _selectedUser;
        private ObservableCollection<TreeViewModel> _treeViewRollCollection = new ObservableCollection<TreeViewModel>();
        private ObservableCollection<UserTypeToAdd> _userTypeCollection;
        private ObservableCollection<PieceJointeType> _typePieceJointeCollection;
        private PieceJointeType _selectedTypePieceJointe;
        private string _reportPath;
        #endregion
        #region Properties
        public string ReportPath
        {
            get
            {
                return _reportPath;
            }

            set
            {
                if (_reportPath == value)
                {
                    return;
                }

                _reportPath = value;
                RaisePropertyChanged();
            }
        }
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
        private RelayCommand _typePieceJointeDataGridLoadedCommand;
        public RelayCommand TypePieceJointeDataGridLoadedCommand
        {
            get
            {
                return _typePieceJointeDataGridLoadedCommand
                    ?? (_typePieceJointeDataGridLoadedCommand = new RelayCommand(async () =>
                    {
                        await LoadTypePieceJointsCollection();

                    }));
            }
        }

        private async Task LoadTypePieceJointsCollection()
        {
            TypePieceJointeCollection = new ObservableCollection<PieceJointeType>(await Task.Run(() => _dbContext.PieceJointeTypes));
        }
        private RelayCommand _saveAddTypePieceJointCommand;
        public RelayCommand SaveAddTypePieceJointsCommand
        {
            get
            {
                return _saveAddTypePieceJointCommand
                    ?? (_saveAddTypePieceJointCommand = new RelayCommand(
                    () =>
                    {
                        foreach (var pieceJointeType in TypePieceJointeCollection)
                        {
                            if (pieceJointeType.PieceJointeTypeId == null || pieceJointeType.PieceJointeTypeId == Guid.Empty)
                            {
                                _dbContext.PieceJointeTypes.Add(pieceJointeType);
                            }
                        }
                        _dbContext.SaveChanges();

                    }));
            }
        }
        private RelayCommand _cancelAddTypePieceJointeCommand;
        public RelayCommand CancelAddTypePieceJointsCommand
        {
            get
            {
                return _cancelAddTypePieceJointeCommand
                    ?? (_cancelAddTypePieceJointeCommand = new RelayCommand(
                    () =>
                    {
                        //Todo 
                    }));
            }
        }
        private RelayCommand _openRecuDeDepotDesignerCommand;
        public RelayCommand OpenRecuDeDepotDesignerCommand
        {
            get
            {
                return _openRecuDeDepotDesignerCommand
                    ?? (_openRecuDeDepotDesignerCommand = new RelayCommand(async () =>
                    {

                        var editor = new ReportEditorView(App.RecuDeDepotReport);
                        await editor.ShowDialogAsync();



                    }));
            }
        }
        private RelayCommand _openRendezVousDesignerCommand;
        public RelayCommand OpenRendezVousDesignerCommand
        {
            get
            {
                return _openRendezVousDesignerCommand
                    ?? (_openRendezVousDesignerCommand = new RelayCommand(async () =>
                    {

                        var editor = new ReportEditorView(App.RendezVousReport);
                        await editor.ShowDialogAsync();

                    }));
            }
        }

        private RelayCommand _rendezVousIsSelectedCommand;
        public RelayCommand RendezVousReportIsSelectedCommand
        {
            get
            {
                return _rendezVousIsSelectedCommand
                    ?? (_rendezVousIsSelectedCommand = new RelayCommand(
                    () =>
                    {
                        ReportPath = App.RendezVousReport;
                        Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Refresh"));
                    }));
            }
        }
        private RelayCommand _recuDeDepotIsSelectedCommand;
        public RelayCommand RecuDeDepotRepportIsSelectedCommand
        {
            get
            {
                return _recuDeDepotIsSelectedCommand
                    ?? (_recuDeDepotIsSelectedCommand = new RelayCommand(
                    () =>
                    {
                        ReportPath = App.RecuDeDepotReport;
                        Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Refresh"));
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


                if (SelectedUser.RolesCollection == null)
                {
                    SelectedUser.RolesCollection = new RolesCollection();
                }
                TreeViewRollCollection = new ObservableCollection<TreeViewModel>()
                {
                    new TreeViewModel()
                    {
                        Content = "CalendarView",
                        TreeViewModelCollection = new ObservableCollection<TreeViewModel>()
                        {
                            new TreeViewModel()
                            {
                                Content = "AppointementViewAllow",
                                IsChecked = SelectedUser.RolesCollection.AppointementViewAllow
                            },
                            new TreeViewModel()
                            {
                                Content = "AppointementEditAllow",
                                IsChecked = SelectedUser.RolesCollection.AppointementEditAllow
                            }
                        }
                    },
                    new TreeViewModel()
                    {
                        Content = "Doctors View",
                        TreeViewModelCollection = new ObservableCollection<TreeViewModel>()
                        {
                            new TreeViewModel()
                            {
                                Content = "DoctorsViewAllow",
                                IsChecked = SelectedUser.RolesCollection.DoctorsViewAllow
                            },
                            new TreeViewModel()
                            {
                                Content = "DoctorsAddAllow",
                                IsChecked = SelectedUser.RolesCollection.DoctorsAddAllow
                            }
                        }
                    },
                    new TreeViewModel()
                    {
                        Content = "Patient View",
                        TreeViewModelCollection = new ObservableCollection<TreeViewModel>()
                        {
                            new TreeViewModel()
                            {
                                Content = "PatientsViewAllow",
                                IsChecked = SelectedUser.RolesCollection.PatientsViewAllow
                            },
                            new TreeViewModel()
                            {
                                Content = "PatientsEditAllow",
                                IsChecked = SelectedUser.RolesCollection.PatientsEditAllow
                            },
                            new TreeViewModel()
                            {
                                Content = "PatientsEditAppointementAllow",
                                IsChecked = SelectedUser.RolesCollection.PatientsEditAppointementAllow
                            }
                        }
                    },
                    new TreeViewModel()
                    {
                        Content = "Speciality View",
                        TreeViewModelCollection = new ObservableCollection<TreeViewModel>()
                        {
                            new TreeViewModel()
                            {
                                Content = "SpecialitiesViewAllow",
                                IsChecked = SelectedUser.RolesCollection.SpecialitiesViewAllow
                            },
                            new TreeViewModel()
                            {
                                Content = "SpecialitiesEditAllow",
                                IsChecked = SelectedUser.RolesCollection.SpecialitiesEditAllow
                            }
                        }
                    },
                    new TreeViewModel()
                    {
                        Content = "Pathology View",
                        TreeViewModelCollection = new ObservableCollection<TreeViewModel>()
                        {
                            new TreeViewModel()
                            {
                                Content = "PathologiesViewAllow",
                                IsChecked = SelectedUser.RolesCollection.PathologiesViewAllow
                            },
                            new TreeViewModel()
                            {
                                Content = "PathologiesEditAllow",
                                IsChecked = SelectedUser.RolesCollection.PathologiesEditAllow
                            }
                        }
                    },
                    new TreeViewModel()
                    {
                        Content = "MyPatients View",
                        TreeViewModelCollection = new ObservableCollection<TreeViewModel>()
                        {
                            new TreeViewModel()
                            {
                                Content = "MyPatientsViewAllow",
                                IsChecked = SelectedUser.RolesCollection.MyPatientsViewAllow
                            },
                            new TreeViewModel()
                            {
                                Content = "MyPatientsEditAllow",
                                IsChecked = SelectedUser.RolesCollection.MyPatientsEditAllow
                            },
                            new TreeViewModel()
                            {
                                Content = "MyPatientsEditAppointementAllow",
                                IsChecked = SelectedUser.RolesCollection.MyPatientsEditAppointementAllow
                            }
                        }
                    },
                    new TreeViewModel()
                    {
                        Content = "Sms Notifications View",
                        TreeViewModelCollection = new ObservableCollection<TreeViewModel>()
                        {
                            new TreeViewModel()
                            {
                                Content = "SmsNotificationViewAllow",
                                IsChecked = SelectedUser.RolesCollection.SmsNotificationViewAllow
                            },
                            new TreeViewModel()
                            {
                                Content = "SmsNotificationEditAllow",
                                IsChecked = SelectedUser.RolesCollection.SmsNotificationEditAllow
                            }

                        }
                    },
                    new TreeViewModel()
                    {
                        Content = "Settings View",
                        TreeViewModelCollection = new ObservableCollection<TreeViewModel>()
                        {
                            new TreeViewModel()
                            {
                                Content = "SettingsViewUsersAllow",
                                IsChecked = SelectedUser.RolesCollection.SettingsViewUsersAllow
                            },
                            new TreeViewModel()
                            {
                                Content = "SettingsEditUsersAllow",
                                IsChecked = SelectedUser.RolesCollection.SettingsEditUsersAllow
                            },
                            new TreeViewModel()
                            {
                                Content = "SettingsMangeThemeAllow",
                                IsChecked = SelectedUser.RolesCollection.SettingsMangeThemeAllow
                            }
                        }
                    }

                };
            }

        }
        #endregion
    }
}
