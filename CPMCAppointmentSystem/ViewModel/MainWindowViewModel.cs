using DataLayer.Model;
using GalaSoft.MvvmLight;
using CPMCAppointmentSystem.Model;
using GalaSoft.MvvmLight.Command;

namespace CPMCAppointmentSystem.ViewModel
{

    public class MainWindowViewModel : ViewModelBase
    {
        #region Fields
        
        #endregion
        #region Properties
        
        #endregion
        #region Commands
        private RelayCommand _mainViewLoadedCommand;
        public RelayCommand MainViewLoadedCommand
        {
            get
            {
                return _mainViewLoadedCommand
                    ?? (_mainViewLoadedCommand = new RelayCommand(
                    () =>
                    {
                        using (var context=new CpmcContext())
                        {
                            context.Willayas.Add(new Willaya()
                            {
                                WillayaId = 16,
                                Designation = "Alger"
                            });
                        }
                        
                        
                    }));
            }
        }
        #endregion
        #region Ctors and Methods
        
        #endregion
        
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