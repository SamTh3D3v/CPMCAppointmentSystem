using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CPMCAppointmentSystem.Helpers;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;

namespace CPMCAppointmentSystem.ViewModel.StatisticsViewModels
{
    public class PatientsPerSexeChartViewModel : NavigableViewModelBase
    {
        #region Fields

        #endregion
        #region Properties

        #endregion
        #region Commands
        private RelayCommand<object> _returnToOriginalTileCommand;    
        public RelayCommand<object> ReturnToOriginalTileCommand
        {
            get
            {
                return _returnToOriginalTileCommand
                    ?? (_returnToOriginalTileCommand = new RelayCommand<object>(
                    (root) => Messenger.Default.Send(new NotificationMessage("RestoreTile"))));
            }
        }

        #endregion
        #region Ctors Methods
        public PatientsPerSexeChartViewModel(IFrameNavigationService mainFrameNavigationService, IInnerFrameNavigationService innerFrameNavigationService)
            : base(mainFrameNavigationService, innerFrameNavigationService)
        {
        }
        #endregion
    }
}
