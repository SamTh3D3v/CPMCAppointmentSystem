using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CPMCAppointmentSystem.Helpers;
using GalaSoft.MvvmLight.Command;

namespace CPMCAppointmentSystem.ViewModel
{
    public class LogViewModel : NavigableViewModelBase
    {
        #region Fileds
        
        #endregion
        #region Properties
        
        #endregion 
        #region Commands
        private RelayCommand _logViewLoadedCommand;
        public RelayCommand LogViewLoadedCommand
        {
            get
            {
                return _logViewLoadedCommand
                    ?? (_logViewLoadedCommand = new RelayCommand(
                    () =>
                    {
                        
                    }));
            }
        }
        #endregion
        #region Ctors and Methods
        public LogViewModel(IFrameNavigationService mainFrameNavigationService, IInnerFrameNavigationService innerFrameNavigationService)
            : base(mainFrameNavigationService, innerFrameNavigationService)
        {
        }
        #endregion       
    }
}
