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
                new ChartTile() { Name = "Medecin par pathology",Description = "Medecin par pathology", Color = new SolidColorBrush(Colors.LightSeaGreen), View = new MedecinPerPathologyChartView(), Header = "Pathology", Icon = "../../Tiles/Chart.png" },
                new ChartTile() { Name = "Medecin par specialité",Description = "Medecin par specialité", Color = new SolidColorBrush(Colors.LightSteelBlue), View = new MedecinPerSpecialityChartView(), Header = "Specialité", Icon = "../../Tiles/Chart.png" },
                new ChartTile() { Name = "Moyenne des rdvs",Description = "Moyenne des rdvs", Color = new SolidColorBrush(Colors.LightSlateGray), View = new MoyenneDateDepotDateRdvChartView(), Header = "Rdvs", Icon = "../../Tiles/Chart.png" },
                new ChartTile() { Name = "Patient par date",Description = "Patient par date", Color = new SolidColorBrush(Colors.LightGray), View = new NombreDesPatientPerDateChartView(), Header = "Date", Icon = "../../Tiles/Chart.png" },
                new ChartTile() { Name = "Pathology par sexe",Description = "Pathology par sexe", Color = new SolidColorBrush(Colors.LightBlue), View = new PathologyPerPatientSexeChartView(), Header = "Sexe", Icon = "../../Tiles/Chart.png" },
                new ChartTile() { Name = "Patient par age",Description = "Patient par age", Color = new SolidColorBrush(Colors.LightPink), View = new PatientPerAgeChartView(), Header = "Age", Icon = "../../Tiles/Chart.png" },
                new ChartTile() { Name = "Patient par date depot",Description = "Patient par date depot", Color = new SolidColorBrush(Colors.LightSeaGreen), View = new PatientPerDateChartView(), Header = "Date depot", Icon = "../../Tiles/Chart.png" },
                new ChartTile() { Name = "Patient par medecin",Description = "Patient par medecin", Color =new SolidColorBrush(Colors.LightSalmon), View = new PatientPerMedecinChartView(), Header = "Medecin", Icon = "../../Tiles/Chart.png" },
                new ChartTile() { Name = "Patient par pathology",Description = "Patient par pathology", Color = new SolidColorBrush(Colors.LightGreen), View = new PatientPerPathologyChartView(), Header = "Pathology", Icon = "../../Tiles/Chart.png" },
                new ChartTile() { Name = "Patient par willaya",Description = "Patient par willaya", Color = new SolidColorBrush(Colors.LightCoral), View = new PatientPerWillayaDeResidanceChartView() , Header = "Willaya", Icon = "../../Tiles/Chart.png" },
                new ChartTile() { Name = "Patient per sexe", Description = "Patient per sexe", Color = new SolidColorBrush(Colors.LightBlue), View = new PatientsPerSexeChartView(), Header = "Sexe", Icon = "../../Tiles/Chart.png" }
            };
            
        }
        #endregion       
    }
}
