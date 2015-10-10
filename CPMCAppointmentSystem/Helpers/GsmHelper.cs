using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace CPMCAppointmentSystem.Helpers
{
    public class GsmHelper : INotifyPropertyChanged
    {
        #region Consts
        private const int SleepTimeStamp = 1000;
        #endregion
        #region Fields
        private String _portName;
        private readonly SerialPort _serialPort;
        private string _messageCenterNumber;
        private int _baudRate;
        #endregion
        #region Properties
        public String PortName
        {
            get
            {
                return _portName;
            }

            set
            {
                if (_portName == value)
                {
                    return;
                }

                _portName = value;
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
        public string MessageCenterNumber
        {
            get
            {
                return _messageCenterNumber;
            }

            set
            {
                if (_messageCenterNumber == value)
                {
                    return;
                }

                _messageCenterNumber = value;
                OnPropertyChanged();
            }
        }

        #endregion
        #region Ctors
        public GsmHelper(int baudRate, string portName, string messageCenterNumber)
        {
            _serialPort = new SerialPort(portName, baudRate);
            MessageCenterNumber = messageCenterNumber;
            Thread.Sleep(SleepTimeStamp);
        }
        #endregion
        #region Methods

        public void SendSms(string number, string message)
        {
            _serialPort.Open();
            _serialPort.Write("AT+CMGF=1\r");
            Thread.Sleep(SleepTimeStamp);
            _serialPort.Write("AT+CSCA=\"" + MessageCenterNumber + "\"\r");
            Thread.Sleep(SleepTimeStamp);
            _serialPort.Write("AT+CMGS=\"" + number + "\"\r");
            Thread.Sleep(SleepTimeStamp);
            _serialPort.Write(message + "\x1A");

            Thread.Sleep(SleepTimeStamp);
            _serialPort.Close();
        }
        public void Callphone(string number)
        {
            _serialPort.Open();
            _serialPort.Write("ATD + +"+number+";");
            Thread.Sleep(100);
            _serialPort.Close();
        }


        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
