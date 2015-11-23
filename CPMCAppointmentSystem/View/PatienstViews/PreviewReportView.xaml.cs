using System.Collections.Generic;
using DataLayer.Model;
using GalaSoft.MvvmLight.Messaging;
using MahApps.Metro.Controls;
using Syncfusion.Windows.Reports;

namespace CPMCAppointmentSystem.View.PatienstViews
{
    public partial class PreviewReportView : MetroWindow
    {
        public PreviewReportView(object obj)
        {
            InitializeComponent();
            this.Loaded += (sender, arg) => this.ReportPreviewer.RefreshReport();
            
            if (obj.GetType().Name.StartsWith("Patient"))
            {
                ReportParameter[] parms = new ReportParameter[2];
                var p = obj as Patient;
                parms[0] = new ReportParameter()
                {
                    Name = "NumeroDordrePara",
                    Values = new List<string>() {p.NumeroDordre}

                };
                parms[1] = new ReportParameter()
                {
                    Name = "DateDepot",
                    Values = new List<string>() {p.DateDeDepot.Date.ToString("dd/MM/yyyy")}

                };
                ReportPreviewer.SetParameters(parms);
            }
          
            if (obj.GetType().Name.StartsWith("RendezVous"))
            {
                ReportParameter[] parms = new ReportParameter[6];
                var r = obj as RendezVous;
                parms[0] = new ReportParameter()
                {
                    Name = "NomPatient",
                    Values = new List<string>() {r.Patient.Nom}

                };
                parms[1] = new ReportParameter()
                {
                    Name = "PrenomParient",
                    Values = new List<string>() {r.Patient.Prenom}

                };
                parms[2] = new ReportParameter()
                {
                    Name = "NomMedecin",
                    Values = new List<string>() {r.Medecin.User.UserNom}

                };
                parms[3] = new ReportParameter()
                {
                    Name = "PrenomMedecin",
                    Values = new List<string>() {r.Medecin.User.UserPrenom}

                };
                parms[4] = new ReportParameter()
                {
                    Name = "DateRdv",
                    Values = new List<string>() {r.DateTimeRdv.ToString("dd/MM/yyyy")}

                };
                parms[5] = new ReportParameter()
                {
                    Name = "LieuRdv",
                    Values = new List<string>() {r.LieuRdv}

                };
                ReportPreviewer.SetParameters(parms);
            }                
        }
    }
}
