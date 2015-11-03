using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using GalaSoft.MvvmLight.Messaging;
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
            Messenger.Default.Register<NotificationMessage>(this, (m) =>
            {
                switch (m.Notification)
                {
                    case "Refresh":                        
                        Schedule.Refresh();                                         
                        break;
                }
                
            });            
        }

        private void Schedule_OnAppointmentEditorOpening(object sender, AppointmentEditorOpeningEventArgs e)
        {
            e.Cancel = true;                           
        }
        private void Schedule_OnScheduleClick(object sender, ScheduleClickEventArgs e)
        {
            //A Dirty Trick from the deep hell of dirty coders 
            Messenger.Default.Send<DateTime>((DateTime) e.SelectedDate);
        }


        private void Schedule_OnAppointmentEndDragging(object sender, AppointmentEndDraggingEventArgs e)
        {
            
        }
    }
}
