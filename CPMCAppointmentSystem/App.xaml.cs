using System;
using System.Windows;
using GalaSoft.MvvmLight.Threading;

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
        #endregion
        static App()
        {
            DispatcherHelper.Initialize();
        }
    }
}
