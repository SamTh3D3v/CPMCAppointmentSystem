using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CPMCAppointmentSystem.Helpers;

namespace CPMCAppointmentSystem.ViewModel
{
    public class NotificationViewModel:NavigableViewModelBase
    {
        #region Fields
        
        #endregion 
        #region Properties
        
        #endregion 
        #region Commands
        
        #endregion 
        #region Ctors and methods
        public NotificationViewModel(IFrameNavigationService mainFrameNavigationService, IInnerFrameNavigationService innerFrameNavigationService)
            : base(mainFrameNavigationService, innerFrameNavigationService)
        {
        }
        #endregion       
    }
}
