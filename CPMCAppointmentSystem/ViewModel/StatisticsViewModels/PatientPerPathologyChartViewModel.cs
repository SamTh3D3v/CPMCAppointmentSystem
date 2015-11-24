using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CPMCAppointmentSystem.Helpers;
using CPMCAppointmentSystem.SubModel;
using DataLayer.Model;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;

namespace CPMCAppointmentSystem.ViewModel.StatisticsViewModels
{
    public class PatientPerPathologyChartViewModel : StatisticsChartsViewModelBase
    {
        #region Fields
        private CpmcContext _dbContext;
        private ObservableCollection<BarModel> _patientPerPathologyCollection;
        #endregion
        #region Properties
        public ObservableCollection<BarModel> PatientPerPathologyCollection
        {
            get
            {
                return _patientPerPathologyCollection;
            }

            set
            {
                if (_patientPerPathologyCollection == value)
                {
                    return;
                }

                _patientPerPathologyCollection = value;
                RaisePropertyChanged();
            }
        }
        #endregion
        #region Commands      
        private RelayCommand _pathoPerPatLoadedCommand;
        public RelayCommand PathoPerPatLoadedCommand
        {
            get
            {
                return _pathoPerPatLoadedCommand
                    ?? (_pathoPerPatLoadedCommand = new RelayCommand(async () =>
                    {
                        _dbContext = new CpmcContext();
                        PatientPerPathologyCollection = new ObservableCollection<BarModel>(await Task.Run(() => _dbContext.Pathologies.Select(p => new BarModel()
                        {
                            Item = p.NomPathology,
                            ItemsCount = p.Patients.Count

                        })));
                    }));
            }
        }
        private RelayCommand _pathoPerPatUnLoadedCommand;
        public RelayCommand PathoPerPatUnLoadedCommand
        {
            get
            {
                return _pathoPerPatUnLoadedCommand
                    ?? (_pathoPerPatUnLoadedCommand = new RelayCommand(
                    () =>
                    {
                        _dbContext.Dispose();
                    }));
            }
        }
        #endregion
        #region Ctors Methods
        public PatientPerPathologyChartViewModel(IFrameNavigationService mainFrameNavigationService, IInnerFrameNavigationService innerFrameNavigationService)
            : base(mainFrameNavigationService, innerFrameNavigationService)
        {
        }
        #endregion
    }
}
