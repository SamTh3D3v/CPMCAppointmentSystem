using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControlLibrary.ChartModel;
using CPMCAppointmentSystem.Helpers;
using DataLayer.Model;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;

namespace CPMCAppointmentSystem.ViewModel.StatisticsViewModels
{
    public class PatientPerDateChartViewModel : StatisticsChartsViewModelBase
    {
        #region Fields
        private CpmcContext _dbContext;
        private ObservableCollection<EntityPerFieldCountModel> _patientPerDateCollection;
        private DateTime _dateFinDateTime;
        private DateTime _dateDebutDateTime;
        #endregion
        #region Properties         
        public DateTime DateDebut
        {
            get
            {
                return _dateDebutDateTime;
            }

            set
            {
                if (_dateDebutDateTime == value)
                {
                    return;
                }

                _dateDebutDateTime = value;
                RaisePropertyChanged();
            }
        }             
        public DateTime DateFin
        {
            get
            {
                return _dateFinDateTime;
            }

            set
            {
                if (_dateFinDateTime == value)
                {
                    return;
                }

                _dateFinDateTime = value;
                RaisePropertyChanged();
            }
        }
        public ObservableCollection<EntityPerFieldCountModel> PatientPerDateCollection
        {
            get
            {
                return _patientPerDateCollection;
            }

            set
            {
                if (_patientPerDateCollection == value)
                {
                    return;
                }

                _patientPerDateCollection = value;
                RaisePropertyChanged();
            }
        }
        #endregion
        #region Commands   
        private RelayCommand _patientPerDateLoadedCommand;
        public RelayCommand PatientPerDateLoadedCommand
        {
            get
            {
                return _patientPerDateLoadedCommand
                    ?? (_patientPerDateLoadedCommand = new RelayCommand(async () =>
                    {
                        _dbContext=new CpmcContext();
                        //PatientPerDateCollection = new ObservableCollection<EntityPerFieldCountModel>(await Task.Run(() => _dbContext.Patients.AsEnumerable().GroupBy(p=>p.DateDeDepot).Select(p => new EntityPerFieldCountModel()
                        //{
                        //    Field = p.NomPathology,
                        //    Count = p.Medecins.Count

                        //})));
                        
                    }));
            }
        }
        private RelayCommand _patientPerDateUnLoadedCommand;
        public RelayCommand PatientPerDateUnLoadedCommand
        {
            get
            {
                return _patientPerDateUnLoadedCommand
                    ?? (_patientPerDateUnLoadedCommand = new RelayCommand(
                    () =>
                    {
                        _dbContext.Dispose();
                        
                    }));
            }
        }

        #endregion
        #region Ctors Methods
        public PatientPerDateChartViewModel(IFrameNavigationService mainFrameNavigationService, IInnerFrameNavigationService innerFrameNavigationService)
            : base(mainFrameNavigationService, innerFrameNavigationService)
        {
        }
        #endregion
    }
}
