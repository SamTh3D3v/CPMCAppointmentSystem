using System;
using System.Diagnostics;
using System.Windows.Controls;
using CPMCAppointmentSystem.Helpers;
using CPMCAppointmentSystem.ViewModel.StatisticsViewModels;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace CPMCAppointmentSystem.ViewModel
{

    public class ViewModelLocator
    {
        public static FrameNavigationService MainNavigationService;
        public static InnerFrameNavigationService InnerFrameNavigationService;

        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
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
            SimpleIoc.Default.Register<NotificationViewModel>();
            SimpleIoc.Default.Register<StatisticsViewModel>();
            SimpleIoc.Default.Register<PatientsPerSexeChartViewModel>();
            SimpleIoc.Default.Register<MedecinPerPathologyChartViewModel>();
            SimpleIoc.Default.Register<MedecinPerSpecialityChartViewModel>();
            SimpleIoc.Default.Register<PatientPerDateChartViewModel>();
            SimpleIoc.Default.Register<PatientPerPathologyChartViewModel>();
            SimpleIoc.Default.Register<LogViewModel>();
            SimpleIoc.Default.Register<PatientPerWillayaDeResidanceChartViewModel>();
            SimpleIoc.Default.Register<PatientPerAgeChartViewModel>();

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
            InnerFrameNavigationService.Configure(App.CalendarViewKey, new Uri("../View/AppointementViews/CalendarView.xaml", UriKind.Relative));
            InnerFrameNavigationService.Configure(App.SettingsViewKey, new Uri("../View/SettingsViews/SettingsView.xaml", UriKind.Relative));
            InnerFrameNavigationService.Configure(App.DoctorsViewKey, new Uri("../View/DoctorsViews/DoctorsView.xaml", UriKind.Relative));
            InnerFrameNavigationService.Configure(App.MyPatientsViewKey, new Uri("../View/DoctorsViews/MyPatientsView.xaml", UriKind.Relative));
            InnerFrameNavigationService.Configure(App.PathologiesViewKey, new Uri("../View/PathologiesViews/PathologiesView.xaml", UriKind.Relative));
            InnerFrameNavigationService.Configure(App.PatientsViewKey, new Uri("../View/PatienstViews/PatientsView.xaml", UriKind.Relative));
            InnerFrameNavigationService.Configure(App.SpecialityViewKey, new Uri("../View/SpecialitiesViews/SpecialiteView.xaml", UriKind.Relative));
            InnerFrameNavigationService.Configure(App.StatisticsViewKey, new Uri("../View/StatisticsView.xaml", UriKind.Relative));
            InnerFrameNavigationService.Configure(App.NotificationViewKey, new Uri("../View/NotificationView.xaml", UriKind.Relative));
            InnerFrameNavigationService.Configure(App.LogViewKey, new Uri("../View/LogViews/LogView.xaml", UriKind.Relative));
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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public StatisticsViewModel StatisticsViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<StatisticsViewModel>();
            }
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public NotificationViewModel NotificationViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<NotificationViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public PatientsPerSexeChartViewModel PatientsPerSexeChartViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<PatientsPerSexeChartViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public MedecinPerPathologyChartViewModel MedecinPerPathologyChartViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MedecinPerPathologyChartViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public MedecinPerSpecialityChartViewModel MedecinPerSpecialityChartViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MedecinPerSpecialityChartViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public PatientPerDateChartViewModel PatientPerDateChartViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<PatientPerDateChartViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public LogViewModel LogViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<LogViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public PatientPerPathologyChartViewModel PatientPerPathologyChartViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<PatientPerPathologyChartViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public PatientPerWillayaDeResidanceChartViewModel PatientPerWillayaDeResidanceChartViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<PatientPerWillayaDeResidanceChartViewModel>();
            }
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public PatientPerAgeChartViewModel PatientPerAgeChartViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<PatientPerAgeChartViewModel>();
            }
        }
        public static void Cleanup()
        {
        }
    }
}