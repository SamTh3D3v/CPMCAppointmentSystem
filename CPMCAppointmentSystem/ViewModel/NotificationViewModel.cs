using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CPMCAppointmentSystem.Helpers;
using DataLayer.Model;
using Syncfusion.Windows.Forms.Tools.Navigation;

namespace CPMCAppointmentSystem.ViewModel
{
    public class NotificationViewModel:NavigableViewModelBase
    {
        #region Fields
        private ObservableCollection<RendezVous> _rdvCollectionList;
        #endregion 
        #region Properties
        public ObservableCollection<RendezVous> RdvCollectionList
        {
            get
            {
                return _rdvCollectionList;
            }

            set
            {
                if (_rdvCollectionList == value)
                {
                    return;
                }

                _rdvCollectionList = value;
                RaisePropertyChanged();
            }
        }
        
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
