using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Documents;
using DataLayer.Model;
using GalaSoft.MvvmLight.Messaging;
using MahApps.Metro.Controls;
using Syncfusion.Windows.Reports;
using Syncfusion.Windows.Shared;

namespace CPMCAppointmentSystem.View
{
    public partial class PreviewReportView : MetroWindow
    {
        public PreviewReportView()
        {
            InitializeComponent();
            this.Loaded += (sender, arg) => this.ReportPreviewer.RefreshReport();

            Messenger.Default.Register<Patient>(this, (p) =>
            {

                //ReportPreviewer.DataSources.Add(new Syncfusion.Windows.Reports.ReportDataSource()

                //{

                //    Name = "DataSet1",

                //    Value = new List<Patient>() { p},

                //});
                ReportPreviewer.DataContext = p;
                ReportPreviewer.RefreshReport();

            });

        }


    }
}
