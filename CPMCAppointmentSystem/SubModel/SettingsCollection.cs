using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Model;
using JetBrains.Annotations;

namespace CPMCAppointmentSystem.SubModel
{
    public class SettingsCollection:INotifyPropertyChanged,INotifyCollectionChanged
    {        
        #region Fields

        private readonly CpmcContext _dbContext;
        private ObservableCollection<SchedulerSetting> _scheduleSettingsCollection;        
        #endregion
        #region Properties
        public ObservableCollection<SchedulerSetting> ScheduleSettingsCollection
        {
            get { return _scheduleSettingsCollection; }
            set
            {
                if (Equals(value, _scheduleSettingsCollection)) return;
                _scheduleSettingsCollection = value;
                OnPropertyChanged();
            }
        }

        #endregion
        #region Indexers
        public SchedulerSetting this[String settingName]
        {
            get
            {
                if (ScheduleSettingsCollection == null) return null;               
                return ScheduleSettingsCollection.FirstOrDefault(s => s.SettingName == settingName);
            }
            internal set
            {
                if (ScheduleSettingsCollection != null)
                {
                    var setting = _dbContext.SchedulerSettings.FirstOrDefault(s => s.SettingName == settingName);
                    if (setting!=null)
                    {
                        setting.Blink = value.Blink;
                        setting.Color = value.Color;
                        setting.Information = value.Information;
                        _dbContext.SaveChanges();
                    }
                   
                }
                
            }

        }
        
        #endregion
        #region Ctors and Methods
        public SettingsCollection()
        {
           _dbContext=new CpmcContext();
           
        }
        public async Task LoadSchedulerSettings()
        {
            ScheduleSettingsCollection=new ObservableCollection<SchedulerSetting>(await Task.Run(() => _dbContext.SchedulerSettings));
            ScheduleSettingsCollection.CollectionChanged += (s, e) =>
            {
                if (CollectionChanged != null)
                    CollectionChanged(s, e);
            };
            if (!ScheduleSettingsCollection.Any())
            {
                //Get the schedule settings from the Xml file then save them to the database                
            }
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;
    }
}
