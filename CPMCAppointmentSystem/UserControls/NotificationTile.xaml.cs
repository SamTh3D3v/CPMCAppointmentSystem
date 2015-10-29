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
using CPMCAppointmentSystem.Helpers;
using DataLayer.Model;

namespace CPMCAppointmentSystem.UserControls
{

    public partial class NotificationTile : UserControl
    {
        public NotificationTile()
        {
            InitializeComponent();
        }

        private void RemoveNotificationClick(object sender, RoutedEventArgs e)
        {
            NotficationManager.RemoveNotification(this.DataContext as Notification);
        }
    }
}
