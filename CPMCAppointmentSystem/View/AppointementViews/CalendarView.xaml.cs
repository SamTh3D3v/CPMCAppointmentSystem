using CPMCAppointmentSystem.Helpers;
using DataLayer.Model;
using GalaSoft.MvvmLight.Messaging;
using Syncfusion.UI.Xaml.Schedule;
using Syncfusion.Windows.Controls.Navigation;
using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CPMCAppointmentSystem.View.AppointementViews
{
    /// <summary>
    /// Interaction logic for CalendarView.xaml
    /// </summary>
    public partial class CalendarView : Page
    {
        internal RendezVous SelectedAppointment;
        RendezVous copiedAppointment;
        DateTime CurrentSelectedDate;
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

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                RadialPopup.IsOpen = false;
            
        }

        private void Schedule_OnAppointmentEditorOpening(object sender, AppointmentEditorOpeningEventArgs e)
        {
            e.Cancel = true;
        }
        private void Schedule_OnScheduleClick(object sender, ScheduleClickEventArgs e)
        {
            //A Dirty Trick from the deep hell of dirty coders 
            if (e.SelectedDate != null) Messenger.Default.Send<DateTime>((DateTime)e.SelectedDate);
        }

        #region Popup Menu Click Events

        void pasteButton_Click(object sender, RoutedEventArgs e)
        {
            RadialPopup.IsOpen = false;
        }

        void cutButton_Click(object sender, RoutedEventArgs e)
        {
            RadialPopup.IsOpen = false;
            copiedAppointment = (RendezVous)Schedule.SelectedAppointment;
        }

        void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            RadialPopup.IsOpen = false;
        }

        void editButton_Click(object sender, RoutedEventArgs e)
        {
            RadialPopup.IsOpen = false;
        }

        void addButton_Click(object sender, RoutedEventArgs e)
        {
            RadialPopup.IsOpen = false;
        }

        #endregion

        private void Schedule_OnMouseLeave(object sender, MouseEventArgs e)
        {
            if (!RadialPopup.IsMouseOver && RadialPopup.IsOpen)
                RadialPopup.IsOpen = false;
        }

        private void Schedule_OnContextMenuOpening(object sender, ContextMenuOpeningEventArgs e)
        {
            if (e.CurrentSelectedDate != null) Messenger.Default.Send<DateTime>((DateTime)e.CurrentSelectedDate);
            RadialPopup.IsOpen = false;
            e.Cancel = true;
            if (RestDayHelper.IsRestDay((DateTime)e.CurrentSelectedDate)) return;


            RadialPopup.IsOpen = true;
            radialMenu.IsOpen = true;
            if (e.CurrentSelectedDate != null)
            {
                CurrentSelectedDate = (DateTime)e.CurrentSelectedDate;
            }
            if (e.Appointment != null)
            {
                for (int i = 0; i < radialMenu.Items.Count; i++)
                {
                    if (i == 3 && copiedAppointment == null)
                    {
                        (radialMenu.Items[i] as SfRadialMenuItem).IsEnabled = false;
                        (radialMenu.Items[i] as SfRadialMenuItem).Opacity = 0.5;
                    }
                    else
                    {
                        (radialMenu.Items[i] as SfRadialMenuItem).IsEnabled = true;
                        (radialMenu.Items[i] as SfRadialMenuItem).Opacity = 1;
                    }
                }

            }
            else
            {
                (radialMenu.Items[1] as SfRadialMenuItem).IsEnabled = false;
                (radialMenu.Items[1] as SfRadialMenuItem).Opacity = 0.5;
                (radialMenu.Items[2] as SfRadialMenuItem).IsEnabled = false;
                (radialMenu.Items[2] as SfRadialMenuItem).Opacity = 0.5;
                (radialMenu.Items[5] as SfRadialMenuItem).IsEnabled = false;
                (radialMenu.Items[5] as SfRadialMenuItem).Opacity = 0.5;
                (radialMenu.Items[0] as SfRadialMenuItem).IsEnabled = true;
                if (copiedAppointment != null)
                {
                    (radialMenu.Items[3] as SfRadialMenuItem).IsEnabled = true;
                    (radialMenu.Items[3] as SfRadialMenuItem).Opacity = 1;
                }
                else
                {
                    (radialMenu.Items[3] as SfRadialMenuItem).IsEnabled = false;
                    (radialMenu.Items[3] as SfRadialMenuItem).Opacity = 0.5;
                }

            }
        }

        private void Calendar_OnSelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CalendarFilter.SelectedDate != null) Schedule.MoveToDate(CalendarFilter.SelectedDate.Value);
        }

        private void Schedule_VisibleDatesChanging(object sender, VisibleDatesChangingEventArgs e)
        {
            //Dispatcher.InvokeAsync(()=> 
            //MessageBox.Show(((Collection<DateTime>)e.NewValue).First() + "-->"+ ((Collection<DateTime>)e.NewValue).Last()));            
        }
    }
}
