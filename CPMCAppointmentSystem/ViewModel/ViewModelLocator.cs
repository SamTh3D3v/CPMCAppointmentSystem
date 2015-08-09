using System;
using System.Diagnostics;
using System.Windows.Controls;
using CPMCAppointmentSystem.Helpers;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using CPMCAppointmentSystem.Model;

namespace CPMCAppointmentSystem.ViewModel
{
    
    public class ViewModelLocator
    {
        public static FrameNavigationService MainNavigationService;
        public static FrameNavigationService InnerFrameNavigationService;

        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            if (ViewModelBase.IsInDesignModeStatic)
            {
                SimpleIoc.Default.Register<IDataService, Design.DesignDataService>();
            }
            else
            {
                SimpleIoc.Default.Register<IDataService, DataService>();
            }

            SimpleIoc.Default.Register<MainWindowViewModel>();
            SimpleIoc.Default.Register<LoginViewModel>();  
            SetupMainNavigationService();                       
           // SetupInnerNavigationService();            
        }
        private static void SetupMainNavigationService()
        {
            MainNavigationService = new FrameNavigationService("MainFrame");
            MainNavigationService.Configure(App.LoginViewKey, new Uri("../View/LoginView.xaml", UriKind.Relative));
            MainNavigationService.Configure(App.MainViewKey, new Uri("../View/MainView.xaml", UriKind.Relative));
            SimpleIoc.Default.Register<IFrameNavigationService>(() => MainNavigationService);
        }
        private static void SetupInnerNavigationService()
        {
            InnerFrameNavigationService = new FrameNavigationService("InnerFrame");
            InnerFrameNavigationService.Configure(App.CalendarViewKey,new Uri("../View/CalendarView.xaml",UriKind.Relative));
            InnerFrameNavigationService.Configure(App.SettingsViewKey,new Uri("../View/SettingsView.xaml",UriKind.Relative));
            SimpleIoc.Default.Register<IFrameNavigationService>(()=>InnerFrameNavigationService);
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public MainWindowViewModel MainWindow
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainWindowViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public LoginViewModel LoginViewLodel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<LoginViewModel>();
            }
        }
        public static void Cleanup()
        {
        }
    }
}