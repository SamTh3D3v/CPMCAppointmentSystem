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
    [Table("SchedulerSetting")]
    public class SchedulerSetting : INotifyPropertyChanged
    {
        #region Fields        
        private Guid _schedulerSettingsId;       
        private string _color;
        private bool _blink;
        private string _information;
        private string _settingName;

        #endregion
        #region Properties
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid SchedulerSettingsId
        {
            get { return _schedulerSettingsId; }
            set
            {
                if (value.Equals(_schedulerSettingsId)) return;
                _schedulerSettingsId = value;
                OnPropertyChanged();
            }
        }
        public String SettingName
        {
            get { return _settingName; }
            set
            {
                if (value == _settingName) return;
                _settingName = value;
                OnPropertyChanged();
            }
        }
        public String Color
        {
            get { return _color; }
            set
            {
                if (value == _color) return;
                _color = value;
                OnPropertyChanged();
            }
        }

        public bool Blink
        {
            get { return _blink; }
            set
            {
                if (value.Equals(_blink)) return;
                _blink = value;
                OnPropertyChanged();
            }
        }

        public String Information
        {
            get { return _information; }
            set
            {
                if (value == _information) return;
                _information = value;
                OnPropertyChanged();
            }
        } //depands on the setting it self

        #endregion
        #region INotifyPropertyChanged Related Logic
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
