using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
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
    public class DragableProperty
    {
        public String PropertyName { get; set; }
        public String PropertyId { get; set; }
    }

    public class SettingsViewModel : NavigableViewModelBase
    {
        #region Fields
        private ObservableCollection<UserType> _userTypeCollection;
        private ObservableCollection<JourFerie> _listDesJoursFerieFix;
        private JourFerie _selectedJourFerieFix;
        private readonly CpmcContext _dbContext = new CpmcContext();
        private ObservableCollection<User> _usersList;
        private User _selectedUser;
        private ObservableCollection<TreeViewModel> _treeViewRollCollection = new ObservableCollection<TreeViewModel>();
        private ObservableCollection<EntityToAdd<UserType>> _userTypeToAddCollection;
        private ObservableCollection<PieceJointeType> _typePieceJointeCollection;
        private PieceJointeType _selectedTypePieceJointe;
        private ObservableCollection<JourFerie> _listDesJourFeriesOccasionnelle;
        private JourFerie _selectedJourFerie;
        private string _reportPath;
        private bool _isFormEnabled;
        private ObservableCollection<string> _monthList;
        private SettingsCollection _settingsCollection;

        
        

        private ObservableCollection<DragableProperty>_dragablePropertiesCollection=new ObservableCollection<DragableProperty>()
        {
            new DragableProperty()
            {
                PropertyId = "@NomPatient",
                PropertyName = "Nom du patient"
            }, new DragableProperty()
            {
                PropertyId = "@PrenomPatient",
                PropertyName = "Prenom du patient"
            }, new DragableProperty()
            {
                PropertyId = "@NomMedecin",
                PropertyName = "Nom du medecin"
            }, new DragableProperty()
            {
                PropertyId = "@PrenomMedecin",
                PropertyName = "Prenom du patient"
            }, new DragableProperty()
            {
                PropertyId = "@DateRdv",
                PropertyName = "Date Rdv"
            },
        };

        public ObservableCollection<DragableProperty> DragablePropertiesCollection
        {
            get
            {
                return _dragablePropertiesCollection;;
            }

            set
            {
                if (_dragablePropertiesCollection == value)
                {
                    return;
                }

                _dragablePropertiesCollection = value;
                RaisePropertyChanged();
            }
        }
        #endregion
        #region Properties
        public ObservableCollection<UserType> UserTypeCollection
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
        public ObservableCollection<string> MonthsList
        {
            get
            {
                return _monthList;
            }

            set
            {
                if (_monthList == value)
                {
                    return;
                }

                _monthList = value;
                RaisePropertyChanged();
            }
        }
        public ObservableCollection<JourFerie> ListDesJoursFerieFix
        {
            get
            {
                return _listDesJoursFerieFix;
            }

            set
            {
                if (_listDesJoursFerieFix == value)
                {
                    return;
                }

                _listDesJoursFerieFix = value;
                RaisePropertyChanged();
            }
        }
        public JourFerie SelectedJourFerieFix
        {
            get
            {
                return _selectedJourFerieFix;
            }

            set
            {
                if (_selectedJourFerieFix == value)
                {
                    return;
                }

                _selectedJourFerieFix = value;
                RaisePropertyChanged();
            }
        }
        public SettingsCollection SettingsCollection
        {
            get
            {
                return _settingsCollection;
            }

            set
            {
                if (_settingsCollection == value)
                {
                    return;
                }

                _settingsCollection = value;
                RaisePropertyChanged();
            }
        }
        public bool IsFormEnabled
        {
            get
            {
                return _isFormEnabled;
            }

            set
            {
                if (_isFormEnabled == value)
                {
                    return;
                }

                _isFormEnabled = value;
                RaisePropertyChanged();
            }
        }
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
        public ObservableCollection<EntityToAdd<UserType>> UserTypeToAddCollection
        {
            get
            {
                return _userTypeToAddCollection;
            }

            set
            {
                if (_userTypeToAddCollection == value)
                {
                    return;
                }

                _userTypeToAddCollection = value;
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
                IsFormEnabled = value != null;

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
        public ObservableCollection<JourFerie> ListDesJourFeriesOccasionnelle
        {
            get
            {
                return _listDesJourFeriesOccasionnelle;
            }

            set
            {
                if (_listDesJourFeriesOccasionnelle == value)
                {
                    return;
                }

                _listDesJourFeriesOccasionnelle = value;
                RaisePropertyChanged();
            }
        }
        public JourFerie SelectedJourFerie
        {
            get
            {
                return _selectedJourFerie;
            }

            set
            {
                if (_selectedJourFerie == value)
                {
                    return;
                }

                _selectedJourFerie = value;
                RaisePropertyChanged();
            }
        }
        #endregion
        #region Commands
        private RelayCommand _savePatientStatusCommand;
        public RelayCommand SavePatientStatusCommand
        {
            get
            {
                return _savePatientStatusCommand
                    ?? (_savePatientStatusCommand = new RelayCommand(
                    () =>
                    {
                        SettingsCollection.SaveScheduleSettingsToDataBase();   //this a temporary hack to be updated //todo

                    }));
            }
        }
        private RelayCommand _cancelPatientStatusCommand;
        public RelayCommand CancelPatientStatusCommand
        {
            get
            {
                return _cancelPatientStatusCommand
                    ?? (_cancelPatientStatusCommand = new RelayCommand(async () =>
                    {
                        SettingsCollection = new SettingsCollection();
                        await SettingsCollection.LoadSchedulerSettings();
                        RaisePropertyChanged("SettingsCollection");
                    }));
            }
        }
        private RelayCommand _resetPatientStatusCommand;
        public RelayCommand ResetPatientStatusCommand
        {
            get
            {
                return _resetPatientStatusCommand
                    ?? (_resetPatientStatusCommand = new RelayCommand(
                    () =>
                    {
                        //Todo   
                    }));
            }
        }
        private RelayCommand _statusDesPatientsSettingsLoadedCommand;
        public RelayCommand StatusDesPatientsSettingsLoadedCommand
        {
            get
            {
                return _statusDesPatientsSettingsLoadedCommand
                    ?? (_statusDesPatientsSettingsLoadedCommand = new RelayCommand(async () =>
                    {
                        SettingsCollection = new SettingsCollection();
                        await SettingsCollection.LoadSchedulerSettings();
                        RaisePropertyChanged("SettingsCollection");
                    }));
            }
        }
        private RelayCommand _settingsViewLoadedCommand;
        public RelayCommand SettingsViewLoadedCommand
        {
            get
            {
                return _settingsViewLoadedCommand
                    ?? (_settingsViewLoadedCommand = new RelayCommand(async () =>
                    {

                    }));
            }
        }
        private RelayCommand _accountsViewLoadedCommand;
        public RelayCommand AccountsViewLoadedCommand
        {
            get
            {
                return _accountsViewLoadedCommand
                    ?? (_accountsViewLoadedCommand = new RelayCommand(async () =>
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

        private RelayCommand _jourFerieDataGridLoadedCommand;
        public RelayCommand JourFerieDataGridLoadedCommand
        {
            get
            {
                return _jourFerieDataGridLoadedCommand
                    ?? (_jourFerieDataGridLoadedCommand = new RelayCommand(async () =>
                    {

                        await LoadJourFerieOcasion();
                        await LoadJourFerieFix();
                    }));
            }
        }



        private async Task LoadJourFerieOcasion()
        {
            ListDesJourFeriesOccasionnelle = new ObservableCollection<JourFerie>(await Task.Run(() => _dbContext.JourFeries.Where(x => x.TypeJourFerie == TypeJourFerie.Ocas)));
        }
        private async Task LoadJourFerieFix()
        {
            ListDesJoursFerieFix = new ObservableCollection<JourFerie>(await Task.Run(() => _dbContext.JourFeries.Where(x => x.TypeJourFerie == TypeJourFerie.Fix)));
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
        private RelayCommand _saveJourFerieOcasCommand;
        public RelayCommand SaveJourFerieOcasiCommand
        {
            get
            {
                return _saveJourFerieOcasCommand
                    ?? (_saveJourFerieOcasCommand = new RelayCommand(async () =>
                        {
                            await Task.Run(() => ListDesJourFeriesOccasionnelle.ForEach((jf) =>
                            {
                                if (jf.JourFerieId == Guid.Empty)
                                {
                                    _dbContext.JourFeries.Add(new JourFerie()
                                    {
                                        DateJourFerie = jf.DateJourFerie,
                                        TitreJourFerie = jf.TitreJourFerie,
                                        DescriptionJourFerie = jf.DescriptionJourFerie,
                                        TypeJourFerie = TypeJourFerie.Ocas
                                    });
                                }
                            }));
                            _dbContext.SaveChanges();
                            await LoadJourFerieOcasion();
                        }));
            }
        }
        private RelayCommand _saveJourFerieFixCommand;
        public RelayCommand SaveJourFerieFixCommand
        {
            get
            {
                return _saveJourFerieFixCommand
                    ?? (_saveJourFerieFixCommand = new RelayCommand(async () =>
                        {
                            await Task.Run(() => ListDesJoursFerieFix.ForEach((jf) =>
                            {
                                if (jf.JourFerieId == Guid.Empty)
                                {
                                    _dbContext.JourFeries.Add(new JourFerie()
                                    {
                                        DateJourFerie = jf.DateJourFerie,
                                        TitreJourFerie = jf.TitreJourFerie,
                                        DescriptionJourFerie = jf.DescriptionJourFerie,
                                        TypeJourFerie = TypeJourFerie.Fix
                                    });
                                }
                            }));
                            _dbContext.SaveChanges();
                            await LoadJourFerieFix();
                        }));
            }
        }
        private RelayCommand _cancelUpdateJourFerieCommand;
        public RelayCommand CancelUpdateJourFerieCommand
        {
            get
            {
                return _cancelUpdateJourFerieCommand
                    ?? (_cancelUpdateJourFerieCommand = new RelayCommand(async () =>
                        {
                            await LoadJourFerieOcasion();
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
        private RelayCommand _addNewUserCommand;
        public RelayCommand AddNewUserCommand
        {
            get
            {
                return _addNewUserCommand
                    ?? (_addNewUserCommand = new RelayCommand(
                    () =>
                    {
                        SelectedUser = new User()
                        {
                            RolesCollection = new RolesCollection()
                        };
                    }));
            }
        }
        private RelayCommand<object> _SaveAddNewUserCommand;

        public RelayCommand<object> SaveAddNewUserCommand
        {
            get
            {
                return _SaveAddNewUserCommand
                    ?? (_SaveAddNewUserCommand = new RelayCommand<object>(async (obj) =>
                    {
                        var passwordBox = obj as PasswordBox;
                        if (passwordBox != null)
                        {
                            SelectedUser.UserPass = passwordBox.Password;  //to be hashed
                        }
                        if (SelectedUser.UserId == Guid.Empty)
                        {
                            await AddNewUser();
                        }
                        _dbContext.SaveChanges();
                        await LoadUsersList();
                        SelectedUser = null;

                    }));
            }
        }

        private async Task AddNewUser()
        {
            //Added by Farouk for Audit purpose
            SelectedUser.UserId = Guid.NewGuid();

            await Task.Run(() =>
            {
                _dbContext.Users.Add(SelectedUser);
            });
        }

        private RelayCommand _deleteUserCommand;
        public RelayCommand DeleteUserCommand
        {
            get
            {
                return _deleteUserCommand
                    ?? (_deleteUserCommand = new RelayCommand(
                    () =>
                    {
                        //todo Logical suppression 
                        if (SelectedUser != null)
                        {
                            //You can't delete a doc from the users view, only from the Doctors View
                            if (SelectedUser.UserId != Guid.Empty && SelectedUser.UserType.UserTypeName != App.Medecin)
                            {
                                _dbContext.RolesCollections.Remove(SelectedUser.RolesCollection);
                                _dbContext.Users.Remove(SelectedUser);
                                _dbContext.SaveChanges();
                                UsersList.Remove(SelectedUser);
                                SelectedUser = null;
                                TreeViewRollCollection = null;
                            }
                        }

                    }));
            }
        }
        private RelayCommand _cancelAddUserCommand;
        public RelayCommand CancelAddUserCommand
        {
            get
            {
                return _cancelAddUserCommand
                    ?? (_cancelAddUserCommand = new RelayCommand(
                    () =>
                    {
                        if (SelectedUser != null)
                        {
                            if (SelectedUser.UserId != Guid.Empty)
                                _dbContext.Entry(SelectedUser).Reload();
                        }
                        SelectedUser = null;

                    }));
            }
        }
        private RelayCommand<object> _userTypeCheckedCommand;

        public RelayCommand<object> UserTypeCheckedCommand
        {
            get
            {
                return _userTypeCheckedCommand
                    ?? (_userTypeCheckedCommand = new RelayCommand<object>(async (obj) =>
                    {
                        var search = UserTypeToAddCollection.Where(ut => ut.IsAdded).Select(x => x.Entity.UserTypeId);
                        UsersList = new ObservableCollection<User>(await Task.Run(() => _dbContext.Users.Where(u => search.Contains(u.UserTypeId))));

                    }));
            }
        }
        private RelayCommand _userTypeChangedCommand;
        public RelayCommand UserTypeChangedCommand
        {
            get
            {
                return _userTypeChangedCommand
                    ?? (_userTypeChangedCommand = new RelayCommand(async () =>
                    {
                        if (SelectedUser != null)
                        {
                            //if (SelectedUser.UserType != null)              //todo 
                            //{
                            //    var rolesCollection = SelectedUser.RolesCollection;
                            //    RollsManager.GetDefaultUserRolls(SelectedUser.UserType.UserTypeName,ref rolesCollection);
                            //}
                        }

                    }));
            }
        }
        #endregion
        #region Ctors and Methods
        public SettingsViewModel(IFrameNavigationService mainFrameNavigationService, IInnerFrameNavigationService innerFrameNavigationService)
            : base(mainFrameNavigationService, innerFrameNavigationService)
        {
            if (DateTimeFormatInfo.CurrentInfo != null)
                MonthsList = new ObservableCollection<string>(DateTimeFormatInfo.CurrentInfo.MonthNames);
        }

        private async Task LoadUsersList()
        {
            UsersList = new ObservableCollection<User>(await Task.Run(() => _dbContext.Users));
        }

        private async Task LoadUserTypeCollection()
        {
            UserTypeToAddCollection = new ObservableCollection<EntityToAdd<UserType>>(await Task.Run(() => _dbContext.UserTypes.Select(x => new EntityToAdd<UserType>()
            {
                Entity = x,
                IsAdded = true
            })));
            UserTypeCollection = new ObservableCollection<UserType>(await Task.Run(() => _dbContext.UserTypes));
        }

        //private async Task LoadRollCollectionForSelectedUser()
        //{

        //    await Task.Run(() =>
        //    {
        //        if (SelectedUser != null)
        //        {

        //            TreeViewRollCollection = new ObservableCollection<TreeViewModel>()
        //        {
        //            new TreeViewModel()
        //            {
        //                Content = "CalendarView",
        //                TreeViewModelCollection = new ObservableCollection<TreeViewModel>()
        //                {
        //                    new TreeViewModel()
        //                    {
        //                        Content = "AppointementViewAllow",
        //                        IsChecked = SelectedUser.RolesCollection.AppointementViewAllow
        //                    },
        //                    new TreeViewModel()
        //                    {
        //                        Content = "AppointementEditAllow",
        //                        IsChecked = SelectedUser.RolesCollection.AppointementEditAllow
        //                    }
        //                }
        //            },
        //            new TreeViewModel()
        //            {
        //                Content = "DoctorsView",
        //                TreeViewModelCollection = new ObservableCollection<TreeViewModel>()
        //                {
        //                    new TreeViewModel()
        //                    {
        //                        Content = "DoctorsViewAllow",
        //                        IsChecked = SelectedUser.RolesCollection.DoctorsViewAllow
        //                    },
        //                    new TreeViewModel()
        //                    {
        //                        Content = "DoctorsAddAllow",
        //                        IsChecked = SelectedUser.RolesCollection.DoctorsAddAllow
        //                    }
        //                }
        //            },
        //            new TreeViewModel()
        //            {
        //                Content = "PatientView",
        //                TreeViewModelCollection = new ObservableCollection<TreeViewModel>()
        //                {
        //                    new TreeViewModel()
        //                    {
        //                        Content = "PatientsViewAllow",
        //                        IsChecked = SelectedUser.RolesCollection.PatientsViewAllow
        //                    },
        //                    new TreeViewModel()
        //                    {
        //                        Content = "PatientsEditAllow",
        //                        IsChecked = SelectedUser.RolesCollection.PatientsEditAllow
        //                    },
        //                    new TreeViewModel()
        //                    {
        //                        Content = "PatientsEditAppointementAllow",
        //                        IsChecked = SelectedUser.RolesCollection.PatientsEditAppointementAllow
        //                    }
        //                }
        //            },
        //            new TreeViewModel()
        //            {
        //                Content = "SpecialitePrincipaleView",
        //                TreeViewModelCollection = new ObservableCollection<TreeViewModel>()
        //                {
        //                    new TreeViewModel()
        //                    {
        //                        Content = "SpecialitiesViewAllow",
        //                        IsChecked = SelectedUser.RolesCollection.SpecialitiesViewAllow
        //                    },
        //                    new TreeViewModel()
        //                    {
        //                        Content = "SpecialitiesEditAllow",
        //                        IsChecked = SelectedUser.RolesCollection.SpecialitiesEditAllow
        //                    }
        //                }
        //            },
        //            new TreeViewModel()
        //            {
        //                Content = "PathologyView",
        //                TreeViewModelCollection = new ObservableCollection<TreeViewModel>()
        //                {
        //                    new TreeViewModel()
        //                    {
        //                        Content = "PathologiesViewAllow",
        //                        IsChecked = SelectedUser.RolesCollection.PathologiesViewAllow
        //                    },
        //                    new TreeViewModel()
        //                    {
        //                        Content = "PathologiesEditAllow",
        //                        IsChecked = SelectedUser.RolesCollection.PathologiesEditAllow
        //                    }
        //                }
        //            },
        //            new TreeViewModel()
        //            {
        //                Content = "MyPatientsView",
        //                TreeViewModelCollection = new ObservableCollection<TreeViewModel>()
        //                {
        //                    new TreeViewModel()
        //                    {
        //                        Content = "MyPatientsViewAllow",
        //                        IsChecked = SelectedUser.RolesCollection.MyPatientsViewAllow
        //                    },
        //                    new TreeViewModel()
        //                    {
        //                        Content = "MyPatientsEditAllow",
        //                        IsChecked = SelectedUser.RolesCollection.MyPatientsEditAllow
        //                    },
        //                    new TreeViewModel()
        //                    {
        //                        Content = "MyPatientsEditAppointementAllow",
        //                        IsChecked = SelectedUser.RolesCollection.MyPatientsEditAppointementAllow
        //                    }
        //                }
        //            },
        //            new TreeViewModel()
        //            {
        //                Content = "Sms Notifications View",
        //                TreeViewModelCollection = new ObservableCollection<TreeViewModel>()
        //                {
        //                    new TreeViewModel()
        //                    {
        //                        Content = "SmsNotificationViewAllow",
        //                        IsChecked = SelectedUser.RolesCollection.SmsNotificationViewAllow
        //                    },
        //                    new TreeViewModel()
        //                    {
        //                        Content = "SmsNotificationEditAllow",
        //                        IsChecked = SelectedUser.RolesCollection.SmsNotificationEditAllow
        //                    }

        //                }
        //            },
        //            new TreeViewModel()
        //            {
        //                Content = "Settings View",
        //                TreeViewModelCollection = new ObservableCollection<TreeViewModel>()
        //                {
        //                    new TreeViewModel()
        //                    {
        //                        Content = "SettingsViewUsersAllow",
        //                        IsChecked = SelectedUser.RolesCollection.SettingsViewUsersAllow
        //                    },
        //                    new TreeViewModel()
        //                    {
        //                        Content = "SettingsEditUsersAllow",
        //                        IsChecked = SelectedUser.RolesCollection.SettingsEditUsersAllow
        //                    },
        //                    new TreeViewModel()
        //                    {
        //                        Content = "SettingsMangeThemeAllow",
        //                        IsChecked = SelectedUser.RolesCollection.SettingsMangeThemeAllow
        //                    }
        //                }
        //            }

        //        };
        //        }
        //    });
        //}
        #endregion
    }
}
