using System.Windows.Input;
using CPMCAppointmentSystem.Helpers;
using DataLayer.Model;
using GalaSoft.MvvmLight;
using CPMCAppointmentSystem.Model;
using GalaSoft.MvvmLight.Command;

namespace CPMCAppointmentSystem.ViewModel
{

    public class MainWindowViewModel : NavigableViewModelBase
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
                        MainFrameNavigationService.NavigateTo(App.LoginViewKey);
                        using (var context = new CpmcContext())
                        {                            
                            var res=context.Patients;
                        }
                    }));
            }
        }
        #endregion
        #region Ctors and Methods

        #endregion

        private readonly IDataService _dataService;
        public MainWindowViewModel(IFrameNavigationService mainFrameNavigationService, IInnerFrameNavigationService innerFrameNavigationService)
            : base(mainFrameNavigationService,innerFrameNavigationService)
        {

        }

        public override void Cleanup()
        {
            // Clean up if needed

            base.Cleanup();
        }
    }
}