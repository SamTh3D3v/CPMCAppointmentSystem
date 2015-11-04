using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Annotations;

namespace DataLayer.Model
{
    [Table("RolesCollection")]
    public class RolesCollection : INotifyPropertyChanged
    {
        #region Fields
        private Guid _rolesCollectionId;
        private bool _appointementViewAllow;
        private bool _appointementEditAllow;
        private bool _doctorsViewAllow;
        private bool _doctorsAddAllow;
        private bool _patientsViewAllow;
        private bool _patientsEditAllow;
        private bool _patientsEditAppointementAllow;
        private bool _specialitiesViewAllow;
        private bool _specialitiesEditAllow;
        private bool _pathologiesViewAllow;
        private bool _pathologiesEditAllow;
        private bool _myPatientsViewAllow;
        private bool _myPatientsEditAllow;
        private bool _myPatientsEditAppointementAllow;
        private bool _settingsViewUsersAllow;
        private bool _settingsEditUsersAllow;
        private bool _settingsMangeThemeAllow;
        private bool _smsNotificationViewAllow;
        private bool _smsNotificationEditAllow;
        private bool _statisticsViewAllow;
        private bool _logViewAllow;
        private bool _logEditAllow;

        #endregion
        #region Properties
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid RolesCollectionId
        {
            get { return _rolesCollectionId; }
            set
            {
                if (value.Equals(_rolesCollectionId)) return;
                _rolesCollectionId = value;
                OnPropertyChanged();
            }
        }

        //Roles

        #region CalendarView

        public bool AppointementViewAllow
        {
            get { return _appointementViewAllow; }
            set
            {
                if (value.Equals(_appointementViewAllow)) return;
                _appointementViewAllow = value;
                OnPropertyChanged();
            }
        }

        public bool AppointementEditAllow
        {
            get { return _appointementEditAllow; }
            set
            {
                if (value.Equals(_appointementEditAllow)) return;
                _appointementEditAllow = value;
                OnPropertyChanged();
            }
        }

        #endregion
        #region Doctors View

        public bool DoctorsViewAllow
        {
            get { return _doctorsViewAllow; }
            set
            {
                if (value.Equals(_doctorsViewAllow)) return;
                _doctorsViewAllow = value;
                OnPropertyChanged();
            }
        }

        public bool DoctorsAddAllow
        {
            get { return _doctorsAddAllow; }
            set
            {
                if (value.Equals(_doctorsAddAllow)) return;
                _doctorsAddAllow = value;
                OnPropertyChanged();
            }
        }

        #endregion
        #region Patient View

        public bool PatientsViewAllow
        {
            get { return _patientsViewAllow; }
            set
            {
                if (value.Equals(_patientsViewAllow)) return;
                _patientsViewAllow = value;
                OnPropertyChanged();
            }
        }

        public bool PatientsEditAllow
        {
            get { return _patientsEditAllow; }
            set
            {
                if (value.Equals(_patientsEditAllow)) return;
                _patientsEditAllow = value;
                OnPropertyChanged();
            }
        }

        public bool PatientsEditAppointementAllow
        {
            get { return _patientsEditAppointementAllow; }
            set
            {
                if (value.Equals(_patientsEditAppointementAllow)) return;
                _patientsEditAppointementAllow = value;
                OnPropertyChanged();
            }
        }

        #endregion
        #region SpecialitePrincipale View

        public bool SpecialitiesViewAllow
        {
            get { return _specialitiesViewAllow; }
            set
            {
                if (value.Equals(_specialitiesViewAllow)) return;
                _specialitiesViewAllow = value;
                OnPropertyChanged();
            }
        }

        public bool SpecialitiesEditAllow
        {
            get { return _specialitiesEditAllow; }
            set
            {
                if (value.Equals(_specialitiesEditAllow)) return;
                _specialitiesEditAllow = value;
                OnPropertyChanged();
            }
        }

        #endregion
        #region Pathology View

        public bool PathologiesViewAllow
        {
            get { return _pathologiesViewAllow; }
            set
            {
                if (value.Equals(_pathologiesViewAllow)) return;
                _pathologiesViewAllow = value;
                OnPropertyChanged();
            }
        }

        public bool PathologiesEditAllow
        {
            get { return _pathologiesEditAllow; }
            set
            {
                if (value.Equals(_pathologiesEditAllow)) return;
                _pathologiesEditAllow = value;
                OnPropertyChanged();
            }
        }

        #endregion
        #region MyPatients View

        public bool MyPatientsViewAllow
        {
            get { return _myPatientsViewAllow; }
            set
            {
                if (value.Equals(_myPatientsViewAllow)) return;
                _myPatientsViewAllow = value;
                OnPropertyChanged();
            }
        }

        public bool MyPatientsEditAllow
        {
            get { return _myPatientsEditAllow; }
            set
            {
                if (value.Equals(_myPatientsEditAllow)) return;
                _myPatientsEditAllow = value;
                OnPropertyChanged();
            }
        }

        public bool MyPatientsEditAppointementAllow
        {
            get { return _myPatientsEditAppointementAllow; }
            set
            {
                if (value.Equals(_myPatientsEditAppointementAllow)) return;
                _myPatientsEditAppointementAllow = value;
                OnPropertyChanged();
            }
        }

        #endregion
        #region Settings View

        public bool SettingsViewUsersAllow
        {
            get { return _settingsViewUsersAllow; }
            set
            {
                if (value.Equals(_settingsViewUsersAllow)) return;
                _settingsViewUsersAllow = value;
                OnPropertyChanged();
            }
        }

        public bool SettingsEditUsersAllow
        {
            get { return _settingsEditUsersAllow; }
            set
            {
                if (value.Equals(_settingsEditUsersAllow)) return;
                _settingsEditUsersAllow = value;
                OnPropertyChanged();
            }
        }

        public bool SettingsMangeThemeAllow
        {
            get { return _settingsMangeThemeAllow; }
            set
            {
                if (value.Equals(_settingsMangeThemeAllow)) return;
                _settingsMangeThemeAllow = value;
                OnPropertyChanged();
            }
        }

        #endregion
        #region Sms Notification View

        public bool SmsNotificationViewAllow
        {
            get { return _smsNotificationViewAllow; }
            set
            {
                if (value.Equals(_smsNotificationViewAllow)) return;
                _smsNotificationViewAllow = value;
                OnPropertyChanged();
            }
        }

        public bool SmsNotificationEditAllow
        {
            get { return _smsNotificationEditAllow; }
            set
            {
                if (value.Equals(_smsNotificationEditAllow)) return;
                _smsNotificationEditAllow = value;
                OnPropertyChanged();
            }
        }

        #endregion
        #region Statistics View

        public bool StatisticsViewAllow
        {
            get { return _statisticsViewAllow; }
            set
            {
                if (value.Equals(_statisticsViewAllow)) return;
                _statisticsViewAllow = value;
                OnPropertyChanged();
            }
        }

        #endregion
        #region Log View
        public bool LogViewAllow
        {
            get
            {
                return _logViewAllow;
            }

            set
            {
                if (_logViewAllow == value)
                {
                    return;
                }

                _logViewAllow = value;
                OnPropertyChanged();
            }
        }
        public bool LogEditAllow    
        {
            get { return _logEditAllow; }
            set
            {
                if (value == _logEditAllow) return;
                _logEditAllow = value;
                OnPropertyChanged();
            }
        }

        #endregion
        #endregion
        #region INotifyPropertyChanged related

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
