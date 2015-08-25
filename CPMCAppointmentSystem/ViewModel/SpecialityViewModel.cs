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
    public class SpecialityViewModel:NavigableViewModelBase
    {
        #region Fields
        private ObservableCollection<Specialite> _specialitiesList;
        private Specialite _selectedSpeciality;
        #endregion
        #region Properties              
        public ObservableCollection<Specialite> SpecialityList
        {
            get
            {
                return _specialitiesList;
            }

            set
            {
                if (_specialitiesList == value)
                {
                    return;
                }

                _specialitiesList = value;
                RaisePropertyChanged();
            }
        }               
        public Specialite SelectedSpeciality
        {
            get
            {
                return _selectedSpeciality;
            }

            set
            {
                if (_selectedSpeciality == value)
                {
                    return;
                }

                _selectedSpeciality = value;
                RaisePropertyChanged();
            }
        }

        #endregion
        #region Commands

        #endregion
        #region Ctors and Methods
        public SpecialityViewModel(IFrameNavigationService mainFrameNavigationService, IInnerFrameNavigationService innerFrameNavigationService)
            : base(mainFrameNavigationService, innerFrameNavigationService)
        {
        }
        #endregion

        
    }
}
