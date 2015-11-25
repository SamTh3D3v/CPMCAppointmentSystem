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
    public class PatientsPerSexeChartViewModel : StatisticsChartsViewModelBase
    {
        #region Fields

        private CpmcContext _dbContext;
        private ObservableCollection<PatientPerSexeModel> _patientCountPerSexeCollection;


        #endregion
        #region Properties
        public ObservableCollection<PatientPerSexeModel> PatientCountPerSexeCollection
        {
            get
            {
                return _patientCountPerSexeCollection;
            }

            set
            {
                if (_patientCountPerSexeCollection == value)
                {
                    return;
                }

                _patientCountPerSexeCollection = value;
                RaisePropertyChanged();
            }
        }
        #endregion
        #region Commands
        private RelayCommand _patientPerSexeLoadedCommand;
        public RelayCommand PatientPerSexeLoadedCommand
        {
            get
            {
                return _patientPerSexeLoadedCommand
                    ?? (_patientPerSexeLoadedCommand = new RelayCommand(async () =>
                    {
                        _dbContext = new CpmcContext();
                        await Task.Run(() =>
                        {
                            PatientCountPerSexeCollection = new ObservableCollection<PatientPerSexeModel>()
                            {
                                new PatientPerSexeModel()
                                {
                                    Sexe = "Masculin",
                                    Count = _dbContext.Patients.Count(p => p.SexeId == 1)
                                },
                                new PatientPerSexeModel()
                                {
                                    Sexe = "Féminin",
                                    Count = _dbContext.Patients.Count(p => p.SexeId == 2)
                                }
                            };
                        });

                    }));
            }
        }
        private RelayCommand _patientPerSexeUnLoadedCommand;
        public RelayCommand PatientPerSexeUnLoadedCommand
        {
            get
            {
                return _patientPerSexeUnLoadedCommand
                    ?? (_patientPerSexeUnLoadedCommand = new RelayCommand(
                    () =>
                    {
                        _dbContext.Dispose();
                    }));
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
