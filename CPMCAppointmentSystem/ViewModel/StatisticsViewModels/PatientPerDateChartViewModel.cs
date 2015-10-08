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
    public class PatientPerDateChartViewModel : NavigableViewModelBase
    {
         #region Fields

        #endregion
        #region Properties

        #endregion
        #region Commands
        private RelayCommand _returnToOriginalTileCommand;
        public RelayCommand ReturnToOriginalTileCommand
        {
            get
            {
                return _returnToOriginalTileCommand
                    ?? (_returnToOriginalTileCommand = new RelayCommand(
                    () => Messenger.Default.Send(new NotificationMessage("RestoreTile"))));
            }
        }

        #endregion
        #region Ctors Methods
        public PatientPerDateChartViewModel(IFrameNavigationService mainFrameNavigationService, IInnerFrameNavigationService innerFrameNavigationService)
            : base(mainFrameNavigationService, innerFrameNavigationService)
        {
        }
        #endregion
    }
}
