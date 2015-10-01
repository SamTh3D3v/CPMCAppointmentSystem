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
using Syncfusion.UI.Xaml.Schedule;

namespace CPMCAppointmentSystem.View
{
    /// <summary>
    /// Interaction logic for CalendarView.xaml
    /// </summary>
    public partial class CalendarView : Page
    {
        public CalendarView()
        {
            InitializeComponent();
        }

        private void Btn_ScheduleType_Click(object sender, RoutedEventArgs e)
        {
            switch ((sender as RadioButton).Name)
            {
                case "Day":
                    {
                        Schedule.ScheduleType = ScheduleType.Day;                        
                        break;
                    }
                case "Week":
                    {
                        Schedule.ScheduleType = ScheduleType.Week;
                        break;
                    }
                case "WorkWeek":
                    {
                        Schedule.ScheduleType = ScheduleType.WorkWeek;
                        break;
                    }
                case "Month":
                    {
                        Schedule.ScheduleType = ScheduleType.Month;
                        break;
                    }
                case "TimeLine":
                    {
                        Schedule.ScheduleType = ScheduleType.TimeLine;
                        break;
                    }
            }
        }
    }
}
