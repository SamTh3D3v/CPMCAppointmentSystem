using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CPMCAppointmentSystem.Helpers;
using CPMCAppointmentSystem.SubModel;
using CPMCAppointmentSystem.View;
using CPMCAppointmentSystem.View.SpecialitiesViews;
using DataLayer;
using DataLayer.Model;
using GalaSoft.MvvmLight.Command;
using Syncfusion.Data.Extensions;
using CPMCAppointmentSystem.View.RcpViews;

namespace CPMCAppointmentSystem.ViewModel
{
    public class RcpViewModel: NavigableViewModelBase
    {
        #region Fields
        private bool _allDataLoaded = false;        
        private AddPatientToRcpView _addPatientToRcpView;
        private AddUserToRcpView _addParticipantsToRcpView;
        private CpmcContext _dbContext;
        private ObservableCollection<RCP> _rcpList;
        private RCP _selectedRcp;
        private bool _isFormEnabled;
        private User _selectedParticipant;
        private Patient _selectedPatient;
        private ObservableCollection<EntityToAdd<User>> _userToRcpLis ;
        #endregion
        #region Properties     
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
        public ObservableCollection<RCP> RcpList
        {
            get
            {
                return _rcpList;
            }

            set
            {
                if (_rcpList == value)
                {
                    return;
                }

                _rcpList = value;
                RaisePropertyChanged();
            }
        }
        public RCP SelectedRcp
        {
            get
            {
                return _selectedRcp;
            }

            set
            {
                if (_selectedRcp == value)
                {
                    return;
                }
                IsFormEnabled = value != null;
                _selectedRcp = value;
                RaisePropertyChanged();
            }
        }    
        public User SelectedParticipant
        {
            get
            {
                return _selectedParticipant;
            }

            set
            {
                if (_selectedParticipant == value)
                {
                    return;
                }

                _selectedParticipant = value;
                RaisePropertyChanged();
            }
        }               
        public Patient SelectedPatient
        {
            get
            {
                return _selectedPatient;
            }

            set
            {
                if (_selectedPatient == value)
                {
                    return;
                }

                _selectedPatient = value;
                RaisePropertyChanged();
            }
        }
        public ObservableCollection<EntityToAdd<User>> UsersToRcpList
        {
            get
            {
                return _userToRcpLis;
            }

            set
            {
                if (_userToRcpLis == value)
                {
                    return;
                }

                _userToRcpLis = value;
                RaisePropertyChanged();
            }
        }
        #endregion
        #region Commands
        private RelayCommand _saveRcpWhithParticipantCommand;
        public RelayCommand SaveRcpWhithParticipantCommand
        {
            get
            {
                return _saveRcpWhithParticipantCommand
                    ?? (_saveRcpWhithParticipantCommand = new RelayCommand(async () =>
                    {
                        await SavePartipantsAddedToRcp();
                        _dbContext.SaveChanges();
                        _addParticipantsToRcpView.Close();

                    }));
            }
        }
        private async Task SavePartipantsAddedToRcp()
        {
            await Task.Run(() =>
            {
                if (SelectedRcp.Participants == null)
                    SelectedRcp.Participants = new ObservableCollection<User>();
                UsersToRcpList.ForEach(uToAdd =>
                {
                    if (uToAdd.IsAdded)
                    {
                        if (SelectedRcp.Participants.All(u => u.UserId != uToAdd.Entity.UserId))
                        {
                            SelectedRcp.Participants.Add(_dbContext.Users.Find(uToAdd.Entity.UserId));
                        }
                    }
                    else
                    {
                        if (SelectedRcp.Participants.Any(u => u.UserId == uToAdd.Entity.UserId))
                        {
                            SelectedRcp.Participants.Remove(_dbContext.Users.Find(uToAdd.Entity.UserId));
                        }
                    }
                });
            });
        }
        private RelayCommand _canceRcpWithParticipantsCommand;
        public RelayCommand CanceRcpWithParticipantsCommand
        {
            get
            {
                return _canceRcpWithParticipantsCommand
                    ?? (_canceRcpWithParticipantsCommand = new RelayCommand(async () =>
                    {
                        _addParticipantsToRcpView.Close();
                        await LoadParticipantsToAddList();
                    }));
            }
        }       
        private RelayCommand _rcpViewLoadedCommand;
        public RelayCommand RcpViewLoadedCommand
        {
            get
            {
                return _rcpViewLoadedCommand
                    ?? (_rcpViewLoadedCommand = new RelayCommand(async () =>
                    {
                        _allDataLoaded = false;
                        _dbContext = new CpmcContext();
                        await LoadRcps();
                        _allDataLoaded = true;
                    }));
            }
        }
        private RelayCommand _rcpViewUnLoadedCommand;
        public RelayCommand RcpViewUnLoadedCommand
        {
            get
            {
                return _rcpViewUnLoadedCommand
                    ?? (_rcpViewUnLoadedCommand = new RelayCommand(async () =>
                    {
                        await Task.Run(() =>
                        {
                            while (!_allDataLoaded) { }
                            _dbContext.Dispose();

                        });
                    }));
            }
        }
        private RelayCommand _addRcpCommand;
        public RelayCommand AddRcpCommand
        {
            get
            {
                return _addRcpCommand
                    ?? (_addRcpCommand = new RelayCommand(
                    () =>
                    {
                        SelectedRcp = new RCP()
                        {
                            Patients = new ObservableCollection<Patient>(),
                            Participants = new ObservableCollection<User>(),
                            DateTimeRcp = DateTime.Now

                        };
                    }));
            }
        }
        private RelayCommand _addParticipantToRcp;
        public RelayCommand AddParticipantToRcp
        {
            get
            {
                return _addParticipantToRcp
                    ?? (_addParticipantToRcp = new RelayCommand(
                    () =>
                    {
                        //If a New Rcp, First add it
                        if (SelectedRcp.RcpId == Guid.Empty)
                        {
                            AddNewRcp();
                        }
                        //If a new Rcp                       

                        _addParticipantsToRcpView = new AddUserToRcpView();
                        _addParticipantsToRcpView.ShowDialog();

                    }));
            }
        }
        private RelayCommand _addPartientsToRcp;
        public RelayCommand AddPatientsToRcp
        {
            get
            {
                return _addPartientsToRcp
                    ?? (_addPartientsToRcp = new RelayCommand(
                    () =>
                    {
                        //If a New Rcp, First add it
                        if (SelectedRcp.RcpId == Guid.Empty)
                        {
                            AddNewRcp();
                        }                                            

                        _addPatientToRcpView = new AddPatientToRcpView();
                        _addPatientToRcpView.ShowDialog();
                    }));
            }
        }        
        private RelayCommand _saveRcpCommand;
        public RelayCommand SaveRcpCommand
        {
            get
            {
                return _saveRcpCommand
                    ?? (_saveRcpCommand = new RelayCommand(async () =>
                    {
                        if (SelectedRcp.RcpId == Guid.Empty)
                        {
                            AddNewRcp();
                            _dbContext.Notifications.Add(new Notification()
                            {
                                NotificationId = Guid.NewGuid(),
                                NotificationTitle = "Nouvelle Rcp",
                                NotificationMessage = "La Réunion de :" + SelectedRcp.DateTimeRcp + " a été insérée",
                                NotificationType = TypeNotification.Information,
                                IsActive = true,
                                TypeUser = TypeUserUtility.WhichTypeUser(true, false, true),
                                CreatedOn = DateTime.Now,
                                ModifiedOn = DateTime.Now
                            });
                        }
                        else
                        {
                            _dbContext.Notifications.Add(new Notification()
                            {
                                NotificationId = Guid.NewGuid(),
                                NotificationTitle = "Mise a jour d'une Rcp",
                                NotificationMessage = "La réunion du:" + SelectedRcp.DateTimeRcp + " a été mis à jour",
                                NotificationType = TypeNotification.Information,
                                IsActive = true,
                                TypeUser = TypeUserUtility.WhichTypeUser(true, false, true),
                                CreatedOn = DateTime.Now,
                                ModifiedOn = DateTime.Now
                            });
                        }
                        _dbContext.SaveChanges();
                        await LoadRcps();
                        SelectedRcp = null;
                    }));
            }
        }
        private RelayCommand _deleteRcpCommand;
        public RelayCommand DeleteRcpCommand
        {
            get
            {
                return _deleteRcpCommand
                    ?? (_deleteRcpCommand = new RelayCommand(
                    () =>
                    {                        
                        if (SelectedRcp != null)
                        {
                            if (SelectedRcp.RcpId != Guid.Empty)
                            {
                                _dbContext.Notifications.Add(new Notification()
                                {
                                    NotificationId = Guid.NewGuid(),
                                    NotificationTitle = "Suppression d'une réunion",
                                    NotificationMessage = "La réunion de :" + SelectedRcp.DateTimeRcp + " a été supprimer",
                                    NotificationType = TypeNotification.Information,
                                    IsActive = true,
                                    TypeUser = TypeUserUtility.WhichTypeUser(true, false, true),
                                    CreatedOn = DateTime.Now,
                                    ModifiedOn = DateTime.Now
                                });
                                _dbContext.Rcps.Remove(SelectedRcp);
                                RcpList.Remove(SelectedRcp);
                                _dbContext.SaveChanges();
                                SelectedRcp = null;
                            }
                        }

                    }));
            }
        }
        private RelayCommand _cancelRcpChangesCommand;
        public RelayCommand CancelRcpChangesCommand
        {
            get
            {
                return _cancelRcpChangesCommand
                    ?? (_cancelRcpChangesCommand = new RelayCommand(
                    () =>
                    {
                        if (SelectedRcp != null)
                        {
                            if (SelectedRcp.RcpId != Guid.Empty)
                                _dbContext.Entry(SelectedRcp).Reload();
                        }
                        SelectedRcp = null;
                    }));
            }
        }
        private RelayCommand _addParticiapantToRcpLoadedCommand;
        public RelayCommand AddParticiapantToRcpLoadedCommand
        {
            get
            {
                return _addParticiapantToRcpLoadedCommand
                    ?? (_addParticiapantToRcpLoadedCommand = new RelayCommand(async () =>
                    {
                        await LoadParticipantsToAddList();
                    }));
            }
        }
        private RelayCommand _addPatientToRcpLoadedCommand;
        public RelayCommand AddPatientToRcpLoadedCommand
        {
            get
            {
                return _addPatientToRcpLoadedCommand
                    ?? (_addPatientToRcpLoadedCommand = new RelayCommand(
                    () =>
                    {
                        //await LoadPatientsToAddList();
                    }));
            }
        }

        private RelayCommand _saveRcpWhithParticipantsCommand;
        public RelayCommand SaveRcpWhithParticipantsCommand
        {
            get
            {
                return _saveRcpWhithParticipantsCommand
                    ?? (_saveRcpWhithParticipantsCommand = new RelayCommand(async () =>
                    {
                        await SaveUsersAddedToRcp();
                        _dbContext.SaveChanges();
                        _addParticipantsToRcpView.Close();
                    }));
            }
        }

        private async Task SaveUsersAddedToRcp()
        {
            //await Task.Run(() =>
            //{
            //    if (SelectedSpeciality.Medecins == null)
            //        SelectedSpeciality.Medecins = new ObservableCollection<Medecin>();
            //    DoctorsToSpecialitiesList.ForEach(dToAdd =>
            //    {
            //        if (dToAdd.IsAdded)
            //        {
            //            if (SelectedSpeciality.Medecins.All(m => m.MedecinId != dToAdd.Entity.MedecinId))
            //            {
            //                SelectedSpeciality.Medecins.Add(_dbContext.Medecins.Find(dToAdd.Entity.MedecinId));
            //            }
            //        }
            //        else
            //        {
            //            if (SelectedSpeciality.Medecins.Any(m => m.MedecinId == dToAdd.Entity.MedecinId))
            //            {
            //                SelectedSpeciality.Medecins.Remove(_dbContext.Medecins.Find(dToAdd.Entity.MedecinId));
            //            }
            //        }
            //    });
            //});
        }

        
        #endregion
        #region Ctors and Methods
        public RcpViewModel(IFrameNavigationService mainFrameNavigationService, IInnerFrameNavigationService innerFrameNavigationService)
            : base(mainFrameNavigationService, innerFrameNavigationService)
        {
        }

        private async Task LoadRcps()
        {
            RcpList = new ObservableCollection<RCP>(await Task.Run(() => _dbContext.Rcps));
        }
        private void AddNewRcp()
        {
            //Added by Farouk for Audit purpose
            SelectedRcp.RcpId = Guid.NewGuid();

            _dbContext.Rcps.Add(SelectedRcp);
            IsFormEnabled = false;
        }     
      
        private async Task LoadParticipantsToAddList()
        {
            UsersToRcpList = new ObservableCollection<EntityToAdd<User>>(await Task.Run(() => _dbContext.Users.Select(s => new EntityToAdd<User>()
            {
                Entity = s
            })));
            foreach (var userToAdd in UsersToRcpList)
            {
                userToAdd.IsAdded = SelectedRcp.Participants.Any(pp => userToAdd.Entity.UserId == pp.UserId);
            }
        }
        #endregion
    }
}
