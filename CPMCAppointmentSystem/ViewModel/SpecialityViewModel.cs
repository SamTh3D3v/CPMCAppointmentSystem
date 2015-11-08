using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CPMCAppointmentSystem.Helpers;
using CPMCAppointmentSystem.SubModel;
using CPMCAppointmentSystem.View;
using CPMCAppointmentSystem.View.SpecialitiesViews;
using DataLayer.Model;
using GalaSoft.MvvmLight.Command;
using Syncfusion.Data.Extensions;


namespace CPMCAppointmentSystem.ViewModel
{
    public class SpecialityViewModel : NavigableViewModelBase
    {
        #region Fields

        private AddDoctorToSpecialityView _addDoctorToSpecialityView;
        private CpmcContext _dbContext = new CpmcContext();
        private ObservableCollection<Specialite> _specialitiesList;
        private Specialite _selectedSpeciality;
        private bool _isFormEnabled;
        private Medecin _selectedDoctor;
        private ObservableCollection<Medecin> _doctorsList;
        private ObservableCollection<EntityToAdd<Medecin>> _doctorsToSpecialitiesList;
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
                return _selectedDoctor;
            }

            set
            {
                if (_selectedDoctor == value)
                {
                    return;
                }

                _selectedDoctor = value;
                RaisePropertyChanged();
            }
        }
        public bool IsFormEnabled
        {
            get
            {
                return _isFormEnabled;
            }

            set
            {
                if (_isFormEnabled == value)
                {
                    return;
                }

                _isFormEnabled = value;
                RaisePropertyChanged();
            }
        }
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
                IsFormEnabled = value != null;
                _selectedSpeciality = value;
                RaisePropertyChanged();
            }
        }
        public ObservableCollection<EntityToAdd<Medecin>> DoctorsToSpecialitiesList
        {
            get
            {
                return _doctorsToSpecialitiesList;
            }

            set
            {
                if (_doctorsToSpecialitiesList == value)
                {
                    return;
                }

                _doctorsToSpecialitiesList = value;
                RaisePropertyChanged();
            }
        }

        #endregion
        #region Commands
        private RelayCommand _addSpecialityCommand;
        public RelayCommand AddSpecialityCommand
        {
            get
            {
                return _addSpecialityCommand
                    ?? (_addSpecialityCommand = new RelayCommand(
                    () =>
                    {
                        SelectedSpeciality = new Specialite();                        
                    }));
            }
        }
        private RelayCommand _addDoctorToSpeciality;
        public RelayCommand AddDoctorToSpeciality
        {
            get
            {
                return _addDoctorToSpeciality
                    ?? (_addDoctorToSpeciality = new RelayCommand(
                    () =>
                    {
                        //If a New Patient, First add him
                        if (SelectedSpeciality.SpecialiteId == Guid.Empty)
                        {
                            AddNewSpeciality();
                        }
                        //If a new Appointement                       

                        _addDoctorToSpecialityView = new AddDoctorToSpecialityView();
                        _addDoctorToSpecialityView.ShowDialog();
                    }));
            }
        }
        private RelayCommand _specialitiesViewLoadedCommand;
        public RelayCommand SpecialitiesViewLoadedCommand
        {
            get
            {
                return _specialitiesViewLoadedCommand
                    ?? (_specialitiesViewLoadedCommand = new RelayCommand(
                    () =>
                    {
                        LoadSpacialities();
                    }));
            }
        }
        private RelayCommand _saveSpecialityCommand;
        public RelayCommand SaveSpecialityCommand
        {
            get
            {
                return _saveSpecialityCommand
                    ?? (_saveSpecialityCommand = new RelayCommand(async () =>
                    {
                        if (SelectedSpeciality.SpecialiteId == Guid.Empty)
                        {
                            AddNewSpeciality();
                        }
                        _dbContext.SaveChanges();
                        await LoadSpacialities();
                        SelectedSpeciality = null;
                    }));
            }
        }
        private RelayCommand _deleteSpecialityCommand;
        public RelayCommand DeleteSpecialityCommand
        {
            get
            {
                return _deleteSpecialityCommand
                    ?? (_deleteSpecialityCommand = new RelayCommand(
                    () =>
                    {
                        //todo Logical suppression 
                        if (SelectedSpeciality != null)
                        {
                            if (SelectedSpeciality.SpecialiteId != Guid.Empty)
                            {
                                _dbContext.Specialites.Remove(SelectedSpeciality);
                                SpecialityList.Remove(SelectedSpeciality);                                
                                _dbContext.SaveChanges();
                                SelectedSpeciality = null;
                            }
                        }

                    }));
            }
        }
        private RelayCommand _cancelSpecialityChangesCommand;
        public RelayCommand CancelSpecialityChangesCommand
        {
            get
            {
                return _cancelSpecialityChangesCommand
                    ?? (_cancelSpecialityChangesCommand = new RelayCommand(
                    () =>
                    {
                        if (SelectedSpeciality != null)
                        {
                            if (SelectedSpeciality.SpecialiteId != Guid.Empty)
                                _dbContext.Entry(SelectedSpeciality).Reload();
                        }
                        SelectedSpeciality = null;
                    }));
            }
        }
        private RelayCommand _addDoctorToSpecialityLoadedCommand;
        public RelayCommand AddDoctorToSpecialityLoadedCommand
        {
            get
            {
                return _addDoctorToSpecialityLoadedCommand
                    ?? (_addDoctorToSpecialityLoadedCommand = new RelayCommand(async () =>
                    {
                         await LoadDoctorsToAddList();
                    }));
            }
        }

        private RelayCommand _saveSpecialityWhithDoctorsCommand;
        public RelayCommand SaveSpecialityWhithDoctorsCommand
        {
            get
            {
                return _saveSpecialityWhithDoctorsCommand
                    ?? (_saveSpecialityWhithDoctorsCommand = new RelayCommand(async () =>
                    {
                        await SaveDoctorsAddedToSpeciality();
                        _dbContext.SaveChanges();
                        _addDoctorToSpecialityView.Close();
                    }));
            }
        }

        private async Task SaveDoctorsAddedToSpeciality()
        {
            await Task.Run(() =>
            {
                if (SelectedSpeciality.Medecins == null)
                    SelectedSpeciality.Medecins = new ObservableCollection<Medecin>();
                DoctorsToSpecialitiesList.ForEach(dToAdd =>
                {
                    if (dToAdd.IsAdded)
                    {
                        if (SelectedSpeciality.Medecins.All(m => m.MedecinId != dToAdd.Entity.MedecinId))
                        {
                            SelectedSpeciality.Medecins.Add(_dbContext.Medecins.Find(dToAdd.Entity.MedecinId));
                        }
                    }
                    else
                    {
                        if (SelectedSpeciality.Medecins.Any(m => m.MedecinId == dToAdd.Entity.MedecinId))
                        {
                            SelectedSpeciality.Medecins.Remove(_dbContext.Medecins.Find(dToAdd.Entity.MedecinId));
                        }
                    }
                });
            });
        }

        private RelayCommand _deleteSpecialityWhithDoctorsCommand;
        public RelayCommand DeleteSpecialityWhithDoctorsCommand
        {
            get
            {
                return _deleteSpecialityWhithDoctorsCommand
                    ?? (_deleteSpecialityWhithDoctorsCommand = new RelayCommand(
                    () =>
                    {

                    }));
            }
        }
        private RelayCommand _cancelSpecialityWhithDoctorsCommand;
        public RelayCommand CancelSpecialityWhithDoctorsCommand
        {
            get
            {
                return _cancelSpecialityWhithDoctorsCommand
                    ?? (_cancelSpecialityWhithDoctorsCommand = new RelayCommand(async () =>
                    {
                        _addDoctorToSpecialityView.Close();
                        await LoadDoctorsToAddList();
                    }));
            }
        }
        #endregion
        #region Ctors and Methods
        public SpecialityViewModel(IFrameNavigationService mainFrameNavigationService, IInnerFrameNavigationService innerFrameNavigationService)
            : base(mainFrameNavigationService, innerFrameNavigationService)
        {
        }

        private async Task LoadSpacialities()
        {
            SpecialityList = new ObservableCollection<Specialite>(await Task.Run(() => _dbContext.Specialites));
        }
        private void AddNewSpeciality()
        {
            //Added by Farouk for Audit purpose
            SelectedSpeciality.SpecialiteId = Guid.NewGuid();

            _dbContext.Specialites.Add(SelectedSpeciality);
            IsFormEnabled = false;
        }

        private void LoadDoctorsList()
        {
            DoctorsList = new ObservableCollection<Medecin>(_dbContext.Medecins);
        }
        private async Task LoadDoctorsToAddList()
        {
            DoctorsToSpecialitiesList = new ObservableCollection<EntityToAdd<Medecin>>(await Task.Run(() => _dbContext.Medecins.Select(s => new EntityToAdd<Medecin>()
            {
                Entity = s
            })));
            foreach (var docToAdd in DoctorsToSpecialitiesList)
            {
                docToAdd.IsAdded = SelectedSpeciality.Medecins.Any(dp => docToAdd.Entity.MedecinId == dp.MedecinId);
            }
        }
        #endregion
    }
}
