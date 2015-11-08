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
using GalaSoft.MvvmLight.Messaging;

namespace CPMCAppointmentSystem.View
{
    public partial class NotificationView : Page
    {
        public NotificationView()
        {
            InitializeComponent();
        }
        #region redirect click event to Vm (due to a bug in syncfusion SfRadialMenu control)
        private void SendSmsToPatentClick(object sender, RoutedEventArgs e)
        {
            RadialContextMenu.IsOpen = !RadialContextMenu.IsOpen;
            Messenger.Default.Send(new NotificationMessage("SendSmsToPatient"));            
        }
        private void CallFixClick(object sender, RoutedEventArgs e)
        {
            RadialContextMenu.IsOpen = !RadialContextMenu.IsOpen;
            Messenger.Default.Send(new NotificationMessage("CallFix"));            
        }

        private void SendSmsToPatentAccompClick(object sender, RoutedEventArgs e)
        {
            RadialContextMenu.IsOpen = !RadialContextMenu.IsOpen;
            Messenger.Default.Send(new NotificationMessage("SendSmsToAccom"));            
        }

        private void CallPatentClick(object sender, RoutedEventArgs e)
        {
            RadialContextMenu.IsOpen = !RadialContextMenu.IsOpen;
            Messenger.Default.Send(new NotificationMessage("CallPatient"));            
        }

        private void CallAccompClick(object sender, RoutedEventArgs e)
        {
            RadialContextMenu.IsOpen = !RadialContextMenu.IsOpen;
            Messenger.Default.Send(new NotificationMessage("CallAccompagnant"));
            
        } 
        #endregion
    }
}
