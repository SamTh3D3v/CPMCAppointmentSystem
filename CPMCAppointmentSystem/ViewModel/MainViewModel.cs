using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CPMCAppointmentSystem.Helpers;
using GalaSoft.MvvmLight.Command;

namespace CPMCAppointmentSystem.ViewModel
{
    public class MainViewModel : NavigableViewModelBase
    {
        #region Fields
       
        #endregion
        #region Properties      

        #endregion
        #region Commands
        private RelayCommand _mainViewLoadedCommand;
        public RelayCommand MainViewLoadedCommand
        {
            get
            {
                return _mainViewLoadedCommand
                    ?? (_mainViewLoadedCommand = new RelayCommand(
                    () => InnerFrameNavigationService.NavigateTo(App.PatientsViewKey)));
            }
        }
        private RelayCommand _calendarCommand;
        public RelayCommand CalendarCommand
        {
            get
            {
                return _calendarCommand
                    ?? (_calendarCommand = new RelayCommand(
                    () => InnerFrameNavigationService.NavigateTo(App.CalendarViewKey)));
            }
        }
        private RelayCommand _patientsCommand;  
        public RelayCommand PatientsCommand
        {
            get
            {
                return _patientsCommand
                    ?? (_patientsCommand = new RelayCommand(
                    () => InnerFrameNavigationService.NavigateTo(App.PatientsViewKey)));
            }
        }
        private RelayCommand _myPatientsCommand;
        public RelayCommand MyPatientsCommand
        {
            get
            {
                return _myPatientsCommand
                    ?? (_myPatientsCommand = new RelayCommand(
                    () => InnerFrameNavigationService.NavigateTo(App.MyPatientsViewKey)));
            }
        }
        private RelayCommand _doctorsCommand;
        public RelayCommand DoctorsCommand
        {
            get
            {
                return _doctorsCommand
                    ?? (_doctorsCommand = new RelayCommand(
                    () => InnerFrameNavigationService.NavigateTo(App.DoctorsViewKey)));
            }
        }        
        private RelayCommand _specialitiesCommand;
        public RelayCommand SpecialitiesCommand
        {
            get
            {
                return _specialitiesCommand
                    ?? (_specialitiesCommand = new RelayCommand(
                    () => InnerFrameNavigationService.NavigateTo(App.SpecialityViewKey)));
            }
        }
        private RelayCommand _settingsCommand;
        public RelayCommand SettingsCommand
        {
            get
            {
                return _settingsCommand
                    ?? (_settingsCommand = new RelayCommand(
                    () => InnerFrameNavigationService.NavigateTo(App.SettingsViewKey)));
            }
        }
        private RelayCommand _pathologiesCommand;
        public RelayCommand PathologiesCommand
        {
            get
            {
                return _pathologiesCommand
                    ?? (_pathologiesCommand = new RelayCommand(
                    () => InnerFrameNavigationService.NavigateTo(App.PathologiesViewKey)));
            }
        }
        private RelayCommand _statisticsCommand;            
        public RelayCommand StatisticsCommand
        {
            get
            {
                return _statisticsCommand
                    ?? (_statisticsCommand = new RelayCommand(
                    () =>
                    {
                        
                    }));
            }
        }

        #endregion
        #region Ctors and Methods
        public MainViewModel(IFrameNavigationService mainFrameNavigationService, IInnerFrameNavigationService innerFrameNavigationService)
            : base(mainFrameNavigationService, innerFrameNavigationService)
        {
        }
        #endregion
    }
}
