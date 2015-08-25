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
    public class DoctorsViewModel:NavigableViewModelBase
    {
        #region Fields
        private ObservableCollection<Medecin> _doctorsList;
        private Medecin _seletedDoctor;
        #endregion
        #region Properties              
        public ObservableCollection<Medecin> DoctorsList
        {
            get
            {
                return _doctorsList;
            }

            set
            {
                if (_doctorsList == value)
                {
                    return;
                }

                _doctorsList = value;
                RaisePropertyChanged();
            }
        }
        public Medecin SelectedDoctor
        {
            get
            {
                return _seletedDoctor;
            }

            set
            {
                if (_seletedDoctor == value)
                {
                    return;
                }

                _seletedDoctor = value;
                RaisePropertyChanged();
            }
        }
        
        #endregion
        #region Commands
        
        #endregion
        #region Ctors and Methods
        public DoctorsViewModel(IFrameNavigationService mainFrameNavigationService, IInnerFrameNavigationService innerFrameNavigationService)
            : base(mainFrameNavigationService, innerFrameNavigationService)
        {
        }
        #endregion        
    }
}
