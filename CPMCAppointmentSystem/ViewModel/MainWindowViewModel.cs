using GalaSoft.MvvmLight;
using CPMCAppointmentSystem.Model;

namespace CPMCAppointmentSystem.ViewModel
{

    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;   
        public MainWindowViewModel(IDataService dataService)
        {
            _dataService = dataService;
            _dataService.GetData(
                (item, error) =>
                {
                    if (error != null)
                    {
                        // Report error here
                        return;
                    }
                    
                });
        }

        public override void Cleanup()
        {
            // Clean up if needed

            base.Cleanup();
        }
    }
}