using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using CPMCAppointmentSystem.ViewModel;
using GalaSoft.MvvmLight.Messaging;
using MahApps.Metro.Controls;
using Syncfusion.Windows.Shared;

namespace CPMCAppointmentSystem
{

    public partial class MainWindow : MetroWindow
    {
   
        public MainWindow()
        {
            InitializeComponent();
            Closing += (s, e) => ViewModelLocator.Cleanup();
            #region View Related Logic
            Messenger.Default.Register<NotificationMessage>(this, (message) =>
            {
                switch (message.Notification)
                {
                    case "OpenNotificationFlayout":
                        NotificationFlyout.IsOpen = true;
                        break;
                    case "ShowCurrentUserFlayout":
                        CurrentUserFlyout.IsOpen = true;
                        break;
                }
            });
            #endregion
        }

      

        private void MainFrame_OnContentRendered(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ShowDataBaesSettingsOnClick(object sender, RoutedEventArgs e)
        {
            if (!DataBaseSettingsFlyout.IsOpen)
                DataBaseSettingsFlyout.IsOpen = true;

        }

        private void MainWindow_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (NotificationFlyout.IsOpen) NotificationFlyout.IsOpen = false;
            if (DataBaseSettingsFlyout.IsOpen) DataBaseSettingsFlyout.IsOpen = false;
            if (CurrentUserFlyout.IsOpen) CurrentUserFlyout.IsOpen = false;
        }

        private void DataBaseSettingsFlyout_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }
    }
}