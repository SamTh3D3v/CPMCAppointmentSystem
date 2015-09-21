using System.Windows;
using CPMCAppointmentSystem.ViewModel;
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
        }
    }
}