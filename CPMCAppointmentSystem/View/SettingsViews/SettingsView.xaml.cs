using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using DataLayer.Model;
using GalaSoft.MvvmLight.Messaging;
using Syncfusion.Windows.Converters;
using Path = System.Windows.Shapes.Path;

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


        private void UserPass_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(UserPass.Password) && UserPass.Password.Equals(UserConfirmPass.Password))
                BtnSave.Tag = "yes";
            else
                BtnSave.Tag = "no";

        }
    }
}
