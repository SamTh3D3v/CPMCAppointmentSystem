using System;
using System.Globalization;
using System.Threading;
using System.Windows;
using GalaSoft.MvvmLight.Threading;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

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
  

        #endregion
        public App():base()
        {
            DispatcherHelper.Initialize();
            Application.Current.Dispatcher.UnhandledException += OnDispatcherUnhandledException;
            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += new UnhandledExceptionEventHandler(DomainUnhandlerEceptionHandler);
            Thread.CurrentThread.CurrentCulture = new CultureInfo("fr");
        }

        public async void OnDispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            var errorMessage = string.Format("An exception occurred: {0}", e.Exception.Message);
            var controller = await ((Application.Current.MainWindow as MetroWindow).ShowMessageAsync("Opération non permise, Details :", errorMessage));
            e.Handled = true;
        }

        public async void DomainUnhandlerEceptionHandler(object sender, UnhandledExceptionEventArgs args)
        {
            var errorMessage = string.Format("An exception occurred: {0}", args.ExceptionObject.ToString());
            var controller = await ((Application.Current.MainWindow as MetroWindow).ShowMessageAsync("Opération non permise, Details :", errorMessage));
        }
    }
}
