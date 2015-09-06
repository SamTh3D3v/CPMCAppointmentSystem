using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CPMCAppointmentSystem.Helpers;
using DataLayer.Model;
using GalaSoft.MvvmLight.Command;
using Syncfusion.Windows.Forms.Tools.Navigation;

namespace CPMCAppointmentSystem.ViewModel
{
    public class PathologiesViewModel:NavigableViewModelBase
    {
        #region Fields
        private ObservableCollection<Pathology> _pathologiesList;
        private Pathology _selectedPathology;
        private Medecin _selectedDoctorWithinPathology;
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
        public Medecin SelectedDoctorWithinPathology
        {
            get
            {
                return _selectedDoctorWithinPathology;
            }

            set
            {
                if (_selectedDoctorWithinPathology == value)
                {
                    return;
                }

                _selectedDoctorWithinPathology = value;
                RaisePropertyChanged();
            }
        }
        #endregion
        #region Commands
        private RelayCommand _pathologyViewLoadedCommand;
        public RelayCommand PathologyViewLoadedCommand
        {
            get
            {
                return _pathologyViewLoadedCommand
                    ?? (_pathologyViewLoadedCommand = new RelayCommand(
                    () =>
                    {
                        
                    }));
            }
        }
        private RelayCommand _addPathologyCommand;
        public RelayCommand AddPathologyCommand
        {
            get
            {
                return _addPathologyCommand
                    ?? (_addPathologyCommand = new RelayCommand(
                    () =>
                    {
                        
                    }));
            }
        }
        private RelayCommand _addDoctorToPathologyCommand;
        public RelayCommand AddDoctorToPathologyCommand
        {
            get
            {
                return _addDoctorToPathologyCommand
                    ?? (_addDoctorToPathologyCommand = new RelayCommand(
                    () =>
                    {
                        
                    }));
            }
        }
        private RelayCommand _savePathologyCommand;
        public RelayCommand SavePathologyCommand
        {
            get
            {
                return _savePathologyCommand
                    ?? (_savePathologyCommand = new RelayCommand(
                    () =>
                    {
                        
                    }));
            }
        }
        private RelayCommand _deletePathologyCommand;
        public RelayCommand DeletePathologyCommand
        {
            get
            {
                return _deletePathologyCommand
                    ?? (_deletePathologyCommand = new RelayCommand(
                    () =>
                    {
                        
                    }));
            }
        }
        private RelayCommand _cancelChangesToPathologyCommand;
        public RelayCommand CancelChangesToPathologyCommand
        {
            get
            {
                return _cancelChangesToPathologyCommand
                    ?? (_cancelChangesToPathologyCommand = new RelayCommand(
                    () =>
                    {
                        
                    }));
            }
        }
        
        #endregion
        #region Ctors and Methods
        public PathologiesViewModel(IFrameNavigationService mainFrameNavigationService, IInnerFrameNavigationService innerFrameNavigationService)
            : base(mainFrameNavigationService, innerFrameNavigationService)
        {
        }
        #endregion        
    }
}
