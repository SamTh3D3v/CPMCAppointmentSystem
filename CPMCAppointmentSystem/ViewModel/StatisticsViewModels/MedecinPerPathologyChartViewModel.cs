using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControlLibrary.ChartModel;
using CPMCAppointmentSystem.Helpers;
using CPMCAppointmentSystem.SubModel;
using DataLayer.Model;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;

namespace CPMCAppointmentSystem.ViewModel.StatisticsViewModels
{   
    public class MedecinPerPathologyChartViewModel : StatisticsChartsViewModelBase
    {
        #region Fields
        private CpmcContext _dbContext;
        private ObservableCollection<EntityPerFieldCountModel> _doctorPerPathologyCollection;
        #endregion
        #region Properties
        public ObservableCollection<EntityPerFieldCountModel> DoctorPerPathologyCollection
        {
            get
            {
                return _doctorPerPathologyCollection;
            }

            set
            {
                if (_doctorPerPathologyCollection == value)
                {
                    return;
                }

                _doctorPerPathologyCollection = value;
                RaisePropertyChanged();
            }
        }
        #endregion
        #region Commands
        private RelayCommand _pathoPerDocLoadedCommand;
        public RelayCommand PathoPerDocLoadedCommand
        {
            get
            {
                return _pathoPerDocLoadedCommand
                    ?? (_pathoPerDocLoadedCommand = new RelayCommand(async () =>
                    {
                        _dbContext = new CpmcContext();
                        DoctorPerPathologyCollection = new ObservableCollection<EntityPerFieldCountModel>(await Task.Run(() => _dbContext.Pathologies.Select(p => new EntityPerFieldCountModel()
                             {
                                 Field = p.NomPathology,
                                 Count = p.Medecins.Count

                             })));
                    }));
            }
        }
        private RelayCommand _pathoPerDocUnLoadedCommand;
        public RelayCommand PathoPerDocUnLoadedCommand
        {
            get
            {
                return _pathoPerDocUnLoadedCommand
                    ?? (_pathoPerDocUnLoadedCommand = new RelayCommand(
                    () =>
                    {
                        _dbContext.Dispose();
                    }));
            }
        }
        #endregion
        #region Ctors Methods
        public MedecinPerPathologyChartViewModel(IFrameNavigationService mainFrameNavigationService, IInnerFrameNavigationService innerFrameNavigationService)
            : base(mainFrameNavigationService, innerFrameNavigationService)
        {
        }
        #endregion
    }
}
