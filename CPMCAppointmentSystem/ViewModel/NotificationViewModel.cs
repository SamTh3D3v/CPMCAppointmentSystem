using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CPMCAppointmentSystem.Helpers;
using DataLayer.Model;
using GalaSoft.MvvmLight.CommandWpf;

namespace CPMCAppointmentSystem.ViewModel
{
    public class NotificationViewModel:NavigableViewModelBase
    {
        #region Fields
        private readonly CpmcContext _dbContext=new CpmcContext();
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
        private RelayCommand _notificationViewLoadedCommand;
        public RelayCommand NotificationViewLoadedCommand
        {
            get
            {
                return _notificationViewLoadedCommand
                    ?? (_notificationViewLoadedCommand = new RelayCommand(async () =>
                    {
                        await LoadRdvs();
                    }));
            }
        }

        private async Task LoadRdvs()
        {
            RdvCollectionList=new ObservableCollection<RendezVous>(await Task.Run(()=>_dbContext.RendezVouses));
        }

        #endregion 
        #region Ctors and methods
        public NotificationViewModel(IFrameNavigationService mainFrameNavigationService, IInnerFrameNavigationService innerFrameNavigationService)
            : base(mainFrameNavigationService, innerFrameNavigationService)
        {
        }
        #endregion       
    }
}
