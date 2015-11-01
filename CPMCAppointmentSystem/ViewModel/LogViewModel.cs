using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CPMCAppointmentSystem.Helpers;
using GalaSoft.MvvmLight.Command;

namespace CPMCAppointmentSystem.ViewModel
{
    public class LogViewModel : NavigableViewModelBase
    {
        #region Fileds
        private string _selectedLog;
        private ObservableCollection<string> _logCollectionList; 
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
        public ObservableCollection<string> LogCollectionList
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
                    ?? (_logViewLoadedCommand = new RelayCommand(
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
