using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Model;
using JetBrains.Annotations;
using Syncfusion.Data.Extensions;

namespace CPMCAppointmentSystem.Helpers
{
    public class GsmHelper : INotifyPropertyChanged
    {
     
        #region Fields
        private String _portName=String.Empty;
        private string _messageCenterNumber;
        private int _baudRate;
        #endregion
        #region Properties
        public int DelayBetweenAtCmds { get; set; }      
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
        public GsmHelper(int baudRate)
        {                         
            BaudRate = baudRate;            
        }

        public async Task InitGsmDevice()
        {
            GetSmsSettings();
            if (PortName==String.Empty)
            {
                var validport = "";
                await Task.Run(() => SerialPort.GetPortNames().ForEach((str) =>
                {
                    _serialPort = new SerialPort(str, BaudRate);                    
                    try
                    {
                        _serialPort.Open();
                        if (CheckExistingModemOnComPort(_serialPort))
                            validport = str;
                        _serialPort.Close();
                    }
                    catch
                    {
                        _serialPort.Close();
                        return;
                    }
                }));
                if (validport == "")
                    throw new Exception("The GSM device isn't pluged-in");
                    //validport = "COM9";
                PortName = validport;
                _serialPort = new SerialPort(validport, BaudRate); 
            }
        }      

        private void GetSmsSettings()
        {
            MessageCenterNumber = ParameterManager.GetValue<string>(ParameterNames.SMSCenterNumber);
            DelayBetweenAtCmds = ParameterManager.GetValue<int>(ParameterNames.DelayBetweenATCommand);
        }

        public bool CheckExistingModemOnComPort(SerialPort serialPort)
        {

            var modemCommands = new string[] { "AT",       // Check connected modem. After 'AT' command some modems autobaud their speed.
                                               "ATQ0" };   // Switch on confirmations
            serialPort.DtrEnable = true;    // Set Data Terminal Ready (DTR) signal 
            serialPort.RtsEnable = true;
            foreach (string command in modemCommands)
            {
                serialPort.Write(command + "\r");
                Thread.Sleep(2000);
                var answer = serialPort.ReadExisting();
                if (answer.IndexOf("OK", System.StringComparison.Ordinal) >= 0)
                {
                    return true;
                }
            }
            return false;
        }

        #endregion
        #region Methods
        public void SendSms(string number, string message)
        {

            _serialPort.Open();
            _serialPort.Write("AT+CMGF=1\r");
            Thread.Sleep(DelayBetweenAtCmds);
            _serialPort.Write("AT+CSCA=\"" + MessageCenterNumber + "\"\r");
            Thread.Sleep(DelayBetweenAtCmds);
            _serialPort.Write("AT+CMGS=\"" + number + "\"\r");
            Thread.Sleep(DelayBetweenAtCmds);
            _serialPort.Write(message + "\x1A");
            Thread.Sleep(DelayBetweenAtCmds);
            _serialPort.Close();
        }

        private SerialPort _serialPort;
        public void Callphone(string number)
        {
            _serialPort.Open();
            _serialPort.Write("ATD + +" + number + ";");
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
