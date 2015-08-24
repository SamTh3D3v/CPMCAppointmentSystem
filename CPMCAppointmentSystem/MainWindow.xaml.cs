using System.Windows;
using CPMCAppointmentSystem.ViewModel;
using Syncfusion.Windows.Shared;

namespace CPMCAppointmentSystem
{

    public partial class MainWindow : ChromelessWindow
    {
   
        public MainWindow()
        {
            InitializeComponent();
            Closing += (s, e) => ViewModelLocator.Cleanup();
        }
    }
}