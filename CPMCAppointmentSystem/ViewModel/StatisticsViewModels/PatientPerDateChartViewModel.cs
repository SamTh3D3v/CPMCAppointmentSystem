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
using Syncfusion.Windows.Shared;

namespace CPMCAppointmentSystem.ViewModel.StatisticsViewModels
{
    public class PatientPerDateChartViewModel : StatisticsChartsViewModelBase
    {
        #region Fields
       
        private bool _isAllDataLoaded = true;        
        private String _loadPatientsPer = "Day";
        private bool _patientsWithRdvFilterIsEnabled;
        private bool _patientsWithNoRdv=true;
        private bool _patientsWithRdv;
        private bool _patentsWithDueRdv;
        private CpmcContext _dbContext;
        private ObservableCollection<EntityPerFieldCountModel> _patientPerDateCollection;
        private DateTime _dateFinDateTime = DateTime.Now;
        private DateTime _dateDebutDateTime = DateTime.Now;
        private bool _patientEntreEnabled;
        #endregion
        #region Properties 
        public bool IsAllDataLoaded
        {
            get
            {
                return _isAllDataLoaded;
            }

            set
            {
                if (_isAllDataLoaded == value)
                {
                    return;
                }

                _isAllDataLoaded = value;
                RaisePropertyChanged();
            }
        }
        public String LoadPatientsPer
        {
            get
            {
                return _loadPatientsPer;
            }

            set
            {
                if (_loadPatientsPer == value)
                {
                    return;
                }

                _loadPatientsPer = value;
                RaisePropertyChanged();
            }
        }
        public bool PatientsWithNoRdv
        {
            get
            {
                return _patientsWithNoRdv;
            }

            set
            {
                if (_patientsWithNoRdv == value)
                {
                    return;
                }

                _patientsWithNoRdv = value;
                RaisePropertyChanged();
            }
        }
        public bool PatientsWithRdv
        {
            get
            {
                return _patientsWithRdv;
            }

            set
            {
                if (_patientsWithRdv == value)
                {
                    return;
                }

                _patientsWithRdv = value;
                RaisePropertyChanged();
            }
        }
        public bool PatientWithDueRdv
        {
            get
            {
                return _patentsWithDueRdv;
            }

            set
            {
                if (_patentsWithDueRdv == value)
                {
                    return;
                }

                _patentsWithDueRdv = value;
                RaisePropertyChanged();
            }
        }                
        public bool PatientEntreEnabled
        {
            get
            {
                return _patientEntreEnabled;
            }

            set
            {
                if (_patientEntreEnabled == value)
                {
                    return;
                }

                _patientEntreEnabled = value;
                RaisePropertyChanged();
            }
        }
        public bool PatientsWithRdvFilterIsEnabled
        {
            get
            {
                return _patientsWithRdvFilterIsEnabled;
            }

            set
            {
                if (_patientsWithRdvFilterIsEnabled == value)
                {
                    return;
                }

                _patientsWithRdvFilterIsEnabled = value;
                RaisePropertyChanged();
            }
        }
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
        private RelayCommand _filterActivatedCommand;
        public RelayCommand FilterActivatedCommand
        {
            get
            {
                return _filterActivatedCommand
                    ?? (_filterActivatedCommand = new RelayCommand(async () =>
                    {
                        await LoadPatientPerDate();
                    }));
            }
        }
        private RelayCommand _patientPerDateLoadedCommand;
        public RelayCommand PatientPerDateLoadedCommand
        {
            get
            {
                return _patientPerDateLoadedCommand
                    ?? (_patientPerDateLoadedCommand = new RelayCommand(async () =>
                    {
                        _dbContext = new CpmcContext();
                        await LoadPatientPerDate();
                    }));
            }
        }
        //needs to be refactored
        private async Task LoadPatientPerDate()
        {            
            IsAllDataLoaded = false;
            await Task.Run(() =>
            {
                if (!PatientsWithRdvFilterIsEnabled)
                {
                    #region No rdv filter => Load all patients                    

                    switch (LoadPatientsPer)
                    {
                        case "Day":
                            PatientPerDateCollection = (!PatientEntreEnabled)
                                ? new ObservableCollection<EntityPerFieldCountModel>(_dbContext.Patients.GroupBy(
                                    p => DbFunctions.TruncateTime(p.DateDeDepot))
                                    .AsEnumerable().Select(p => new EntityPerFieldCountModel()
                                    {
                                        Field = ((DateTime)p.Key).ToString("dd/MM/yyyy"),
                                        Count = p.Count()

                                    }))
                                : new ObservableCollection<EntityPerFieldCountModel>
                                    (_dbContext.Patients.Where(p => DbFunctions.TruncateTime
                                        (p.DateDeDepot) > DateDebut && DbFunctions.TruncateTime
                                            (p.DateDeDepot) < DateFin)
                                        .GroupBy(p => DbFunctions.TruncateTime(p.DateDeDepot))
                                        .AsEnumerable().Select(p => new EntityPerFieldCountModel()
                                        {
                                            Field = ((DateTime)p.Key).ToString("dd/MM/yyyy"),
                                            Count = p.Count()

                                        }));
                            break;
                        case "Month":
                            PatientPerDateCollection = (!PatientEntreEnabled)
                                ? new ObservableCollection<EntityPerFieldCountModel>
                                    (_dbContext.Patients.AsEnumerable().GroupBy(p => new
                                    { p.DateDeDepot.Month, p.DateDeDepot.Year }).
                                        AsEnumerable()
                                        .OrderBy(p => p.Key.Year)
                                        .ThenBy(p => p.Key.Month)
                                        .Select(p => new EntityPerFieldCountModel()
                                        {
                                            Field = (new DateTime(p.Key.Year, p.Key.Month, 1)).ToString("MM/yyyy"),
                                            Count = p.Count()

                                        }))
                                : new ObservableCollection<EntityPerFieldCountModel>(_dbContext.Patients.Where
                                    (p =>
                                        DbFunctions.TruncateTime(p.DateDeDepot) > DateDebut &&
                                        DbFunctions.TruncateTime(p.DateDeDepot) < DateFin).
                                    AsEnumerable()
                                    .GroupBy(p => new { p.DateDeDepot.Month, p.DateDeDepot.Year })
                                    .OrderBy(p => p.Key.Year)
                                    .ThenBy(p => p.Key.Month)
                                    .AsEnumerable().Select(p => new EntityPerFieldCountModel()
                                    {
                                        Field = (new DateTime(p.Key.Year, p.Key.Month, 1)).ToString("MM/yyyy"),
                                        Count = p.Count()

                                    }));
                            break;
                        case "Year":
                            PatientPerDateCollection = (!PatientEntreEnabled)
                                ? new ObservableCollection<EntityPerFieldCountModel>(_dbContext.Patients.GroupBy(
                                    p => p.DateDeDepot.Year)
                                    .AsEnumerable().Select(p => new EntityPerFieldCountModel()
                                    {
                                        Field = p.Key.ToString(),
                                        Count = p.Count()
                                    }))
                                : new ObservableCollection<EntityPerFieldCountModel>(_dbContext.Patients.Where(
                                    p =>
                                        DbFunctions.TruncateTime(p.DateDeDepot) > DateDebut &&
                                        DbFunctions.TruncateTime(p.DateDeDepot) < DateFin)
                                    .GroupBy(p => p.DateDeDepot.Year)
                                    .AsEnumerable().Select(p => new EntityPerFieldCountModel()
                                    {
                                        Field = p.Key.ToString(),
                                        Count = p.Count()
                                    }));
                            break;
                    }

                    #endregion
                }
                else
                {
                    //this is bad code i know, don't judge me
                    if (PatientsWithNoRdv)
                    {
                        #region Patient with no rdv
                        switch (LoadPatientsPer)
                        {
                            case "Day":
                                PatientPerDateCollection = (!PatientEntreEnabled)
                                    ? new ObservableCollection<EntityPerFieldCountModel>(_dbContext.Patients.Where
                                    (p=>!p.RendezVouses.Any()).GroupBy(
                                        p => DbFunctions.TruncateTime(p.DateDeDepot))
                                        .AsEnumerable().Select(p => new EntityPerFieldCountModel()
                                        {
                                            Field = ((DateTime)p.Key).ToString("dd/MM/yyyy"),
                                            Count = p.Count()

                                        }))
                                    : new ObservableCollection<EntityPerFieldCountModel>
                                        (_dbContext.Patients.Where(p => !p.RendezVouses.Any() && DbFunctions.TruncateTime
                                            (p.DateDeDepot) > DateDebut && DbFunctions.TruncateTime
                                                (p.DateDeDepot) < DateFin)
                                            .GroupBy(p => DbFunctions.TruncateTime(p.DateDeDepot))
                                            .AsEnumerable().Select(p => new EntityPerFieldCountModel()
                                            {
                                                Field = ((DateTime)p.Key).ToString("dd/MM/yyyy"),
                                                Count = p.Count()

                                            }));
                                break;
                            case "Month":
                                PatientPerDateCollection = (!PatientEntreEnabled)
                                    ? new ObservableCollection<EntityPerFieldCountModel>
                                        (_dbContext.Patients.Where(p=> !p.RendezVouses.Any()).AsEnumerable().GroupBy(p => new
                                        { p.DateDeDepot.Month, p.DateDeDepot.Year }).
                                            AsEnumerable()
                                            .OrderBy(p => p.Key.Year)
                                            .ThenBy(p => p.Key.Month)
                                            .Select(p => new EntityPerFieldCountModel()
                                            {
                                                Field = (new DateTime(p.Key.Year, p.Key.Month, 1)).ToString("MM/yyyy"),
                                                Count = p.Count()

                                            }))
                                    : new ObservableCollection<EntityPerFieldCountModel>(_dbContext.Patients.Where
                                        (p =>
                                            !p.RendezVouses.Any() &&
                                            DbFunctions.TruncateTime(p.DateDeDepot) > DateDebut &&
                                            DbFunctions.TruncateTime(p.DateDeDepot) < DateFin).
                                        AsEnumerable()
                                        .GroupBy(p => new { p.DateDeDepot.Month, p.DateDeDepot.Year })
                                        .OrderBy(p => p.Key.Year)
                                        .ThenBy(p => p.Key.Month)
                                        .AsEnumerable().Select(p => new EntityPerFieldCountModel()
                                        {
                                            Field = (new DateTime(p.Key.Year, p.Key.Month, 1)).ToString("MM/yyyy"),
                                            Count = p.Count()

                                        }));
                                break;
                            case "Year":
                                PatientPerDateCollection = (!PatientEntreEnabled)
                                    ? new ObservableCollection<EntityPerFieldCountModel>(_dbContext.Patients.Where(p=> !p.RendezVouses.Any()).GroupBy(
                                        p => p.DateDeDepot.Year)
                                        .AsEnumerable().Select(p => new EntityPerFieldCountModel()
                                        {
                                            Field = p.Key.ToString(),
                                            Count = p.Count()
                                        }))
                                    : new ObservableCollection<EntityPerFieldCountModel>(_dbContext.Patients.Where(
                                        p =>
                                            !p.RendezVouses.Any() &&
                                            DbFunctions.TruncateTime(p.DateDeDepot) > DateDebut &&
                                            DbFunctions.TruncateTime(p.DateDeDepot) < DateFin)
                                        .GroupBy(p => p.DateDeDepot.Year)
                                        .AsEnumerable().Select(p => new EntityPerFieldCountModel()
                                        {
                                            Field = p.Key.ToString(),
                                            Count = p.Count()
                                        }));
                                break;
                        }

                        #endregion
                    }
                    else if (PatientsWithRdv)
                    {
                        #region Patient with  rdv that has'nt come yet
                        switch (LoadPatientsPer)
                        {
                            case "Day":
                                PatientPerDateCollection = (!PatientEntreEnabled)
                                    ? new ObservableCollection<EntityPerFieldCountModel>(_dbContext.Patients.Where(p => p.RendezVouses.Any(r=>r.DateTimeRdv >= DateTime.Today)).GroupBy(
                                        p => DbFunctions.TruncateTime(p.DateDeDepot))
                                        .AsEnumerable().Select(p => new EntityPerFieldCountModel()
                                        {
                                            Field = ((DateTime)p.Key).ToString("dd/MM/yyyy"),
                                            Count = p.Count()

                                        }))
                                    : new ObservableCollection<EntityPerFieldCountModel>
                                        (_dbContext.Patients.Where(p => p.RendezVouses.Any(r => r.DateTimeRdv >= DateTime.Today) && DbFunctions.TruncateTime
                                            (p.DateDeDepot) > DateDebut && DbFunctions.TruncateTime
                                                (p.DateDeDepot) < DateFin)
                                            .GroupBy(p => DbFunctions.TruncateTime(p.DateDeDepot))
                                            .AsEnumerable().Select(p => new EntityPerFieldCountModel()
                                            {
                                                Field = ((DateTime)p.Key).ToString("dd/MM/yyyy"),
                                                Count = p.Count()

                                            }));
                                break;
                            case "Month":
                                PatientPerDateCollection = (!PatientEntreEnabled)
                                    ? new ObservableCollection<EntityPerFieldCountModel>
                                        (_dbContext.Patients.Where(p => p.RendezVouses.Any(r => r.DateTimeRdv >= DateTime.Today)).AsEnumerable().GroupBy(p => new
                                        { p.DateDeDepot.Month, p.DateDeDepot.Year }).
                                            AsEnumerable()
                                            .OrderBy(p => p.Key.Year)
                                            .ThenBy(p => p.Key.Month)
                                            .Select(p => new EntityPerFieldCountModel()
                                            {
                                                Field = (new DateTime(p.Key.Year, p.Key.Month, 1)).ToString("MM/yyyy"),
                                                Count = p.Count()

                                            }))
                                    : new ObservableCollection<EntityPerFieldCountModel>(_dbContext.Patients.Where
                                        (p =>
                                            p.RendezVouses.Any(r => r.DateTimeRdv >= DateTime.Today) &&
                                            DbFunctions.TruncateTime(p.DateDeDepot) > DateDebut &&
                                            DbFunctions.TruncateTime(p.DateDeDepot) < DateFin).
                                        AsEnumerable()
                                        .GroupBy(p => new { p.DateDeDepot.Month, p.DateDeDepot.Year })
                                        .OrderBy(p => p.Key.Year)
                                        .ThenBy(p => p.Key.Month)
                                        .AsEnumerable().Select(p => new EntityPerFieldCountModel()
                                        {
                                            Field = (new DateTime(p.Key.Year, p.Key.Month, 1)).ToString("MM/yyyy"),
                                            Count = p.Count()

                                        }));
                                break;
                            case "Year":
                                PatientPerDateCollection = (!PatientEntreEnabled)
                                    ? new ObservableCollection<EntityPerFieldCountModel>(_dbContext.Patients.Where(p => p.RendezVouses.Any(r => r.DateTimeRdv >= DateTime.Today)).GroupBy(
                                        p => p.DateDeDepot.Year)
                                        .AsEnumerable().Select(p => new EntityPerFieldCountModel()
                                        {
                                            Field = p.Key.ToString(),
                                            Count = p.Count()
                                        }))
                                    : new ObservableCollection<EntityPerFieldCountModel>(_dbContext.Patients.Where(
                                        p =>
                                            p.RendezVouses.Any(r => r.DateTimeRdv >= DateTime.Today) &&
                                            DbFunctions.TruncateTime(p.DateDeDepot) > DateDebut &&
                                            DbFunctions.TruncateTime(p.DateDeDepot) < DateFin)
                                        .GroupBy(p => p.DateDeDepot.Year)
                                        .AsEnumerable().Select(p => new EntityPerFieldCountModel()
                                        {
                                            Field = p.Key.ToString(),
                                            Count = p.Count()
                                        }));
                                break;
                        }
                        #endregion
                    }
                    else if (PatientWithDueRdv)
                    {
                        #region Patient with due date rdv 
                        switch (LoadPatientsPer)
                        {
                            case "Day":
                                PatientPerDateCollection = (!PatientEntreEnabled)
                                    ? new ObservableCollection<EntityPerFieldCountModel>(_dbContext.Patients.Where(p => p.RendezVouses.Any(r => r.DateTimeRdv < DateTime.Today)).GroupBy(
                                        p => DbFunctions.TruncateTime(p.DateDeDepot))
                                        .AsEnumerable().Select(p => new EntityPerFieldCountModel()
                                        {
                                            Field = ((DateTime)p.Key).ToString("dd/MM/yyyy"),
                                            Count = p.Count()

                                        }))
                                    : new ObservableCollection<EntityPerFieldCountModel>
                                        (_dbContext.Patients.Where(p => p.RendezVouses.Any(r => r.DateTimeRdv < DateTime.Today) && DbFunctions.TruncateTime
                                            (p.DateDeDepot) > DateDebut && DbFunctions.TruncateTime
                                                (p.DateDeDepot) < DateFin)
                                            .GroupBy(p => DbFunctions.TruncateTime(p.DateDeDepot))
                                            .AsEnumerable().Select(p => new EntityPerFieldCountModel()
                                            {
                                                Field = ((DateTime)p.Key).ToString("dd/MM/yyyy"),
                                                Count = p.Count()

                                            }));
                                break;
                            case "Month":
                                PatientPerDateCollection = (!PatientEntreEnabled)
                                    ? new ObservableCollection<EntityPerFieldCountModel>
                                        (_dbContext.Patients.Where(p => p.RendezVouses.Any(r => r.DateTimeRdv < DateTime.Today)).AsEnumerable().GroupBy(p => new
                                        { p.DateDeDepot.Month, p.DateDeDepot.Year }).
                                            AsEnumerable()
                                            .OrderBy(p => p.Key.Year)
                                            .ThenBy(p => p.Key.Month)
                                            .Select(p => new EntityPerFieldCountModel()
                                            {
                                                Field = (new DateTime(p.Key.Year, p.Key.Month, 1)).ToString("MM/yyyy"),
                                                Count = p.Count()

                                            }))
                                    : new ObservableCollection<EntityPerFieldCountModel>(_dbContext.Patients.Where
                                        (p =>
                                            p.RendezVouses.Any(r => r.DateTimeRdv < DateTime.Today) &&
                                            DbFunctions.TruncateTime(p.DateDeDepot) > DateDebut &&
                                            DbFunctions.TruncateTime(p.DateDeDepot) < DateFin).
                                        AsEnumerable()
                                        .GroupBy(p => new { p.DateDeDepot.Month, p.DateDeDepot.Year })
                                        .OrderBy(p => p.Key.Year)
                                        .ThenBy(p => p.Key.Month)
                                        .AsEnumerable().Select(p => new EntityPerFieldCountModel()
                                        {
                                            Field = (new DateTime(p.Key.Year, p.Key.Month, 1)).ToString("MM/yyyy"),
                                            Count = p.Count()

                                        }));
                                break;
                            case "Year":
                                PatientPerDateCollection = (!PatientEntreEnabled)
                                    ? new ObservableCollection<EntityPerFieldCountModel>(_dbContext.Patients.Where(p => p.RendezVouses.Any(r => r.DateTimeRdv < DateTime.Today)).GroupBy(
                                        p => p.DateDeDepot.Year)
                                        .AsEnumerable().Select(p => new EntityPerFieldCountModel()
                                        {
                                            Field = p.Key.ToString(),
                                            Count = p.Count()
                                        }))
                                    : new ObservableCollection<EntityPerFieldCountModel>(_dbContext.Patients.Where(
                                        p =>
                                            p.RendezVouses.Any(r => r.DateTimeRdv < DateTime.Today) &&
                                            DbFunctions.TruncateTime(p.DateDeDepot) > DateDebut &&
                                            DbFunctions.TruncateTime(p.DateDeDepot) < DateFin)
                                        .GroupBy(p => p.DateDeDepot.Year)
                                        .AsEnumerable().Select(p => new EntityPerFieldCountModel()
                                        {
                                            Field = p.Key.ToString(),
                                            Count = p.Count()
                                        }));
                                break;
                        }
                        #endregion
                    }

                }

            });
            IsAllDataLoaded = true;            

        }
        private RelayCommand _patientPerDateUnLoadedCommand;
        public RelayCommand PatientPerDateUnLoadedCommand
        {
            get
            {
                return _patientPerDateUnLoadedCommand
                    ?? (_patientPerDateUnLoadedCommand = new RelayCommand(async () =>
                    {
                        await Task.Run(() =>
                        {
                           /* while (!_allDataLoaded) { }         //To assure that the Context isn't disposed before all the data is loaded  
                            _dbContext.Dispose();
                            PatientsWithRdvFilterIsEnabled = false;
                            PatientEntreEnabled = false;
                            PatientPerDateCollection = new ObservableCollection<EntityPerFieldCountModel>();*/
                        });                                                     

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
                        LoadPatientsPer = per;
                        await LoadPatientPerDate();
                    }));
            }
        }        
        private RelayCommand _patientWithRdvFilterCommand;
        public RelayCommand PatientWithRdvFilterCommand
        {
            get
            {
                return _patientWithRdvFilterCommand
                    ?? (_patientWithRdvFilterCommand = new RelayCommand(async () =>
                    {
                        await LoadPatientPerDate();
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
