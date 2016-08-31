using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CPMCAppointmentSystem.Helpers;
using DataLayer;
using DataLayer.Model;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GsmManager;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using GsmHelper = CPMCAppointmentSystem.Helpers.GsmHelper;

namespace CPMCAppointmentSystem.ViewModel
{
    public class CentrageViewModel: NavigableViewModelBase
    {
        #region Fields        
        private bool _allDataLoaded = false;
        private bool _stillInView;
        private DateTime _selectedDateRdvAvecMedecin = DateTime.Now;                
        private CpmcContext _dbContext = new CpmcContext();
        private ObservableCollection<RendezVous> _rdvCollectionList;
        private RendezVous _selectedRdv;
        private bool _isFilterCheckActivated;
        private bool _isProgressRingActive;
        #endregion
        #region Properties                
        public bool IsProgressRingActive
        {
            get
            {
                return _isProgressRingActive;
            }

            set
            {
                if (_isProgressRingActive == value)
                {
                    return;
                }

                _isProgressRingActive = value;
                RaisePropertyChanged();
            }
        }
        public DateTime SelectedDateRdvAvecMedecin
        {
            get
            {
                return _selectedDateRdvAvecMedecin; ;
            }

            set
            {
                if (_selectedDateRdvAvecMedecin == value)
                {
                    return;
                }

                _selectedDateRdvAvecMedecin = value;
                RaisePropertyChanged();
            }
        }

        public bool IsFilterCheckActivated
        {
            get
            {
                return _isFilterCheckActivated;
            }

            set
            {
                if (_isFilterCheckActivated == value)
                {
                    return;
                }

                _isFilterCheckActivated = value;
                RaisePropertyChanged();
            }
        }                
        public RendezVous SelectedRdv
        {
            get
            {
                return _selectedRdv;
            }

            set
            {
                if (_selectedRdv == value)
                {
                    return;
                }

                _selectedRdv = value;
                RaisePropertyChanged();
            }
        }
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
        public String SmsMessageTemplate { get; set; }
        #endregion
        #region Commands        
   
        private RelayCommand _centrageViewLoadedCommand;
        public RelayCommand CentrageViewLoadedCommand
        {
            get
            {
                return _centrageViewLoadedCommand
                    ?? (_centrageViewLoadedCommand = new RelayCommand(async () =>
                    {
                        _allDataLoaded = false;
                        _stillInView = true;
                        _dbContext = new CpmcContext();
                        await LoadRdvs();                       
                        _allDataLoaded = true;
                    }));
            }
        }        

        private RelayCommand _centrageViewUnLoadedCommand ;
        public RelayCommand CentrageViewUnLoadedCommand         
        {
            get
            {
                return _centrageViewUnLoadedCommand
                    ?? (_centrageViewUnLoadedCommand = new RelayCommand(
                    () =>
                    {
                        _dbContext.SaveChanges();
                        Task.Run(() =>
                        {
                            while (!_allDataLoaded) { }
                            _dbContext.Dispose();

                        });
                        _stillInView = false;
                        IsProgressRingActive = false;

                    }));
            }
        }
             

        private async Task LoadRdvs()
        {            
            var date = SelectedDateRdvAvecMedecin.Date;
            RdvCollectionList = IsFilterCheckActivated ? new ObservableCollection<RendezVous>(await Task.Run(() => _dbContext.RendezVouses.Where(rdv => DbFunctions.TruncateTime(rdv.DateTimeRdv) == date))) : new ObservableCollection<RendezVous>(await Task.Run(() => _dbContext.RendezVouses));
        }
        private RelayCommand _reloadRdvsCommand;
        public RelayCommand ReloadRdvsCommand
        {
            get
            {
                return _reloadRdvsCommand
                    ?? (_reloadRdvsCommand = new RelayCommand(async () =>
                    {
                        await LoadRdvs();
                    }));
            }
        }

        #endregion
        #region Ctors and methods
        public CentrageViewModel(IFrameNavigationService mainFrameNavigationService, IInnerFrameNavigationService innerFrameNavigationService)
            : base(mainFrameNavigationService, innerFrameNavigationService)
        {            
        }
        #endregion
    }
}
