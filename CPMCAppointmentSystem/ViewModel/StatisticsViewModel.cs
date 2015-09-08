using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CPMCAppointmentSystem.Helpers;
using CPMCAppointmentSystem.View.StatisticsViews;
using GalaSoft.MvvmLight.Command;
using Syncfusion.Windows.Forms.Tools.Navigation;

namespace CPMCAppointmentSystem.ViewModel
{
    public class StatisticsViewModel : NavigableViewModelBase
    {
        #region Fields
        
        #endregion
        #region Properties
       

        private ObservableCollection<ChartTile> _chartTileCollection ;   
        public ObservableCollection<ChartTile> ChartTileCollection
        {
            get
            {
                return _chartTileCollection;
            }

            set
            {
                if (_chartTileCollection == value)
                {
                    return;
                }

                _chartTileCollection = value;
                RaisePropertyChanged();
            }
        }
        #endregion
        #region Commands
        private RelayCommand _statisticsViewLoadedCommand;
        public RelayCommand StatisticsViewLoadedCommand
        {
            get
            {
                return _statisticsViewLoadedCommand
                    ?? (_statisticsViewLoadedCommand = new RelayCommand(
                    () =>
                    {
                        //Create the Default Charts Collection 
                        CreateDeFaultTileChartsCollection();

                    }));
            }
        }
        
        #endregion
        #region Ctors and Methods
        public StatisticsViewModel(IFrameNavigationService mainFrameNavigationService, IInnerFrameNavigationService innerFrameNavigationService)
            : base(mainFrameNavigationService, innerFrameNavigationService)
        {
        }

        public void CreateDeFaultTileChartsCollection()
        {
            ChartTileCollection=new ObservableCollection<ChartTile>()
            {
                new ChartTile() { Name = "patient/sexe", Color = "#FF4DAEB5", View = new PatientsPerSexeChartView(), Header = "patient homme femme", Icon = "../../Tiles/save.png" }
            };
            
        }
        #endregion       
    }
}
