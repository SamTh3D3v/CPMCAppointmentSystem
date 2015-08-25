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
        public static InnerFrameNavigationService InnerFrameNavigationService;

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
            SimpleIoc.Default.Register<CalendarViewModel>();
            SimpleIoc.Default.Register<DoctorsViewModel>();
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<MyPatientsViewModel>();
            SimpleIoc.Default.Register<PathologiesViewModel>();
            SimpleIoc.Default.Register<PatientsViewModel>();
            SimpleIoc.Default.Register<SettingsViewModel>();
            SimpleIoc.Default.Register<SpecialityViewModel>();
            SetupMainNavigationService();
            SetupInnerNavigationService();
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
            InnerFrameNavigationService = new InnerFrameNavigationService("InnerFrame");
            InnerFrameNavigationService.Configure(App.CalendarViewKey, new Uri("../View/CalendarView.xaml", UriKind.Relative));
            InnerFrameNavigationService.Configure(App.SettingsViewKey, new Uri("../View/SettingsView.xaml", UriKind.Relative));
            InnerFrameNavigationService.Configure(App.DoctorsViewKey, new Uri("../View/DoctorsView.xaml", UriKind.Relative));
            InnerFrameNavigationService.Configure(App.MyPatientsViewKey, new Uri("../View/MyPatientsView.xaml", UriKind.Relative));
            InnerFrameNavigationService.Configure(App.PathologiesViewKey, new Uri("../View/PathologiesView.xaml", UriKind.Relative));
            InnerFrameNavigationService.Configure(App.PatientsViewKey, new Uri("../View/PatientsView.xaml", UriKind.Relative));
            InnerFrameNavigationService.Configure(App.SpecialityViewKey, new Uri("../View/SpecialiteView.xaml", UriKind.Relative));
            SimpleIoc.Default.Register<IInnerFrameNavigationService>(() => InnerFrameNavigationService);
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


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public CalendarViewModel CalendarViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CalendarViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public DoctorsViewModel DoctorsViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<DoctorsViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public MainViewModel MainViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public MyPatientsViewModel MyPatientsViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MyPatientsViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public PathologiesViewModel PathologiesViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<PathologiesViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public PatientsViewModel PatientsViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<PatientsViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public SettingsViewModel SettingsViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SettingsViewModel>();
            }
        }
       
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public SpecialityViewModel SpecialityViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SpecialityViewModel>();
            }
        }
        public static void Cleanup()
        {
        }
    }
}