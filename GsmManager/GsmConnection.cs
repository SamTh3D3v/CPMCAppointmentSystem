using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using GsmComm.GsmCommunication;

namespace GsmManager
{
    public class GsmConnection : INotifyPropertyChanged
    {

        private ConnectionSettings _connectionSettings;
        public ConnectionSettings ConnectionSettings
        {
            get
            {
                return _connectionSettings;
            }

            set
            {
                if (_connectionSettings == value)
                {
                    return;
                }

                _connectionSettings = value;
                OnPropertyChanged();
            }
        }
        public GsmCommMain Comm { get; set; }
        public GsmConnection()
        {

        }

        public GsmConnection(ConnectionSettings conSettings)
        {
            ConnectionSettings = conSettings;
            Comm = new GsmCommMain(conSettings.PortName, conSettings.BaudRate, conSettings.TimeOut);
        }
        

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
