using System;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using CPMCAppointmentSystem.SubModel;
using DataLayer.Notifications;
using GalaSoft.MvvmLight.Threading;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Configuration;

namespace CPMCAppointmentSystem
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region Global keys
        public static String LoginViewKey = "LoginView";
        public static String MainViewKey = "MainView";
        public static String CalendarViewKey = "CalendarView";
        public static String SettingsViewKey = "SettingsView";
        public static String DoctorsViewKey = "DoctorsView";
        public static String MyPatientsViewKey = "MyPatientsView";
        public static String PathologiesViewKey = "PathologiesView";
        public static String PatientsViewKey = "PatientsView";
        public static String SpecialityViewKey = "SpecialityView";
        public static String StatisticsViewKey = "StatisticsView";
        public static String NotificationViewKey = "NotificationView";
        public static String LogViewKey = "LogView";
        public static String RecuDeDepotReport = "Reports/RecuDeDepot.rdlc";
        public static String RendezVousReport = "Reports/Rendez_Vous.rdlc";

        public static String Admin = "Admin";
        public static String Medecin = "Medecin";
        public static String Agent = "Agent";        

        public const string DpNomPatientId = "[@NomPatient]";
        public const string DpPrenomPatientId = "[@PrenomPatient]";
        public const string DpNomMedecinId = "[@NomMedecin]";
        public const string DpPrenomMedecinId = "[@PrenomMedecin]";
        public const string DpDateRdvId = "[@DateRdv]";
        public const string DpLieuRdvId = "[@LieuRdv]";
        

        private static NotificationHelper _sqlHelper;

        public static NotificationHelper NotificationHelper
        {
            get
            {
                _sqlHelper = _sqlHelper ?? new NotificationHelper();
                return _sqlHelper;
            }
        
        }
        #endregion

        NotificationHelper notifcationHelper = new NotificationHelper();
        public App():base()
        {
            DispatcherHelper.Initialize();
            Application.Current.Dispatcher.UnhandledException += OnDispatcherUnhandledException;          
            AppDomain currentDomain = AppDomain.CurrentDomain;            
            currentDomain.UnhandledException += new UnhandledExceptionEventHandler(DomainUnhandlerEceptionHandler);            
            Thread.CurrentThread.CurrentCulture = new CultureInfo("fr");
           

            //NotificationHelper.Start();
            
            ////TODO : OUSSAMA This line to bedoneon nitification View.
            //NotificationHelper.NotificationsChange += NotificationHelper_NotificationsChange;
        }
       
        //void NotificationHelper_NotificationsChange(object sender, DataLayer.Notifications.NotificationEventArgs<DataLayer.Model.Notification> args)
        //{
        //    //Get Valide notifications
        //    var notifications = args.NewResult;
        //    MessageBox.Show(notifications.Count.ToString(),notifications.Count>0?notifications[0].NotificationTitle:"");
        //}

        public async void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            var errorMessage = String.Format("An exception occurred: {0}", e.Exception.Message);
            var window = (Application.Current.MainWindow as MetroWindow);
            if (window==null)
            {
                Debug.WriteLine(errorMessage);
                e.Handled = true;
                return;
            }
            await (window.ShowMessageAsync("Opération non permise, Details :", errorMessage));
            e.Handled = true;
            
        }

        public async void DomainUnhandlerEceptionHandler(object sender, UnhandledExceptionEventArgs args)
        {
            var errorMessage = String.Format("An exception occurred: {0}", args.ExceptionObject.ToString());
            var controller = await ((Application.Current.MainWindow as MetroWindow).ShowMessageAsync("Opération non permise, Details :", errorMessage));
        }

        //protected override void OnExit(ExitEventArgs e)
        //{
        //    NotificationHelper.Stop();
        //    base.OnExit(e);
        //}

        public static void SaveDateBaseSettings(DataBaseSettings databasesettings, object passwordbox)
        {
            var passwordBox = passwordbox as PasswordBox;
            if (passwordBox != null)
            {                 
                //todo farouk : use databasesettings and pass to generate the connexion string
                #region Connection String Settings

                // Get Configuration File Config.
                Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                // Get Connection String Settings
                ConnectionStringsSection connectionStringSection = configuration.ConnectionStrings;
                
                //if (connectionStringSection.SectionInformation.IsProtected)
                //    connectionStringSection.SectionInformation.UnprotectSection();
                
                ConnectionStringSettings connectionStringSettings = connectionStringSection.ConnectionStrings["CpmcContext"];
                
                System.Data.SqlClient.SqlConnectionStringBuilder connectionStringBuilder = new System.Data.SqlClient.SqlConnectionStringBuilder(connectionStringSettings.ConnectionString);

                // Change connection strings Settings according to escSettings.
                connectionStringBuilder.Clear();
                connectionStringBuilder.DataSource = databasesettings.ServerName;
                connectionStringBuilder.InitialCatalog = databasesettings.InstanceName;
                connectionStringBuilder.UserID = databasesettings.UserName;
                connectionStringBuilder.Password = passwordBox.Password;


                // make changes to configuration file.
                connectionStringSettings.ConnectionString = connectionStringBuilder.ToString();
                if (!connectionStringSection.SectionInformation.IsProtected)
                    connectionStringSection.SectionInformation.ProtectSection("DataProtectionConfigurationProvider");

                configuration.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("connectionStrings");
                #endregion
                

            }
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            NotificationHelper.Stop();
          //  base.OnExit(e); 
        }
    }
}
