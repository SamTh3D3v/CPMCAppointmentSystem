using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;
using DataLayer.Model;
using GalaSoft.MvvmLight.Messaging;
using Syncfusion.UI.Xaml.Grid;
using Syncfusion.Windows.Converters;
using Path = System.Windows.Shapes.Path;
using SelectionChangedEventArgs = System.Windows.Controls.SelectionChangedEventArgs;

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
                    case "Refresh   ":
                        ReportPreviewer.RefreshReport();
                        break;
                }
            });

        }

        private void UserPass_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            if (PassCheckBox.IsChecked == true) { 
            if (!String.IsNullOrEmpty(UserPass.Password) && UserPass.Password.Equals(UserConfirmPass.Password))
                BtnSave.Tag = "yes";
            else
                BtnSave.Tag = "no";
            }
        }

        private void SfDataGrid_OnSelectionChanged(object sender, GridSelectionChangedEventArgs e)
        {
            PassCheckBox.IsChecked = false;
            BtnSave.Tag = "yes";
            UserPass.Clear();
            UserConfirmPass.Clear();
            
        }

        private void PassCheckBox_OnUnChecked(object sender, RoutedEventArgs e)
        {            
                BtnSave.Tag = "yes";
        }

        private void PassCheckBox_OnChecked(object sender, RoutedEventArgs e)
        {
            BtnSave.Tag = "no";
        }
        private void HandleOnTreeViewAfterCheck(Object sender,
      TreeViewEventArgs e)
        {
            CheckTreeViewNode(e.Node, e.Node.Checked);
        }

        private void CheckTreeViewNode(TreeNode node, Boolean isChecked)
        {
            foreach (TreeNode item in node.Nodes)
            {
                item.Checked = isChecked;

                if (item.Nodes.Count > 0)
                {
                    this.CheckTreeViewNode(item, isChecked);
                }
            }
        }

       
    }
}
