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
using Syncfusion.Data.Extensions;

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
            set
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
                OnPropertyChanged();
                
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
            if (!ScheduleSettingsCollection.Any())
            {
                //Get the schedule settings from the settings Xml file then save them to the database  //todo
                _dbContext.SchedulerSettings.Add(new SchedulerSetting()
                {
                    SettingName = "EnfantSetting",
                    Blink = false,
                    Color = "#ffff0000",
                    Information = "15"
                });_dbContext.SchedulerSettings.Add(new SchedulerSetting()
                {
                    SettingName = "HommeSetting",
                    Blink = false,
                    Color = "#ff00ff00"                    
                });_dbContext.SchedulerSettings.Add(new SchedulerSetting()
                {
                    SettingName = "FemmeSetting",
                    Blink = false,
                    Color = "#ff0000ff"                    
                });_dbContext.SchedulerSettings.Add(new SchedulerSetting()
                {
                    SettingName = "ProSetting",
                    Blink = false,
                    Color = "#ffffff00",                    
                });_dbContext.SchedulerSettings.Add(new SchedulerSetting()
                {
                    SettingName = "RdvSetting",
                    Blink = false,
                    Color = "#ff00ffff",
                    Information = "7"
                });
                _dbContext.SaveChanges();
                ScheduleSettingsCollection = new ObservableCollection<SchedulerSetting>(await Task.Run(() => _dbContext.SchedulerSettings));
                ScheduleSettingsCollection.CollectionChanged += (s, e) =>
                {
                    if (CollectionChanged != null)
                        CollectionChanged(s, e);
                };

            }
        }
       

        public void SaveScheduleSettingsToDataBase()
        {
            ScheduleSettingsCollection.ForEach(s =>
            {
                var setting=_dbContext.SchedulerSettings.Find(s.SchedulerSettingsId);
                if (setting!=null)
                {
                    setting.Color = s.Color;
                    setting.Information = s.Information;
                    setting.Blink = s.Blink;
                }
            });
            _dbContext.SaveChanges();
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
