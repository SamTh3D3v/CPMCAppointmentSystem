using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using Syncfusion.Windows.Reports.Designer;

namespace CPMCAppointmentSystem.View.SettingsViews
{
    public partial class ReportEditorView : MetroWindow
    {
        private string _reportPath;
        public ReportEditorView(string reportPath)
        {
            InitializeComponent();
            _reportPath = reportPath;
        }

        private void ReportEditor_Loded(object sender, RoutedEventArgs e)
        {
            this.ReportDesignerControl.OpenReport(_reportPath);            
        }

        private void AllReportClosedEventHandler(object sender, AllReportsClosedEventArgs e)
        {
            this.Close();
        }
    }
}
