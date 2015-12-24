using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using CPMCAppointmentSystem.Helpers;
using CPMCAppointmentSystem.SubModel;
using CPMCAppointmentSystem.View.SettingsViews;
using DataLayer;
using DataLayer.Model;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GsmManager;
using Syncfusion.Data.Extensions;
using Xceed.Wpf.Toolkit;

namespace CPMCAppointmentSystem.ViewModel
{
    public class SettingsViewModel : NavigableViewModelBase
    {

        #region Fields
        private Dictionary<string, object> _selectedUserUserTypeDictionary;
        private ObservableCollection<EntityToAdd<UserType>> _userTypeToFilterCollection;
        private AddNewUserTypeView _addNewUserTypeView;
        private EntityToAdd<UserType> _selectedUserType;
        private ObservableCollection<UserType> _userTypesCollection;
        private string _testSmsNumber;
        private string _testSmsMessage;
        private string _pinCode;
        private bool _isGsmProgressRingActive = false;
        private GsmConnection _gsmConnection;
        private bool _isGsmSettingsValidated;
        private string _gsmStateText;
        private ObservableCollection<int> _timeOutsCollection;
        private ObservableCollection<string> _comPortsCollection;
        private ObservableCollection<int> _baudRatesCollection;
        private SolidColorBrush _saveButtonBackground = new SolidColorBrush(Color.FromArgb(255, 84, 168, 253));
        private bool _allDataLoaded = false;
        private User _connectedUser;
        private string _betweenAtCmdDelay;
        private string _centreDeMessagerie;
        private ObservableCollection<JourFerie> _listDesJoursFerieFix;
        private JourFerie _selectedJourFerieFix;
        private CpmcContext _dbContext = new CpmcContext();
        private ObservableCollection<User> _usersList;
        private User _selectedUser;
        private ObservableCollection<TreeViewModel> _treeViewRollCollection = new ObservableCollection<TreeViewModel>();
        private Dictionary<string, object> _userTypeDictionary;
        private ObservableCollection<PieceJointeType> _typePieceJointeCollection;
        private PieceJointeType _selectedTypePieceJointe;
        private ObservableCollection<JourFerie> _listDesJourFeriesOccasionnelle;
        private JourFerie _selectedJourFerie;
        private string _reportPath;
        private bool _isFormEnabled;
        private ObservableCollection<string> _monthList;
        private SettingsCollection _settingsCollection;
        private ObservableCollection<DragableProperty> _dragablePropertiesCollection;
        private String _smsBodyTemplate;
        private ObservableCollection<Medecin> _doctorsListCollection;
        private ConnectionSettings _connectionSettings;
        #endregion
        #region Properties       
        public ObservableCollection<EntityToAdd<UserType>> UserTypeToFilterCollection
        {
            get
            {
                return _userTypeToFilterCollection;
            }

            set
            {
                if (_userTypeToFilterCollection == value)
                {
                    return;
                }

                _userTypeToFilterCollection = value;
                RaisePropertyChanged();
            }
        }
        public EntityToAdd<UserType> SelectedUserType
        {
            get
            {
                return _selectedUserType;
            }

            set
            {
                if (_selectedUserType == value)
                {
                    return;
                }

                _selectedUserType = value;
                RaisePropertyChanged();
            }
        }
        public ObservableCollection<UserType> UserTypesCollection
        {
            get
            {
                return _userTypesCollection;
            }

            set
            {
                if (_userTypesCollection == value)
                {
                    return;
                }

                _userTypesCollection = value;
                RaisePropertyChanged();
            }
        }
        public string TestSmsNumber
        {
            get
            {
                return _testSmsNumber;
            }

            set
            {
                if (_testSmsNumber == value)
                {
                    return;
                }

                _testSmsNumber = value;
                RaisePropertyChanged();
            }
        }
        public string TestSmsMessage
        {
            get
            {
                return _testSmsMessage;
            }

            set
            {
                if (_testSmsMessage == value)
                {
                    return;
                }

                _testSmsMessage = value;
                RaisePropertyChanged();
            }
        }
        public string PinCode
        {
            get
            {
                return _pinCode;
            }

            set
            {
                if (_pinCode == value)
                {
                    return;
                }

                _pinCode = value;
                RaisePropertyChanged();
            }
        }
        public bool IsGsmProgressRingAcive
        {
            get
            {
                return _isGsmProgressRingActive;
            }

            set
            {
                if (_isGsmProgressRingActive == value)
                {
                    return;
                }

                _isGsmProgressRingActive = value;
                RaisePropertyChanged();
            }
        }
        public GsmConnection GsmConnection
        {
            get
            {
                return _gsmConnection;
            }

            set
            {
                if (_gsmConnection == value)
                {
                    return;
                }

                _gsmConnection = value;
                RaisePropertyChanged();
            }
        }
        public bool IsGsmSettingsValidated
        {
            get
            {
                return _isGsmSettingsValidated;
            }

            set
            {
                if (_isGsmSettingsValidated == value)
                {
                    return;
                }

                _isGsmSettingsValidated = value;
                RaisePropertyChanged();
            }
        }
        public string GsmStateText
        {
            get
            {
                return _gsmStateText;
            }

            set
            {
                if (_gsmStateText == value)
                {
                    return;
                }

                _gsmStateText = value;
                RaisePropertyChanged();
            }
        }



        public ObservableCollection<int> TimeOutsCollection
        {
            get
            {
                return _timeOutsCollection;
            }

            set
            {
                if (_timeOutsCollection == value)
                {
                    return;
                }

                _timeOutsCollection = value;
                RaisePropertyChanged();
            }
        }
        public ObservableCollection<string> ComPortsCollection
        {
            get
            {
                return _comPortsCollection;
            }

            set
            {
                if (_comPortsCollection == value)
                {
                    return;
                }

                _comPortsCollection = value;
                RaisePropertyChanged();
            }
        }
        public ObservableCollection<int> BaudRatesCollection
        {
            get
            {
                return _baudRatesCollection;
            }

            set
            {
                if (_baudRatesCollection == value)
                {
                    return;
                }

                _baudRatesCollection = value;
                RaisePropertyChanged();
            }
        }
        public ConnectionSettings ConnectionSettings
        {
            get
            {
                return _connectionSettings;
            }

            set
            {
                if (_connectionSettings == value)
                {
                    return;
                }

                _connectionSettings = value;
                RaisePropertyChanged();
            }
        }
        public SolidColorBrush SaveButtonBackground
        {
            get
            {
                return _saveButtonBackground;
            }

            set
            {
                if (Equals(_saveButtonBackground, value))
                {
                    return;
                }

                _saveButtonBackground = value;
                RaisePropertyChanged();
            }
        }
        public User ConnectedUser
        {
            get
            {
                return _connectedUser;
            }

            set
            {
                if (_connectedUser == value)
                {
                    return;
                }

                _connectedUser = value;
                RaisePropertyChanged();
            }
        }
        public ObservableCollection<Medecin> DoctorsListCollection
        {
            get
            {
                return _doctorsListCollection;
            }

            set
            {
                if (_doctorsListCollection == value)
                {
                    return;
                }

                _doctorsListCollection = value;
                RaisePropertyChanged();
            }
        }
        public string BetweenAtCmdDelay
        {
            get
            {
                return _betweenAtCmdDelay;
            }

            set
            {
                if (_betweenAtCmdDelay == value)
                {
                    return;
                }

                _betweenAtCmdDelay = value;
                RaisePropertyChanged();
            }
        }
        public string CenterDeMessagerie
        {
            get
            {
                return _centreDeMessagerie;
            }

            set
            {
                if (_centreDeMessagerie == value)
                {
                    return;
                }

                _centreDeMessagerie = value;
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
        public Dictionary<string, object> SelectedUserUserTypesDictionary
        {
            get
            {
                return _selectedUserUserTypeDictionary;
            }

            set
            {
                if (_selectedUserUserTypeDictionary == value)
                {
                    return;
                }

                _selectedUserUserTypeDictionary = value;
                RaisePropertyChanged();
            }
        }
        public Dictionary<string, object> UserTypeDictionary
        {
            get
            {
                return _userTypeDictionary;
            }

            set
            {
                if (_userTypeDictionary == value)
                {
                    return;
                }

                _userTypeDictionary = value;
                LoadSelectedUserUserTypes();
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
                LoadUserTypeToAddCollection();
                LoadSelectedUserUserTypes();
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
        public ObservableCollection<DragableProperty> DragablePropertiesCollection
        {
            get
            {
                return _dragablePropertiesCollection;
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
        public String SmsBodyTemplate
        {
            get
            {
                return _smsBodyTemplate;
            }

            set
            {
                if (_smsBodyTemplate == value)
                {
                    return;
                }

                _smsBodyTemplate = value;
                RaisePropertyChanged();
            }
        }
        #endregion
        #region Views Loaded Command Region
        private RelayCommand _settingsTabControlSelectionChangedCommand;
        public RelayCommand SettingsTabControlSelectionChangedCommand
        {
            get
            {
                return _settingsTabControlSelectionChangedCommand
                    ?? (_settingsTabControlSelectionChangedCommand = new RelayCommand(
                    () =>
                    {
                        SaveButtonBackground = new SolidColorBrush(Color.FromArgb(255, 84, 168, 253));
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
                        SaveButtonBackground = new SolidColorBrush(Color.FromArgb(255, 84, 168, 253));
                        _allDataLoaded = false;
                        SettingsCollection = new SettingsCollection();
                        await SettingsCollection.LoadSchedulerSettings();
                        RaisePropertyChanged("SettingsCollection");
                        _allDataLoaded = true;
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
                        _allDataLoaded = false;
                        _dbContext = new CpmcContext();
                        try
                        {
                            var user = MainFrameNavigationService.Parameter as User;
                            if (user != null)                           //todo 
                                ConnectedUser = _dbContext.Users.Find(user.UserId);

                        }
                        catch (Exception)
                        {

                        }
                        _allDataLoaded = true;
                    }));
            }
        }

        private RelayCommand _settingsViewUnLoadedCommand;
        public RelayCommand SettingsViewUnLoadedCommand
        {
            get
            {
                return _settingsViewUnLoadedCommand
                    ?? (_settingsViewUnLoadedCommand = new RelayCommand(async () =>
                    {
                        await Task.Run(() =>
                        {
                            while (!_allDataLoaded) { }
                            _dbContext.Dispose();

                        });
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
                        _allDataLoaded = false;
                        await LoadUserTypeToAddCollection();
                        await LoadUserTypeToFilterCollection();
                        await LoadUsersList();
                        _allDataLoaded = true;
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
                        SaveButtonBackground = new SolidColorBrush(Color.FromArgb(255, 84, 168, 253));
                        _allDataLoaded = false;
                        await LoadTypePieceJointsCollection();
                        _allDataLoaded = true;

                    }));
            }
        }
        private RelayCommand _doctorsWorkDaysLoadedCommand;
        public RelayCommand DoctorsWorkDaysLoadedCommand
        {
            get
            {
                return _doctorsWorkDaysLoadedCommand
                    ?? (_doctorsWorkDaysLoadedCommand = new RelayCommand(async () =>
                    {
                        SaveButtonBackground = new SolidColorBrush(Color.FromArgb(255, 84, 168, 253));
                        _allDataLoaded = false;
                        await LoadDoctorsDataGrid();
                        _allDataLoaded = true;
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
                        SaveButtonBackground = new SolidColorBrush(Color.FromArgb(255, 84, 168, 253));
                        _allDataLoaded = false;

                        await LoadJourFerieOcasion();
                        await LoadJourFerieFix();
                        _allDataLoaded = true;
                    }));
            }
        }
        private RelayCommand _smsSettingsTabLoadedCommand;
        public RelayCommand SmsSettingsTabLoadedCommand
        {
            get
            {
                return _smsSettingsTabLoadedCommand
                    ?? (_smsSettingsTabLoadedCommand = new RelayCommand(() =>
                    {
                        SaveButtonBackground = new SolidColorBrush(Color.FromArgb(255, 84, 168, 253));
                        _allDataLoaded = false;
                        InitDragablePropertiesCollection();
                        //Init the new sms API
                        ComPortsCollection = new ObservableCollection<string>(GsmManager.GsmHelper.GetAvailablePortNamesInDevice());
                        BaudRatesCollection = new ObservableCollection<int>(GsmManager.GsmHelper.GetUsualBaudRate());
                        TimeOutsCollection = new ObservableCollection<int>(GsmManager.GsmHelper.GetUsualTimeOuts());
                        //end init of the new sms API





                        GetSmsSettings();
                        _allDataLoaded = true;
                    }));
            }
        }
        #endregion
        #region Commands
        private RelayCommand _saveUserTypeCommand;
        public RelayCommand SaveUserTypeCommand
        {
            get
            {
                return _saveUserTypeCommand
                    ?? (_saveUserTypeCommand = new RelayCommand(
                    () =>
                    {

                    }));
            }
        }
        private RelayCommand _deleteUserTypeCommand;
        public RelayCommand DeleteUserTypeCommand
        {
            get
            {
                return _deleteUserTypeCommand
                    ?? (_deleteUserTypeCommand = new RelayCommand(
                    () =>
                    {

                    }));
            }
        }
        private RelayCommand _cancelChangesToUserTypeCommand;
        public RelayCommand CancelChangesToUserTypeCommand
        {
            get
            {
                return _cancelChangesToUserTypeCommand
                    ?? (_cancelChangesToUserTypeCommand = new RelayCommand(
                    () =>
                    {

                    }));
            }
        }
        private RelayCommand _openAddNewUserTypeViewCommand;
        public RelayCommand OpenAddNewUserTypeViewCommand
        {
            get
            {
                return _openAddNewUserTypeViewCommand
                    ?? (_openAddNewUserTypeViewCommand = new RelayCommand(
                    () =>
                    {
                        _addNewUserTypeView = new AddNewUserTypeView();
                        SelectedUserType = new EntityToAdd<UserType>()
                       {
                           Entity = new UserType()
                           {
                               RolesCollection = new RolesCollection()
                           }
                       };
                        _addNewUserTypeView.ShowDialog();

                    }));
            }
        }
        private RelayCommand<object> _userTypeIconCheckedCommand;
        public RelayCommand<object> UserTypeIconCheckedCommand
        {
            get
            {
                return _userTypeIconCheckedCommand
                    ?? (_userTypeIconCheckedCommand = new RelayCommand<object>(
                    (obj) =>
                    {
                        var iconId = int.Parse(obj.ToString());
                        SelectedUserType.Entity.UserTypeIconId = iconId;

                    }));
            }
        }
        private RelayCommand _updateTheSelectedUserTypeCommand;
        public RelayCommand UpdateTheSelectedUserTypeCommand
        {
            get
            {
                return _updateTheSelectedUserTypeCommand
                    ?? (_updateTheSelectedUserTypeCommand = new RelayCommand(
                    () =>
                    {
                        if (SelectedUserType == null) return;

                        _addNewUserTypeView = new AddNewUserTypeView();
                        _addNewUserTypeView.ShowDialog();

                    }));
            }
        }
        private RelayCommand _manageUserTypeViewLoadedCommand;
        public RelayCommand ManageUserTypeViewLoadedCommand
        {
            get
            {
                return _manageUserTypeViewLoadedCommand
                    ?? (_manageUserTypeViewLoadedCommand = new RelayCommand(async () =>
                    {


                    }));
            }
        }


        private RelayCommand _readAllReceivedMessagesCommand;
        public RelayCommand ReadAllReceivedMessagesCommand
        {
            get
            {
                return _readAllReceivedMessagesCommand
                    ?? (_readAllReceivedMessagesCommand = new RelayCommand(
                    () =>
                    {
                        try
                        {
                            GsmStateText += "\n " + GsmConnection.ReadAllMessages();
                        }
                        catch (Exception)
                        {

                            GsmStateText += "\n Something went wrong";
                        }
                    }));
            }
        }
        private RelayCommand _deleteAllReceivedMessagesCommand;
        public RelayCommand DeleteAllReceivedMessagesCommand
        {
            get
            {
                return _deleteAllReceivedMessagesCommand
                    ?? (_deleteAllReceivedMessagesCommand = new RelayCommand(
                    () =>
                    {
                        try
                        {
                            GsmStateText += "\n " + GsmConnection.DeleteAllMessages();
                        }
                        catch (Exception)
                        {

                            GsmStateText += "\n Something went wrong";
                        }
                    }));
            }
        }
        private RelayCommand _sendATestSmsMessageCommand;
        public RelayCommand SendATestSmsMessageCommand
        {
            get
            {
                return _sendATestSmsMessageCommand
                    ?? (_sendATestSmsMessageCommand = new RelayCommand(
                    () =>
                    {
                        try
                        {
                            GsmStateText += "\n" + GsmConnection.SendSms(TestSmsMessage, TestSmsNumber);
                        }
                        catch (Exception)
                        {

                            GsmStateText += "Something went wrong";
                        }

                    }));
            }
        }
        private RelayCommand _clearGsmStateTextCommand;
        public RelayCommand ClearGsmStateTextCommand
        {
            get
            {
                return _clearGsmStateTextCommand
                    ?? (_clearGsmStateTextCommand = new RelayCommand(
                    () =>
                    {
                        GsmStateText = "";
                    }));
            }
        }
        private RelayCommand _cancelGsmConnectionSettingsCommand;
        public RelayCommand CancelGsmConnectionSettingsCommand
        {
            get
            {
                return _cancelGsmConnectionSettingsCommand
                    ?? (_cancelGsmConnectionSettingsCommand = new RelayCommand(
                    () =>
                    {
                        ConnectionSettings = new ConnectionSettings();
                    }));
            }
        }
        private RelayCommand _validateGsmConnectionSettingsCommand;
        public RelayCommand ValidateGsmConnectionSettingsCommand
        {
            get
            {
                return _validateGsmConnectionSettingsCommand
                    ?? (_validateGsmConnectionSettingsCommand = new RelayCommand(
                    () =>
                    {
                        IsGsmProgressRingAcive = true;
                        Task.Run(() =>
                        {
                            //First test the current settings
                            GsmStateText += "\n testing the current settings";
                            try
                            {
                                if (GsmManager.GsmHelper.TestConnection(ConnectionSettings))
                                {
                                    GsmStateText += "\n Connection succeded";
                                    IsGsmSettingsValidated = true;
                                    GsmConnection = new GsmConnection(ConnectionSettings);
                                }
                                else
                                    GsmStateText += "\n Something went wrong";
                            }
                            catch (Exception ex)
                            {
                                GsmStateText += "\n " + ex.Message;
                            }
                        });
                        IsGsmProgressRingAcive = false;
                    }));
            }
        }

        private RelayCommand _isGsmDeviceConnectedCommand;
        public RelayCommand IsGsmDeviceConnectedCommand
        {
            get
            {
                return _isGsmDeviceConnectedCommand
                    ?? (_isGsmDeviceConnectedCommand = new RelayCommand(
                    () =>
                    {
                        try
                        {
                            GsmStateText += GsmConnection.IsConnected()
                                ? "\n the gsm device is connected"
                                : "\n the gsm device is not connected";
                        }
                        catch (Exception)
                        {

                            GsmStateText += GsmConnection.IsConnected()
                                ? "\n the gsm device is connected"
                                : "\n the gsm device is not connected";
                        }

                    }));
            }
        }
        private RelayCommand _autoDecectSmcsCommand;
        public RelayCommand AutoDecectSmcsCommand
        {
            get
            {
                return _autoDecectSmcsCommand
                    ?? (_autoDecectSmcsCommand = new RelayCommand(
                    () =>
                    {

                    }));
            }
        }

        private RelayCommand _getPinStatusCommand;
        public RelayCommand GetPinStatusCommand
        {
            get
            {
                return _getPinStatusCommand
                    ?? (_getPinStatusCommand = new RelayCommand(
                    () =>
                    {
                        try
                        {
                            GsmStateText += "\n " + GsmConnection.GetPinStatusCommand();
                        }
                        catch (Exception)
                        {

                            GsmStateText += "\n Something went wrong !";
                        }
                    }));
            }
        }
        private RelayCommand _identifyGsmDeviceCommand;
        public RelayCommand IdentifyGsmDeviceCommand
        {
            get
            {
                return _identifyGsmDeviceCommand
                    ?? (_identifyGsmDeviceCommand = new RelayCommand(
                    () =>
                    {
                        try
                        {
                            GsmStateText += "\n " + GsmConnection.IdentifyDevice();
                        }
                        catch (Exception)
                        {
                            GsmStateText += "\n Something went wrong !";
                        }

                    }));
            }
        }
        private RelayCommand _getSignalQualityCommand;
        public RelayCommand GetSignalQualityCommand
        {
            get
            {
                return _getSignalQualityCommand
                    ?? (_getSignalQualityCommand = new RelayCommand(
                    () =>
                    {
                        try
                        {
                            GsmStateText += "\n " + GsmConnection.GetSignalQuality();
                        }
                        catch (Exception)
                        {

                            GsmStateText += "\n Something went wrong !";
                        }
                    }));
            }
        }
        private RelayCommand _modifyPinCommand;
        public RelayCommand ModifyPinCommand
        {
            get
            {
                return _modifyPinCommand
                    ?? (_modifyPinCommand = new RelayCommand(
                    () =>
                    {
                        try
                        {
                            GsmStateText += "\n " + GsmConnection.ChangePinCode(PinCode);
                        }
                        catch (Exception)
                        {

                            GsmStateText += "\n Something went wrong !";
                        }
                    }));
            }
        }
        private RelayCommand _resetToDefaultConfigCommand;
        public RelayCommand ResetToDefaultConfigCommand
        {
            get
            {
                return _resetToDefaultConfigCommand
                    ?? (_resetToDefaultConfigCommand = new RelayCommand(
                    () =>
                    {
                        try
                        {
                            GsmStateText += "\n " + GsmConnection.ResetToDefaultConfig();
                        }
                        catch (Exception)
                        {

                            GsmStateText += "\n Something went wrong !";
                        }
                    }));
            }
        }

        private RelayCommand _testGsmConnectionSettingsCommand;
        public RelayCommand TestGsmConnectionSettingsCommand
        {
            get
            {
                return _testGsmConnectionSettingsCommand
                    ?? (_testGsmConnectionSettingsCommand = new RelayCommand(async () =>
                    {
                        IsGsmProgressRingAcive = true;
                        await Task.Run(() =>
                        {
                            GsmStateText += "\n testing the current settings";
                            try
                            {
                                if (GsmManager.GsmHelper.TestConnection(ConnectionSettings))
                                    GsmStateText += "\n Connection succeded";
                                else
                                    GsmStateText += "\n Something went wrong";
                            }
                            catch (Exception ex)
                            {

                                GsmStateText += "\n " + ex.Message;
                            }
                        });

                        IsGsmProgressRingAcive = false;

                    }));
            }
        }
        private RelayCommand _autoDetectGsmConnectionSettingsCommand;
        public RelayCommand AutoDetectGsmConnectionSettingsCommand
        {
            get
            {
                return _autoDetectGsmConnectionSettingsCommand
                    ?? (_autoDetectGsmConnectionSettingsCommand = new RelayCommand(async () =>
                    {
                        IsGsmProgressRingAcive = true;

                        ConnectionSettings.BaudRate = 9600;
                        ConnectionSettings.TimeOut = 300;
                        if (!GsmManager.GsmHelper.GetAvailablePortNamesInDevice().Any())
                        {
                            GsmStateText += "\n No availale ports, connection failed";
                            return;
                        }
                        foreach (var port in GsmManager.GsmHelper.GetAvailablePortNamesInDevice())
                        {
                            ConnectionSettings.PortName = port;
                            GsmStateText += "\n testing connection using the port " + port;

                            try
                            {
                                if (!GsmManager.GsmHelper.TestConnection(ConnectionSettings))
                                {
                                    GsmStateText += "\n connection failed using the port " + port;
                                    ConnectionSettings.PortName = "";
                                }
                                else
                                {
                                    GsmStateText += "\n connection succeeded using the port " + port;
                                    break;
                                }
                            }
                            catch (Exception)
                            {

                                GsmStateText += "\n connection failed using the port " + port;
                                ConnectionSettings.PortName = "";
                            }
                        }
                        if (ConnectionSettings.PortName != "")
                        {
                            GsmStateText += "\n All set succesfully";
                            IsGsmSettingsValidated = true;
                            GsmConnection = new GsmConnection(ConnectionSettings);
                        }
                        else
                        {
                            GsmStateText += "\n Can't detect the Gsm device";
                            IsGsmSettingsValidated = false;
                        }


                        IsGsmProgressRingAcive = false;
                    }));
            }
        }
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
                        SaveButtonBackground = new SolidColorBrush(Colors.WhiteSmoke);
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

        private RelayCommand _saveJourDeTrvailCommand;
        public RelayCommand SaveJourDeTravailCommand
        {
            get
            {
                return _saveJourDeTrvailCommand
                    ?? (_saveJourDeTrvailCommand = new RelayCommand(
                    () =>
                    {
                        _dbContext.SaveChanges();
                        SaveButtonBackground = new SolidColorBrush(Colors.WhiteSmoke);
                    }));
            }
        }
        private RelayCommand _cancelSaveJourDeTravailCommand;
        public RelayCommand CancelSaveJourDeTravailCommand
        {
            get
            {
                return _cancelSaveJourDeTravailCommand
                    ?? (_cancelSaveJourDeTravailCommand = new RelayCommand(async () =>
                    {
                        _dbContext.Dispose();
                        _dbContext = new CpmcContext();
                        await LoadDoctorsDataGrid();
                    }));
            }
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
                        SaveButtonBackground = new SolidColorBrush(Colors.WhiteSmoke);

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
                            SaveButtonBackground = new SolidColorBrush(Colors.WhiteSmoke);
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
                        Messenger.Default.Send<NotificationMessage>(new NotificationMessage("RefreshRdv"));
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
                        Messenger.Default.Send<NotificationMessage>(new NotificationMessage("RefreshRecuDepo"));
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
                            UserTypes = new ObservableCollection<UserType>()
                        };
                    }));
            }
        }
        private RelayCommand<object> _saveAddNewUserCommand;

        public RelayCommand<object> SaveAddNewUserCommand
        {
            get
            {
                return _saveAddNewUserCommand
                    ?? (_saveAddNewUserCommand = new RelayCommand<object>(async (obj) =>
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

                        await UpdateSelectedUserUserTypes();
                        _dbContext.SaveChanges();
                        await LoadUsersList();
                        SelectedUser = null;

                    }));
            }
        }

        private async Task UpdateSelectedUserUserTypes()
        {
            SelectedUser.UserTypes = new List<UserType>();
            await Task.Run(() =>
            {
                SelectedUserUserTypesDictionary.ForEach(su =>
                {
                    SelectedUser.UserTypes.Add(
                        _dbContext.UserTypes.AsEnumerable().First(u => u.UserTypeId.ToString() == (string)su.Value));

                });
            });
            
           
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
                            //if (SelectedUser.UserId != Guid.Empty && SelectedUser.UserType.UserTypeName != App.Medecin)
                            //{
                            //    _dbContext.RolesCollections.Remove(SelectedUser.RolesCollection);
                            //    _dbContext.Users.Remove(SelectedUser);
                            //    _dbContext.SaveChanges();
                            //    UsersList.Remove(SelectedUser);
                            //    SelectedUser = null;
                            //    TreeViewRollCollection = null;
                            //}
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
                        // var search = UserTypeDictionary.Where(ut => ut.IsAdded).Select(x => x.Entity.UserTypeId);
                        //UsersList = new ObservableCollection<User>(await Task.Run(() => _dbContext.Users.Where(u => search.Contains(u.UserTypeId))));

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

        private async void LoadSelectedUserUserTypes()
        {
            
            SelectedUserUserTypesDictionary=new Dictionary<string, object>();
            if (SelectedUser == null) return;
            if (SelectedUser.UserTypes == null)
                SelectedUser.UserTypes = new List<UserType>();

            var res = new Dictionary<string, object>();
            await Task.Run(() =>
            {
                SelectedUser.UserTypes.AsEnumerable().ForEach(ut =>
                {
                    res.Add(ut.UserTypeName, ut.UserTypeId.ToString());
                });

            });
            SelectedUserUserTypesDictionary = new Dictionary<string, object>(res);     
        }
        private RelayCommand _saveSmsSettingsCommand;
        public RelayCommand SaveSmsSettingsCommand
        {
            get
            {
                return _saveSmsSettingsCommand
                    ?? (_saveSmsSettingsCommand = new RelayCommand(
                    () =>
                    {
                        try
                        {
                            ParameterManager.SetValue(ParameterNames.SMSCenterNumber, CenterDeMessagerie);
                            ParameterManager.SetValue(ParameterNames.DelayBetweenATCommand, BetweenAtCmdDelay);
                            ParameterManager.SetValue(ParameterNames.SMSBodyTemplate, SmsBodyTemplate);
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine(ex.Message);
                        }
                        SaveButtonBackground = new SolidColorBrush(Colors.WhiteSmoke);
                    }));
            }
        }
        private RelayCommand _cancelSaveSmsSettingsCommand;
        public RelayCommand CancelSaveSmsSettingsCommand
        {
            get
            {
                return _cancelSaveSmsSettingsCommand
                    ?? (_cancelSaveSmsSettingsCommand = new RelayCommand(
                    GetSmsSettings));
            }
        }


        private void GetSmsSettings()
        {
            try
            {
                CenterDeMessagerie = ParameterManager.GetValue<string>(ParameterNames.SMSCenterNumber);
                BetweenAtCmdDelay = ParameterManager.GetValue<string>(ParameterNames.DelayBetweenATCommand);
                SmsBodyTemplate = ParameterManager.GetValue<string>(ParameterNames.SMSBodyTemplate);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        #endregion
        #region Ctors and Methods
        public SettingsViewModel(IFrameNavigationService mainFrameNavigationService, IInnerFrameNavigationService innerFrameNavigationService)
            : base(mainFrameNavigationService, innerFrameNavigationService)
        {
            if (DateTimeFormatInfo.CurrentInfo != null)
                MonthsList = new ObservableCollection<string>(DateTimeFormatInfo.CurrentInfo.MonthNames);
            ConnectionSettings = new ConnectionSettings();
        }
        private async Task LoadDoctorsDataGrid()
        {
            DoctorsListCollection = new ObservableCollection<Medecin>(await Task.Run(() => _dbContext.Medecins));
        }
        private async Task LoadUsersList()
        {
            UsersList = new ObservableCollection<User>(await Task.Run(() => _dbContext.Users));
        }

        private async Task LoadUserTypeToAddCollection()
        {
            var res=new Dictionary<string, object>();
            await Task.Run(() =>
            {
                _dbContext.UserTypes.AsEnumerable().ForEach(ut =>
                {
                    res.Add(ut.UserTypeName, ut.UserTypeId.ToString());
                });
            });  
            UserTypeDictionary=new Dictionary<string, object>(res);      
        }
        private async Task LoadUserTypeToFilterCollection()
        {
            UserTypeToFilterCollection = new ObservableCollection<EntityToAdd<UserType>>(await Task.Run(() => _dbContext.UserTypes.Select(x => new EntityToAdd<UserType>()
            {
                Entity = x,
                IsAdded = true
            })));            
        }

        private void InitDragablePropertiesCollection()
        {
            DragablePropertiesCollection = new ObservableCollection<DragableProperty>()
        {
            new DragableProperty()
            {
                PropertyId = App.DpNomPatientId,
                PropertyName = "Nom du patient"
            }, new DragableProperty()
            {
                PropertyId = App.DpPrenomPatientId,
                PropertyName = "Prenom du patient"
            }, new DragableProperty()
            {
                PropertyId = App.DpNomMedecinId,
                PropertyName = "Nom du medecin"
            }, new DragableProperty()
            {
                PropertyId = App.DpPrenomMedecinId,
                PropertyName = "Prenom du medecin"
            }, new DragableProperty()
            {
                PropertyId = App.DpDateRdvId,
                PropertyName = "Date de RDV"
            },new DragableProperty()
            {
                PropertyId = App.DpLieuRdvId,
                PropertyName = "Lieu de RDV"
            },
        };
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

        #endregion

    }

}
