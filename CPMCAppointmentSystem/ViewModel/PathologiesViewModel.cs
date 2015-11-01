using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using CPMCAppointmentSystem.Helpers;
using CPMCAppointmentSystem.SubModel;
using CPMCAppointmentSystem.View;
using CPMCAppointmentSystem.View.PathologiesViews;
using DataLayer.Model;
using GalaSoft.MvvmLight.Command;
using Syncfusion.Data.Extensions;


namespace CPMCAppointmentSystem.ViewModel
{
    public class PathologiesViewModel : NavigableViewModelBase
    {

        #region Fields
        private AddDoctorsToPathologyView _addDoctorsToPathologyView;
        private bool _isFormEnabled;
        private readonly CpmcContext _dbContext = new CpmcContext();
        private ObservableCollection<Pathology> _pathologiesList;
        private Pathology _selectedPathology;
        private Medecin _selectedDoctorWithinPathology;
        private ObservableCollection<EntityToAdd<Medecin>> _doctorsToPathlogyList;
        #endregion
        #region Properties
        public ObservableCollection<EntityToAdd<Medecin>> DoctorsToPathlogyList
        {
            get
            {
                return _doctorsToPathlogyList;
            }

            set
            {
                if (_doctorsToPathlogyList == value)
                {
                    return;
                }

                _doctorsToPathlogyList = value;
                RaisePropertyChanged();
            }
        }
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
                IsFormEnabled = value != null;
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
        #endregion
        #region Commands
        private RelayCommand _pathologyViewLoadedCommand;
        public RelayCommand PathologyViewLoadedCommand
        {
            get
            {
                return _pathologyViewLoadedCommand
                    ?? (_pathologyViewLoadedCommand = new RelayCommand(async () =>
                    {
                        await LoadPathologies();

                    }));
            }
        }
        private RelayCommand _addDoctorToPathologyLoadedCommand;
        public RelayCommand AddDoctorToPathologyLoadedCommand
        {
            get
            {
                return _addDoctorToPathologyLoadedCommand
                    ?? (_addDoctorToPathologyLoadedCommand = new RelayCommand(async () =>
                    {
                        await LoadDoctorsToAddList();
                    }));
            }
        }
        private async Task LoadDoctorsToAddList()
        {
            DoctorsToPathlogyList = new ObservableCollection<EntityToAdd<Medecin>>(await Task.Run(() => _dbContext.Medecins.Select(s => new EntityToAdd<Medecin>()
            {
                Entity = s
            })));
            foreach (var docToAdd in DoctorsToPathlogyList)
            {
                docToAdd.IsAdded = SelectedPathology.Medecins.Any(dp => docToAdd.Entity.MedecinId == dp.MedecinId);
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
                        SelectedPathology = new Pathology();
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
                        if (SelectedPathology.PathologyId == Guid.Empty)
                        {
                            AddNewPathology();
                        }
                        _addDoctorsToPathologyView = new AddDoctorsToPathologyView();
                        _addDoctorsToPathologyView.ShowDialog();

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
                        if (SelectedPathology.PathologyId == Guid.Empty)
                        {
                            AddNewPathology();
                            NotficationManager.AddNotification(new Notification()
                            {
                                NotificationId = Guid.NewGuid(),
                                NotificationTitle = "New",
                                NotificationMessage = "Pathology " + SelectedPathology.NomPathology + " a été inserer avec succes",
                                NotificationType = TypeNotification.Information
                            });
                        }
                        else
                        {
                            NotficationManager.AddNotification(new Notification()
                            {
                                NotificationId = Guid.NewGuid(),
                                NotificationTitle = "Update",
                                NotificationMessage = "Pathology " + SelectedPathology.NomPathology + " a été mise a jour avec succes",
                                NotificationType = TypeNotification.Information
                            });
                        }
                        _dbContext.SaveChanges();
                        LoadPathologies();
                        SelectedPathology = null;
                    }));
            }
        }

        private RelayCommand _savePathologyWhithDoctorsCommand;
        public RelayCommand SavePathologyWhithDoctorsCommand
        {
            get
            {
                return _savePathologyWhithDoctorsCommand
                    ?? (_savePathologyWhithDoctorsCommand = new RelayCommand(async () =>
                        {
                            await SaveDoctorsAddedToPathology();
                            _dbContext.SaveChanges();
                            _addDoctorsToPathologyView.Close();
                        }));
            }
        }

        private async Task SaveDoctorsAddedToPathology()
        {
            await Task.Run(() =>
            {
                if (SelectedPathology.Medecins == null)
                    SelectedPathology.Medecins = new ObservableCollection<Medecin>();
                DoctorsToPathlogyList.ForEach(dToAdd =>
                {
                    if (dToAdd.IsAdded)
                    {
                        if (SelectedPathology.Medecins.All(m => m.MedecinId != dToAdd.Entity.MedecinId))
                        {
                            SelectedPathology.Medecins.Add(_dbContext.Medecins.Find(dToAdd.Entity.MedecinId));
                        }
                    }
                    else
                    {
                        if (SelectedPathology.Medecins.Any(m => m.MedecinId == dToAdd.Entity.MedecinId))
                        {
                            SelectedPathology.Medecins.Remove(_dbContext.Medecins.Find(dToAdd.Entity.MedecinId));
                        }
                    }
                });

            });
        }

        private RelayCommand _deletePathologyWhithDoctorsCommand;
        public RelayCommand DeletePathologyWhithDoctorsCommand
        {
            get
            {
                return _deletePathologyWhithDoctorsCommand
                    ?? (_deletePathologyWhithDoctorsCommand = new RelayCommand(
                    () =>
                    {

                    }));
            }
        }
        private RelayCommand _cancelPathologyWhithDoctorsCommand;
        public RelayCommand CancelPathologyWhithDoctorsCommand
        {
            get
            {
                return _cancelPathologyWhithDoctorsCommand
                    ?? (_cancelPathologyWhithDoctorsCommand = new RelayCommand(async () =>
                        {
                            _addDoctorsToPathologyView.Close();
                            await LoadDoctorsToAddList();
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
                        //todo Logical suppression 
                        if (SelectedPathology != null)
                        {
                            if (SelectedPathology.PathologyId != Guid.Empty)
                            {
                                _dbContext.Pathologies.Remove(SelectedPathology);
                                PathologiesList.Remove(SelectedPathology);
                                _dbContext.SaveChanges();
                                SelectedPathology = null;
                            }
                        }

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
                        if (SelectedPathology != null)
                        {
                            if (SelectedPathology.PathologyId != Guid.Empty)
                                _dbContext.Entry(SelectedPathology).Reload();
                        }
                        SelectedPathology = null;
                    }));
            }
        }

        #endregion
        #region Ctors and Methods
        public PathologiesViewModel(IFrameNavigationService mainFrameNavigationService, IInnerFrameNavigationService innerFrameNavigationService)
            : base(mainFrameNavigationService, innerFrameNavigationService)
        {
        }
        private async Task LoadPathologies()
        {
            PathologiesList = new ObservableCollection<Pathology>(await Task.Run(() => _dbContext.Pathologies));
        }

        private void AddNewPathology()
        {
            _dbContext.Pathologies.Add(SelectedPathology);
        }
        #endregion
    }
}
