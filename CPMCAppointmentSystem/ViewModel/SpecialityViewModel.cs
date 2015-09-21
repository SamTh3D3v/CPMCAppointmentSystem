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
        private ObservableCollection<MedecinToAdd> _doctorsWhithNoSpecialiteList;
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
                IsFormEnabled = true;
                _selectedSpeciality = value;
                RaisePropertyChanged();
            }
        }
        public ObservableCollection<MedecinToAdd> DoctorsWhithNoSpecialiteList
        {
            get
            {
                return _doctorsWhithNoSpecialiteList;
            }

            set
            {
                if (_doctorsWhithNoSpecialiteList == value)
                {
                    return;
                }

                _doctorsWhithNoSpecialiteList = value;
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
                        IsFormEnabled = true;
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
                    ?? (_saveSpecialityCommand = new RelayCommand(
                    () =>
                    {
                        if (SelectedSpeciality.SpecialiteId == Guid.Empty)
                        {
                            AddNewSpeciality();
                        }
                        _dbContext.SaveChanges();
                        LoadSpacialities();
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

                    }));
            }
        }
        private RelayCommand _addDoctorToSpecialityLoadedCommand;
        public RelayCommand AddDoctorToSpecialityLoadedCommand
        {
            get
            {
                return _addDoctorToSpecialityLoadedCommand
                    ?? (_addDoctorToSpecialityLoadedCommand = new RelayCommand(
                    () =>
                    {
                        LoadDoctorsWithNoSpecialiteList();
                    }));
            }
        }

        private RelayCommand _saveSpecialityWhithDoctorsCommand;
        public RelayCommand SaveSpecialityWhithDoctorsCommand
        {
            get
            {
                return _saveSpecialityWhithDoctorsCommand
                    ?? (_saveSpecialityWhithDoctorsCommand = new RelayCommand(
                    () =>
                    {

                    }));
            }
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
                    ?? (_cancelSpecialityWhithDoctorsCommand = new RelayCommand(
                    () =>
                    {

                    }));
            }
        }
        #endregion
        #region Ctors and Methods
        public SpecialityViewModel(IFrameNavigationService mainFrameNavigationService, IInnerFrameNavigationService innerFrameNavigationService)
            : base(mainFrameNavigationService, innerFrameNavigationService)
        {
        }

        private async void LoadSpacialities()
        {
            SpecialityList = new ObservableCollection<Specialite>(await Task.Run(() => _dbContext.Specialites));
        }
        private void AddNewSpeciality()
        {
            _dbContext.Specialites.Add(SelectedSpeciality);
            IsFormEnabled = false;
        }

        private void LoadDoctorsList()
        {
            DoctorsList = new ObservableCollection<Medecin>(_dbContext.Medecins);
        }
        private async void LoadDoctorsWithNoSpecialiteList()
        {
            DoctorsWhithNoSpecialiteList = new ObservableCollection<MedecinToAdd>(await Task.Run(() => _dbContext.Medecins.Where(x => x.Speciality == null).Select(x => new MedecinToAdd()
            {
                MedecinId = x.MedecinId,               
                DateDeNaissance = x.DateDeNaissance,
                TelephoneFixe = x.TelephoneFixe,
                TelephoneMobile = x.TelephoneMobile,
                SpecialiteId = x.SpecialiteId,
                UserId = x.UserId,
                Speciality = x.Speciality,
                User = x.User,
                Pathologies = x.Pathologies,
                Patients = x.Patients,
                IsAdded = false
            })));
        }
        #endregion
    }
}
