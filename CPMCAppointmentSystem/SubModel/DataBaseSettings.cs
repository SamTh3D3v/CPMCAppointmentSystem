using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace CPMCAppointmentSystem.SubModel
{
    public class DataBaseSettings:INotifyPropertyChanged
    {
        
        #region Fields
        private string _serverName;
        private string _instanceName;
        private string _portNumber;
        private string _userName;

        #endregion 
        #region Properties  

        public String ServerName
        {
            get { return _serverName; }
            set
            {
                if (value == _serverName) return;
                _serverName = value;
                OnPropertyChanged();
            }
        }

        public String InstanceName
        {
            get { return _instanceName; }
            set
            {
                if (value == _instanceName) return;
                _instanceName = value;
                OnPropertyChanged();
            }
        }

        public String PortNumber
        {
            get { return _portNumber; }
            set
            {
                if (value == _portNumber) return;
                _portNumber = value;
                OnPropertyChanged();
            }
        }

        public String UserName
        {
            get { return _userName; }
            set
            {
                if (value == _userName) return;
                _userName = value;
                OnPropertyChanged();
            }
        }
        
        
        #endregion
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
