using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GsmManager
{
    public class ConnectionSettings:INotifyPropertyChanged
    {
      
        private string _PortName ;
        private int _TimeOut;
        private int _baudRate;

        
        public string PortName
        {
            get
            {
                return _PortName;
            }

            set
            {
                if (_PortName == value)
                {
                    return;
                }

                _PortName = value;
                OnPropertyChanged();
            }
        }
        public int BaudRate
        {
            get
            {
                return _baudRate;
            }

            set
            {
                if (_baudRate == value)
                {
                    return;
                }

                _baudRate = value;
                OnPropertyChanged();
            }
        }

        public int TimeOut
        {
            get
            {
                return _TimeOut;
            }

            set
            {
                if (_TimeOut == value)
                {
                    return;
                }

                _TimeOut = value;
                OnPropertyChanged();
            }
        }

        public ConnectionSettings()
        {
            
        }

        public event PropertyChangedEventHandler PropertyChanged;
        
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
