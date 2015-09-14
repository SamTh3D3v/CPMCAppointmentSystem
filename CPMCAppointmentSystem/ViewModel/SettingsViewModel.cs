using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CPMCAppointmentSystem.Helpers;
using DataLayer.Model;
using GalaSoft.MvvmLight.Command;

namespace CPMCAppointmentSystem.ViewModel
{
    public class SettingsViewModel:NavigableViewModelBase
    {
        #region Fields
        private CpmcContext _dbContext=new CpmcContext();       
        private ObservableCollection<Medecin> _medecinUserList ;
        private ObservableCollection<User> _agentsUserList; 
        private ObservableCollection<User> _adminsUsersList;
        private User _selectedAgentUser;
        private Medecin _selectedMedecinUser;
        #endregion
        #region Properties                        
        public ObservableCollection<Medecin> MedecinUserList
        {
            get
            {
                return _medecinUserList;
            }

            set
            {
                if (_medecinUserList == value)
                {
                    return;
                }

                _medecinUserList = value;
                RaisePropertyChanged();
            }
        }
        public ObservableCollection<User> AgentsUserList
        {
            get
            {
                return _agentsUserList;
            }

            set
            {
                if (_agentsUserList == value)
                {
                    return;
                }

                _agentsUserList = value;
                RaisePropertyChanged();
            }
        }
        public ObservableCollection<User> AdminsUserList
        {
            get
            {
                return _adminsUsersList;
            }

            set
            {
                if (_adminsUsersList == value)
                {
                    return;
                }

                _adminsUsersList = value;
                RaisePropertyChanged();
            }
        }
        public Medecin SelectedMedecinUser
        {
            get
            {
                return _selectedMedecinUser;
            }

            set
            {
                if (_selectedMedecinUser == value)
                {
                    return;
                }

                _selectedMedecinUser = value;
                RaisePropertyChanged();
            }
        }
        public User SelectedAgentUser
        {
            get
            {
                return _selectedAgentUser;
            }

            set
            {
                if (_selectedAgentUser == value)
                {
                    return;
                }

                _selectedAgentUser = value;
                RaisePropertyChanged();
            }
        }
        #endregion
        #region Commands
        private RelayCommand _settingsViewLoadedCommand;
        public RelayCommand SettingsViewLoadedCommand
        {
            get
            {
                return _settingsViewLoadedCommand
                    ?? (_settingsViewLoadedCommand = new RelayCommand(
                    () =>
                    {
                        LoadSettingsRelatedData();

                    }));
            }
        }
        
        #endregion
        #region Ctors and Methods
        public SettingsViewModel(IFrameNavigationService mainFrameNavigationService, IInnerFrameNavigationService innerFrameNavigationService)
            : base(mainFrameNavigationService, innerFrameNavigationService)
        {
        }

        private async Task LoadSettingsRelatedData()
        {
           UsersList=new ObservableCollection<User>(await Task.Run(()=>_dbContext.Users));
            
        }
        #endregion        
    }
}
