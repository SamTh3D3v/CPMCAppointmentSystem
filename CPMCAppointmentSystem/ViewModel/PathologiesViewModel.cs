using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CPMCAppointmentSystem.Helpers;
using DataLayer.Model;
using Syncfusion.Windows.Forms.Tools.Navigation;

namespace CPMCAppointmentSystem.ViewModel
{
    public class PathologiesViewModel:NavigableViewModelBase
    {
        #region Fields
        private ObservableCollection<Pathology> _pathologiesList;
        private Pathology _selectedPathology;
        #endregion
        #region Properties
        public ObservableCollection<Pathology> PathologiesList
        {
            get
            {
                return _pathologiesList;
            }

            set
            {
                if (_pathologiesList == value)
                {
                    return;
                }

                _pathologiesList = value;
                RaisePropertyChanged();
            }
        }       
        public Pathology SelectedPathology
        {
            get
            {
                return _selectedPathology;
            }

            set
            {
                if (_selectedPathology == value)
                {
                    return;
                }

                _selectedPathology = value;
                RaisePropertyChanged();
            }
        }
        #endregion
        #region Commands
        
        #endregion
        #region Ctors and Methods
        public PathologiesViewModel(IFrameNavigationService mainFrameNavigationService, IInnerFrameNavigationService innerFrameNavigationService)
            : base(mainFrameNavigationService, innerFrameNavigationService)
        {
        }
        #endregion        
    }
}
