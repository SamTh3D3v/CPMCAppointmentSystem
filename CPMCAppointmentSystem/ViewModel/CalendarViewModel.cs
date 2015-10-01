using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CPMCAppointmentSystem.Helpers;
using DataLayer.Model;
using GalaSoft.MvvmLight.Command;
using Syncfusion.UI.Xaml.Schedule;

namespace CPMCAppointmentSystem.ViewModel
{
    public class CalendarViewModel:NavigableViewModelBase
    {
        #region Fields
        private CpmcContext _dbContext=new CpmcContext();
        private ScheduleAppointmentCollection _patientsScheduleAppointmentCollection; 
        #endregion
        #region Properties                    
        public ScheduleAppointmentCollection   PatientsScheduleAppointmentCollection
        {
            get
            {
                return _patientsScheduleAppointmentCollection;
            }

            set
            {
                if (_patientsScheduleAppointmentCollection == value)
                {
                    return;
                }

                _patientsScheduleAppointmentCollection = value;
                RaisePropertyChanged();
            }
        }
        #endregion
        #region Commands
        private RelayCommand _calendarViewLoadedCommand;
        public RelayCommand CalendarViewLoadedCommand
        {
            get
            {
                return _calendarViewLoadedCommand
                    ?? (_calendarViewLoadedCommand = new RelayCommand(
                    () =>
                    {
                        LoadAppointementsForCurrentRange();

                    }));
            }
        }

        private void LoadAppointementsForCurrentRange()
        {
            
        }

        #endregion
        #region Ctors and Methods
        public CalendarViewModel(IFrameNavigationService mainFrameNavigationService, IInnerFrameNavigationService innerFrameNavigationService)
            : base(mainFrameNavigationService, innerFrameNavigationService)
        {
        }
        #endregion        
    }
}
