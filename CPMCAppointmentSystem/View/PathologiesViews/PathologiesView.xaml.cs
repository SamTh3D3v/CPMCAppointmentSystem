using System.Windows.Controls;

namespace CPMCAppointmentSystem.View.PathologiesViews
{
    /// <summary>
    /// Interaction logic for PathologiesView.xaml
    /// </summary>
    public partial class PathologiesView : Page
    {
        private int _errorsCount;
        public PathologiesView()
        {
            InitializeComponent();
        }

        private void NomBox_OnError(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
                _errorsCount++;
            else
                _errorsCount--;
        }
    }
}
