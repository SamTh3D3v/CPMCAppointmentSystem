using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;
using DataLayer.Model;
using GalaSoft.MvvmLight.Messaging;
using Syncfusion.UI.Xaml.Grid;
using Syncfusion.Windows.Converters;
using Syncfusion.Windows.Reports;
using Syncfusion.Windows.Reports.Viewer;
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
                    case "RefreshRecuDepo":                                              
                        ReportPreviewer.ReportPath = App.RecuDeDepotReport;
                        ReportParameter[]  parms = new ReportParameter[2];
                        parms[0] = new ReportParameter()
                        {
                            Name = "NumeroDordrePara",
                            Values = new List<string>() { "<Noméro d'ordre>" }

                        };
                        parms[1] = new ReportParameter()
                        {
                            Name = "DateDepot",
                            Values = new List<string>() { "<Date de dépot>" }

                        };
                        ReportPreviewer.SetParameters(parms);
                        ReportPreviewer.RefreshReport();
                        ReportPreviewer.Refresh();                        
                        break;
                    case "RefreshRdv":                                            
                        ReportPreviewer.ReportPath =  App.RendezVousReport;
                        ReportParameter[]  parmsRdv = new ReportParameter[6];
                        parmsRdv[0] = new ReportParameter()
                        {
                            Name = "NomPatient",
                            Values = new List<string>() { "<Nom du patient>" }

                        };
                        parmsRdv[1] = new ReportParameter()
                        {
                            Name = "PrenomParient",
                            Values = new List<string>() { "<Prenom du patient>" }

                        };
                        parmsRdv[2] = new ReportParameter()
                        {
                            Name = "NomMedecin",
                            Values = new List<string>() { "<Nom du medecin>" }

                        };
                        parmsRdv[3] = new ReportParameter()
                        {
                            Name = "PrenomMedecin",
                            Values = new List<string>() { "<Prenom du medecin>" }

                        };
                        parmsRdv[4] = new ReportParameter()
                        {
                            Name = "DateRdv",
                            Values = new List<string>() { "<Date du rendez-vous>" }

                        };
                        parmsRdv[5] = new ReportParameter()
                        {
                            Name = "LieuRdv",
                            Values = new List<string>() { "<Lieu du rendez-vous>" }

                        };
                        ReportPreviewer.SetParameters(parmsRdv);
                        ReportPreviewer.RefreshReport();
                        ReportPreviewer.Refresh();
                        break;
                }
            });

        }

        private void UserPass_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            if (PassCheckBox.IsChecked == true)
            {
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

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            PassCheckBox.IsChecked = true;
            BtnSave.Tag = "no";
            UserPass.Clear();
            UserConfirmPass.Clear();
        }


        
    }
}
