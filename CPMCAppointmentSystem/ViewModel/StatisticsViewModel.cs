using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
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
                new ChartTile() { Name = "Medecin par pathology", Color = new SolidColorBrush(Colors.LightSeaGreen), View = new MedecinPerPathologyChartView(), Header = "Medecin par pathology", Icon = "../../Tiles/Chart.png" },
                new ChartTile() { Name = "Medecin par specialité", Color = new SolidColorBrush(Colors.LightSteelBlue), View = new MedecinPerSpecialityChartView(), Header = "Medecin par specialité", Icon = "../../Tiles/Chart.png" },
                new ChartTile() { Name = "Moyenne des rdvs", Color = new SolidColorBrush(Colors.LightSlateGray), View = new MoyenneDateDepotDateRdvChartView(), Header = "Moyenne des rdvs", Icon = "../../Tiles/Chart.png" },
                new ChartTile() { Name = "Patient par date", Color = new SolidColorBrush(Colors.LightGray), View = new NombreDesPatientPerDateChartView(), Header = "Patient par date", Icon = "../../Tiles/Chart.png" },
                new ChartTile() { Name = "Pathology par sexe", Color = new SolidColorBrush(Colors.LightBlue), View = new PathologyPerPatientSexeChartView(), Header = "Pathology par sexe", Icon = "../../Tiles/Chart.png" },
                new ChartTile() { Name = "Patient par age", Color = new SolidColorBrush(Colors.LightPink), View = new PatientPerAgeChartView(), Header = "Patient par age", Icon = "../../Tiles/Chart.png" },
                new ChartTile() { Name = "Patient par date depot", Color = new SolidColorBrush(Colors.LightSeaGreen), View = new PatientPerDateChartView(), Header = "Patient par date depot", Icon = "../../Tiles/Chart.png" },
                new ChartTile() { Name = "Patient par medecin", Color =new SolidColorBrush(Colors.LightSalmon), View = new PatientPerMedecinChartView(), Header = "Patient par medecin", Icon = "../../Tiles/Chart.png" },
                new ChartTile() { Name = "Patient par pathology", Color = new SolidColorBrush(Colors.LightGreen), View = new PatientPerPathologyChartView(), Header = "Patient par pathology", Icon = "../../Tiles/Chart.png" },
                new ChartTile() { Name = "Patient par willaya", Color = new SolidColorBrush(Colors.LightCoral), View = new PatientPerWillayaDeResidanceChartView() , Header = "Patient par willaya", Icon = "../../Tiles/Chart.png" },
                new ChartTile() { Name = "Patient per sexe", Color = new SolidColorBrush(Colors.LightBlue), View = new PatientsPerSexeChartView(), Header = "Patient per sexe", Icon = "../../Tiles/Chart.png" }
            };
            
        }
        #endregion       
    }
}
