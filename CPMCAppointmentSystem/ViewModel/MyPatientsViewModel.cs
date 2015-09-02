using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CPMCAppointmentSystem.Helpers;
using DataLayer.Model;

namespace CPMCAppointmentSystem.ViewModel
{
    public class MyPatientsViewModel:NavigableViewModelBase
    {
        #region Fields
        
        
        #endregion
        #region Properties
        
        #endregion
        #region Commands
        
        #endregion
        #region Ctors and Methods
        public MyPatientsViewModel(IFrameNavigationService mainFrameNavigationService, IInnerFrameNavigationService innerFrameNavigationService)
            : base(mainFrameNavigationService, innerFrameNavigationService)
        {
        }
        #endregion        
    }
}
