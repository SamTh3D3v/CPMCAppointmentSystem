using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Notifications
{
    public delegate void NotificationEventHandler<T>(object sender,NotificationEventArgs<T> args);
    public class NotificationEventArgs<T> : EventArgs
    {
        public NotificationEventArgs(List<T> newResult)
        {
            NewResult = newResult;
        }
        public List<T> NewResult { get; private set; }
    }
}
