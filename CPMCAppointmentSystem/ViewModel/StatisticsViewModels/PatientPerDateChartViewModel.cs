using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Diagnostics;
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
                        _dbContext = new CpmcContext();
                        await LoadPatientPerDate("Day");
                    }));
            }
        }

        private async Task LoadPatientPerDate(string dateField)
        {
            await Task.Run(() =>
            {
                switch (dateField)
                {
                    case "Day":
                        PatientPerDateCollection = new ObservableCollection<EntityPerFieldCountModel>(_dbContext.Patients.GroupBy(p => DbFunctions.TruncateTime(p.DateDeDepot))
                            .AsEnumerable().Select(p => new EntityPerFieldCountModel()
                            {
                                Field = ((DateTime)p.Key).ToString("dd/MM/yyyy"),
                                Count = p.Count()

                            }));
                        break;
                    case "Month":
                        PatientPerDateCollection = new ObservableCollection<EntityPerFieldCountModel>(_dbContext.Patients.AsEnumerable().GroupBy(p => new { p.DateDeDepot.Month, p.DateDeDepot.Year }).AsEnumerable().Select(p => new EntityPerFieldCountModel()
                            {
                                Field = (p.Key.Month).ToString(),
                                Count = p.Count()

                            }));
                        break;
                    case "Year":
                        PatientPerDateCollection = new ObservableCollection<EntityPerFieldCountModel>(_dbContext.Patients.GroupBy(p => p.DateDeDepot.Year)
                            .AsEnumerable().Select(p => new EntityPerFieldCountModel()
                            {
                                Field = p.Key.ToString(),
                                Count = p.Count()
                            }));
                        break;
                }
            });


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
        private RelayCommand<string> _perDateChangedCommand;
        public RelayCommand<string> PerDateChangedCommand
        {
            get
            {
                return _perDateChangedCommand
                    ?? (_perDateChangedCommand = new RelayCommand<string>(async (per) =>
                    {
                        await LoadPatientPerDate(per);
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
