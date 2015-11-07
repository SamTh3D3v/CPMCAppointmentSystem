using System;
using System.Windows;
using System.Windows.Controls;

namespace CPMCAppointmentSystem.View.DoctorsViews
{
    /// <summary>
    /// Interaction logic for DoctorsView.xaml
    /// </summary>
    public partial class DoctorsView : Page
    {
        public DoctorsView()
        {
            InitializeComponent();            
        }

        private void UserPass_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            if (CbUpdatePass.IsChecked == true)
            {
                if (!String.IsNullOrEmpty(PassBoxUserPass.Password) && PassBoxUserPass.Password.Equals(PassBoxConfirmPass.Password))
                    BuSave.Tag = "yes";
                else
                    BuSave.Tag = "no";
            }
        }
        private void PassCheckBox_OnUnChecked(object sender, RoutedEventArgs e)
        {
            BuSave.Tag = "yes";
        }

        private void PassCheckBox_OnChecked(object sender, RoutedEventArgs e)
        {
            BuSave.Tag = "no";
        }
    }
}
