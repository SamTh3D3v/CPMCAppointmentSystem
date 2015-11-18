using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CPMCAppointmentSystem.View
{

    public partial class MainView : Page
    {
        public MainView()
        {
            InitializeComponent();
        }

        private void InnerFrame_OnSourceUpdated(object sender, NavigationEventArgs navigationEventArgs)
        {
            RbCalendar.IsChecked=InnerFrame.Source.ToString().Contains("CalendarView");
            RbPatients.IsChecked = InnerFrame.Source.ToString().Contains("PatientsView");
            RbMedecin.IsChecked = InnerFrame.Source.ToString().Contains("DoctorsView");
            RbPathology.IsChecked = InnerFrame.Source.ToString().Contains("PathologiesView");
            RbSpeciality.IsChecked = InnerFrame.Source.ToString().Contains("SpecialiteView");
            RbNotification.IsChecked = InnerFrame.Source.ToString().Contains("NotificationView");
            RbStatistique.IsChecked = InnerFrame.Source.ToString().Contains("StatisticsView");
            RbParametre.IsChecked = InnerFrame.Source.ToString().Contains("SettingsView");
            RbLog.IsChecked = InnerFrame.Source.ToString().Contains("LogView");
            RbMesPatient.IsChecked = InnerFrame.Source.ToString().Contains("MyPatientsView");

        }

        private int horizontalOffset = 0;
        private void ScrollLeftClick(object sender, RoutedEventArgs e)
        {
            if (MenuGrid.ActualWidth - RootGrid.ActualWidth > horizontalOffset - 120)
            horizontalOffset += 10;
            MenuScrollViewer.ScrollToHorizontalOffset(horizontalOffset);
        }

        private void ScrollRightClick(object sender, RoutedEventArgs e)
        {
            if (horizontalOffset>0)           
            horizontalOffset -= 10;
            MenuScrollViewer.ScrollToHorizontalOffset(horizontalOffset);
        }

      
    }
}
