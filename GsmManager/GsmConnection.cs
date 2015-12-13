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
            Comm.Open();
        }

        public bool IsConnected()
        {

            try
            {
                return Comm.IsConnected();

            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public string GetPinStatusCommand()
        {
            try
            {
                // Get PIN status
                PinStatus status = Comm.GetPinStatus();
                return ("\n PIN status: " + status.ToString() + "\n");
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public string IdentifyDevice()
        {
            try
            {
                IdentificationInfo info = Comm.IdentifyDevice();
                return "\n Manufacturer: " + info.Manufacturer
                + "\n Model: " + info.Model
                + "\n Revision: " + info.Revision
                + "\n Serial number: " + info.SerialNumber + "\n";
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public string GetSignalQuality()
        {
            try
            {                
                SignalQualityInfo info = Comm.GetSignalQuality();
                return "Signal strength: " + info.SignalStrength.ToString()
                       + "\n Bit error rate: " + info.BitErrorRate.ToString() + "\n";
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public string ChangePinCode(string newPin)
        {
            try
            {
                // Enter PIN
                Comm.EnterPin(newPin);
                return  ("\n PIN changed. \n");                
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public string ResetToDefaultConfig()
        {
            try
            {                
                Comm.ResetToDefaultConfig();
                return "\n Config reset \n";                
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
