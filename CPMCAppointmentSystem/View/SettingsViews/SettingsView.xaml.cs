using System.Windows.Controls;
using GalaSoft.MvvmLight.Messaging;

namespace CPMCAppointmentSystem.View.SettingsViews
{    
    public partial class SettingsView : Page
    {
        public SettingsView()
        {
            InitializeComponent();
            Messenger.Default.Register<NotificationMessage>(this, (m) =>
            {
                switch (m.Notification)
                {
                    case "Refresh":
                        ReportPreviewer.RefreshReport();     
                        break;
                }
            });
        }

       
    }
}
