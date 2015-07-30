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
        #endregion
        static App()
        {
            DispatcherHelper.Initialize();
        }
    }
}
