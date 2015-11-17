using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CPMCAppointmentSystem.View;
using DataLayer.Model;
using GalaSoft.MvvmLight.Messaging;

namespace CPMCAppointmentSystem.Helpers
{
    public static class NotficationManager
    {
        private const double TopOffset = 20;
        private const double LeftOffset = 380;
        static readonly NotificationStackWindow NotificationStackWindow = new NotificationStackWindow();

        public static void AddNotification(Notification notification)
        {
            NotificationStackWindow.Top = SystemParameters.WorkArea.Top + TopOffset;
            NotificationStackWindow.Left = SystemParameters.WorkArea.Left + SystemParameters.WorkArea.Width - LeftOffset;
   
            Messenger.Default.Send<Notification>(notification, "AddNotification");
            NotificationStackWindow.AddNotification(new NotificationPopUp(){ Title = "Mesage #1", ImageUrl = "pack://application:,,,/Resources/notification-icon.png", Message = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua." });
        }

        public static void RemoveNotification(Notification notification)
        {
            Messenger.Default.Send<Notification>(notification, "RemoveNotification");
        }
    }
}
