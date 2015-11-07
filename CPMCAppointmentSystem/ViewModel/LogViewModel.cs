using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CPMCAppointmentSystem.Helpers;
using DataLayer.Model;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;

namespace CPMCAppointmentSystem.ViewModel
{
    public class LogViewModel : NavigableViewModelBase
    {
        #region Fileds

        private CpmcContext _dbContext=new CpmcContext();
        private string _selectedLog;
        private ObservableCollection<Trace> _logCollectionList; 
        #endregion
        #region Properties
        public string SelectedLog
        {
            get
            {
                return _selectedLog;
            }

            set
            {
                if (_selectedLog == value)
                {
                    return;
                }

                _selectedLog = value;
                RaisePropertyChanged();
            }
        }               
        public ObservableCollection<Trace> LogCollectionList
        {
            get
            {
                return _logCollectionList;
            }

            set
            {
                if (_logCollectionList == value)
                {
                    return;
                }

                _logCollectionList = value;
                RaisePropertyChanged();
            }
        }
        
        #endregion 
        #region Commands
        private RelayCommand _logViewLoadedCommand;
        public RelayCommand LogViewLoadedCommand
        {
            get
            {
                return _logViewLoadedCommand
                    ?? (_logViewLoadedCommand = new RelayCommand(async () =>
                    {                       
                        _dbContext = new CpmcContext();
                        LogCollectionList=new ObservableCollection<Trace>(await Task.Run(()=>_dbContext.Traces));
                    }));
            }
        }
        private RelayCommand _logViewUnloadedCommand;
        public RelayCommand LogViewUnloadedCommand
        {
            get
            {
                return _logViewUnloadedCommand
                    ?? (_logViewUnloadedCommand = new RelayCommand(
                    () =>
                    {
                        
                    }));
            }
        }
        #endregion
        #region Ctors and Methods
        public LogViewModel(IFrameNavigationService mainFrameNavigationService, IInnerFrameNavigationService innerFrameNavigationService)
            : base(mainFrameNavigationService, innerFrameNavigationService)
        {
        }
        #endregion       
    }
}
