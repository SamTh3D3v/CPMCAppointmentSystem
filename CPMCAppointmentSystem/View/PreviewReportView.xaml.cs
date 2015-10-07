using System;
using GalaSoft.MvvmLight.Messaging;
using MahApps.Metro.Controls;

namespace CPMCAppointmentSystem.View
{
    public partial class PreviewReportView : MetroWindow
    {
        public PreviewReportView()
        {
            InitializeComponent();
            this.Loaded += (sender, arg) => this.ReportPreviewer.RefreshReport();
            //Messenger.Default.Register<NotificationMessage>(this, (m) =>
            //{

            //    if (m.Notification == "RefreshRevier")
            //    {
            //        this.ReportPreviewer.RefreshReport();
            //    }
            //});                        

        }

        
    }
}
