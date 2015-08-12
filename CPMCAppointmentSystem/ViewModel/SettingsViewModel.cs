using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CPMCAppointmentSystem.Helpers;

namespace CPMCAppointmentSystem.ViewModel
{
    class SettingsViewModel:NavigableViewModelBase
    {
        #region Fields
        
        #endregion
        #region Properties
        
        #endregion
        #region Commands
        
        #endregion
        #region Ctors and Methods
        public SettingsViewModel(IFrameNavigationService mainFrameNavigationService, IFrameNavigationService innerFrameNavigationService)
            : base(mainFrameNavigationService, innerFrameNavigationService)
        {
        }
        #endregion        
    }
}
