using System.Collections.Generic;
using DataLayer.Model;
using GalaSoft.MvvmLight.Messaging;
using MahApps.Metro.Controls;
using Syncfusion.Windows.Reports;

namespace CPMCAppointmentSystem.View.PatienstViews
{
    public partial class PreviewReportView : MetroWindow
    {
        public PreviewReportView()
        {
            InitializeComponent();
            this.Loaded += (sender, arg) => this.ReportPreviewer.RefreshReport();

            Messenger.Default.Register<Patient>(this, (p) =>
            {
               
             
                ReportParameter[] parms = new ReportParameter[2];
                parms[0] = new ReportParameter()
                {
                    Name = "NumeroDordrePara",
                    Values =new List<string>(){p.NumeroDordre}
                    
                }; 
                parms[1] = new ReportParameter()
                {
                    Name = "DateDepot",
                    Values =new List<string>(){p.DateDeDepot.Date.ToString("dd/MM/yyyy")}
                    
                };
                ReportPreviewer.SetParameters(parms);
                ReportPreviewer.RefreshReport();                                                                                                         

            });
            Messenger.Default.Register<RendezVous>(this, (r) =>
            {
               
             
                //ReportParameter[] parms = new ReportParameter[2];
                //parms[0] = new ReportParameter()
                //{
                //    Name = "NumeroDordrePara",
                //    Values =new List<string>(){p.NumeroDordre}
                    
                //}; 
                //parms[1] = new ReportParameter()
                //{
                //    Name = "DateDepot",
                //    Values =new List<string>(){p.DateDeDepot.Date.ToString("dd/MM/yyyy")}
                    
                //};
                //ReportPreviewer.SetParameters(parms);
                ReportPreviewer.RefreshReport();                                                                                                         

            });
            

        }


    }
}
