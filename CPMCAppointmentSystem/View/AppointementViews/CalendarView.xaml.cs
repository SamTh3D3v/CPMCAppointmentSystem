using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Syncfusion.UI.Xaml.Schedule;
using Syncfusion.Windows.Forms.Tools.Navigation;

namespace CPMCAppointmentSystem.View.AppointementViews
{
    /// <summary>
    /// Interaction logic for CalendarView.xaml
    /// </summary>
    public partial class CalendarView : Page
    {
        public CalendarView()
        {
            InitializeComponent();

            Schedule.Appointments = new ScheduleAppointmentCollection
            {
                new ScheduleAppointment()
                {
                    Status =
                        new ScheduleAppointmentStatus() {Brush = new SolidColorBrush(Colors.Green), Status = "Free"},
                    StartTime = new DateTime(2015, 10, 12, 5, 0, 0),
                    EndTime = new DateTime(2015, 10, 12, 5, 30, 0),
                    Subject = "Meet the doc",
                    Location = "Hutchison road",
                    AllDay = false
                }
            };

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

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void copyButton_Click(object sender, RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void pasteButton_Click(object sender, RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void Schedule_OnAppointmentEditorOpening(object sender, AppointmentEditorOpeningEventArgs e)
        {
            e.Cancel = true;
            AddAppointementView _add=new AddAppointementView();
            _add.ShowDialog();
        }
    }
}
