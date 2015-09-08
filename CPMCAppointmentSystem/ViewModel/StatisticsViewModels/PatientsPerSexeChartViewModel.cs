using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CPMCAppointmentSystem.Helpers;

namespace CPMCAppointmentSystem.ViewModel.StatisticsViewModels
{
    public class PatientsPerSexeChartViewModel : NavigableViewModelBase
    {
        #region Fields

        #endregion
        #region Properties

        #endregion
        #region Commands

        #endregion
        #region Ctors Methods
        public PatientsPerSexeChartViewModel(IFrameNavigationService mainFrameNavigationService, IInnerFrameNavigationService innerFrameNavigationService)
            : base(mainFrameNavigationService, innerFrameNavigationService)
        {
        }
        #endregion
    }
}
