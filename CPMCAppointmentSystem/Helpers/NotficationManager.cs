using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CPMCAppointmentSystem.View;
using DataLayer;
using DataLayer.Model;
using GalaSoft.MvvmLight.Messaging;

namespace CPMCAppointmentSystem.Helpers
{
    public static class NotficationManager
    {
        private const double TopOffset = 20;
        private const double LeftOffset = 380;
        static readonly NotificationStackWindow NotificationStackWindow = new NotificationStackWindow();

        public static void AddNotification(Notification notification,bool globalNotification)
        {
            NotificationStackWindow.Top = SystemParameters.WorkArea.Top + TopOffset;
            NotificationStackWindow.Left = SystemParameters.WorkArea.Left + SystemParameters.WorkArea.Width - LeftOffset;
            if (globalNotification)
            {
                using (var db = new CpmcContext())
                {
                    db.Notifications.Add(notification);
                    db.SaveChanges();
                }
            }
            //Messenger.Default.Send<Notification>(notification, "AddNotification");
            NotificationStackWindow.AddNotification(notification);
        }

        public static void RemoveNotification(Notification notification)
        {
            Messenger.Default.Send<Notification>(notification, "RemoveNotification");
        }
    }
}
