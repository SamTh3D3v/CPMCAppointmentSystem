using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace CPMCAppointmentSystem.Helpers
{
    public class NavigableViewModelBase:ViewModelBase
    {
        #region Fields

        protected IFrameNavigationService MainFrameNavigationService;
        protected IFrameNavigationService InnerFrameNavigationService;

        #endregion
        #region Ctors and Methods

        public NavigableViewModelBase(IFrameNavigationService mainFrameNavigationService,IFrameNavigationService innerFrameNavigationService)
        {
            MainFrameNavigationService = mainFrameNavigationService;
            InnerFrameNavigationService = innerFrameNavigationService;
        }
        #endregion

    }
}
