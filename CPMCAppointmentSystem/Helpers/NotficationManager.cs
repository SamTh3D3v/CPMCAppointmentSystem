using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Model;
using GalaSoft.MvvmLight.Messaging;

namespace CPMCAppointmentSystem.Helpers
{
    public static class NotficationManager
    {
        public static void AddNotification(Notification notification)
        {
            Messenger.Default.Send<Notification>(notification, "AddNotification");          
        }

        public static void RemoveNotification(Notification notification)
        {
            Messenger.Default.Send<Notification>(notification, "RemoveNotification");
        }
    }
}
