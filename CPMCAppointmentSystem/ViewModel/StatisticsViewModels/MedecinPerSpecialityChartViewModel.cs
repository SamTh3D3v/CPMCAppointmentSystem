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
    public class MedecinPerSpecialityChartViewModel : StatisticsChartsViewModelBase
    {
        #region Fields
        private CpmcContext _dbContext;
        private ObservableCollection<EntityPerFieldCountModel> _medecinperSpecialityCollection;
        #endregion
        #region Properties
        public ObservableCollection<EntityPerFieldCountModel> DoctorsPerSpecialityCollection
        {
            get
            {
                return _medecinperSpecialityCollection;
            }

            set
            {
                if (_medecinperSpecialityCollection == value)
                {
                    return;
                }

                _medecinperSpecialityCollection = value;
                RaisePropertyChanged();
            }
        }

        #endregion
        #region Commands      
        private RelayCommand _spePerDocLoadedCommand;
        public RelayCommand SpePerDocLoadedCommand
        {
            get
            {
                return _spePerDocLoadedCommand
                    ?? (_spePerDocLoadedCommand = new RelayCommand(async () =>
                    {
                        _dbContext = new CpmcContext();
                        DoctorsPerSpecialityCollection = new ObservableCollection<EntityPerFieldCountModel>(await Task.Run(() => _dbContext.Specialites.Select(s => new EntityPerFieldCountModel()
                        {
                            Field = s.Name,
                            Count = s.Medecins.Count

                        })));
                    }));
            }   
        }
        private RelayCommand _spePerDoctorsUnloadedCommand;
        public RelayCommand SpePerDoctorsUnlodedCommand
        {
            get
            {
                return _spePerDoctorsUnloadedCommand
                    ?? (_spePerDoctorsUnloadedCommand = new RelayCommand(
                    () =>
                    {
                        _dbContext.Dispose();
                    }));
            }
        }
        #endregion
        #region Ctors Methods
        public MedecinPerSpecialityChartViewModel(IFrameNavigationService mainFrameNavigationService, IInnerFrameNavigationService innerFrameNavigationService)
            : base(mainFrameNavigationService, innerFrameNavigationService)
        {
        }
        #endregion
    }
}
