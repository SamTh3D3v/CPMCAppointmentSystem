using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControlLibrary.ChartModel;
using CPMCAppointmentSystem.Helpers;
using DataLayer.Model;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;

namespace CPMCAppointmentSystem.ViewModel.StatisticsViewModels
{
    public class PatientPerWillayaDeResidanceChartViewModel : StatisticsChartsViewModelBase
    {
        #region Fields
        private ObservableCollection<EntityPerFieldCountModel> _patientCountPerWillayaCollection;
        private CpmcContext _dbContext;
        #endregion
        #region Properties
        public ObservableCollection<EntityPerFieldCountModel> PatientCountPerWillayaCollection
        {
            get
            {
                return _patientCountPerWillayaCollection;
            }

            set
            {
                if (_patientCountPerWillayaCollection == value)
                {
                    return;
                }

                _patientCountPerWillayaCollection = value;
                RaisePropertyChanged();
            }
        }
        #endregion
        #region Commands
        private RelayCommand _patientPerWillayaLoadedCommand;
        public RelayCommand PatientPerWillayaLoadedCommand
        {
            get
            {
                return _patientPerWillayaLoadedCommand
                    ?? (_patientPerWillayaLoadedCommand = new RelayCommand(async () =>
                    {
                        _dbContext = new CpmcContext();
                        PatientCountPerWillayaCollection = new ObservableCollection<EntityPerFieldCountModel>(await Task.Run(()=>_dbContext.Patients.GroupBy(p=>p.Adresse.Willaya.Designation).Select(p=>new EntityPerFieldCountModel()
                        {
                            Count = p.Count(),
                            Field = p.Key
                        })));                            
                    }));
            }
        }
        private RelayCommand _patientPerWillayaUnLoadedCommand;
        public RelayCommand PatientPerWillayaUnLoadedCommand
        {
            get
            {
                return _patientPerWillayaUnLoadedCommand
                    ?? (_patientPerWillayaUnLoadedCommand = new RelayCommand(
                    () =>
                    {
                        _dbContext.Dispose();
                    }));
            }
        }
        #endregion
        #region Ctors Methods
        public PatientPerWillayaDeResidanceChartViewModel(IFrameNavigationService mainFrameNavigationService, IInnerFrameNavigationService innerFrameNavigationService)
            : base(mainFrameNavigationService, innerFrameNavigationService)
        {
        }
        #endregion
    }
}
