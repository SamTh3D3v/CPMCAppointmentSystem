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
            Messenger.Default.Register<String>(this, "enableLoading", (message) =>
            {
                Application.Current.Dispatcher.BeginInvoke(new Action(async () =>
                {
                    TxtLoadingMessage.Text = message;
                    ProgressBarLoading.IsIndeterminate = true;
                }));

            });
            Messenger.Default.Register<String>(this, "desableLoading", (message) =>
            {
               Application.Current.Dispatcher.BeginInvoke(new Action(async () =>
              {
                  TxtLoadingMessage.Text = message;
                  ProgressBarLoading.IsIndeterminate = false;
              }));
            });
            #endregion
        }
        private void MainFrame_OnContentRendered(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ShowDataBaesSettingsOnClick(object sender, RoutedEventArgs e)
        {
            DataBaseSettingsFlyout.IsOpen = !DataBaseSettingsFlyout.IsOpen;
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

        private void UserPass_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(PbUser.Password) && PbUser.Password.Equals(PbUserConfirme.Password))
                BtnSave.IsEnabled = true;
            else
                BtnSave.IsEnabled = false;
        }

        private void BtnCancel_OnClick(object sender, RoutedEventArgs e)
        {
            PbUser.Clear();
            PbUserConfirme.Clear();
        }

        private void BtnSave_OnClick(object sender, RoutedEventArgs e)
        {
            CurrentUserFlyout.IsOpen = false;
        }
    }
}