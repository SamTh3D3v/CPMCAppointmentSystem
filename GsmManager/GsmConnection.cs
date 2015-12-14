using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using GsmComm.GsmCommunication;
using GsmComm.PduConverter;
using GsmComm.PduConverter.SmartMessaging;

namespace GsmManager
{
    public class GsmConnection : INotifyPropertyChanged
    {
        #region Fields
        private ConnectionSettings _connectionSettings;      
        private bool _requestImmediateDisplay  ;
        private bool _requestStatusReport;
        private bool _sendAsUnicode;
        private bool _enableSmsBatchMode;
        private bool _smcs;
        private string _smcsNumber;
        private int _sendNumber = 10;
        private bool _customSendNumber;
        private bool _customDestinationPort;
        private string _destinationPort;

        #endregion
        #region Properties
        public bool RequestImmediateDisplay
        {
            get
            {
                return _requestImmediateDisplay;
            }

            set
            {
                if (_requestImmediateDisplay == value)
                {
                    return;
                }

                _requestImmediateDisplay = value;
                OnPropertyChanged();
            }
        }
        public bool RequestStatusReport
        {
            get
            {
                return _requestStatusReport;
            }

            set
            {
                if (_requestStatusReport == value)
                {
                    return;
                }

                _requestStatusReport = value;
                OnPropertyChanged();
            }
        }
        public bool SendAsUnicode
        {
            get
            {
                return _sendAsUnicode;
            }

            set
            {
                if (_sendAsUnicode == value)
                {
                    return;
                }

                _sendAsUnicode = value;
                OnPropertyChanged();
            }
        }
        public bool EnableSmsBatchMode
        {
            get
            {
                return _enableSmsBatchMode;
            }

            set
            {
                if (_enableSmsBatchMode == value)
                {
                    return;
                }

                _enableSmsBatchMode = value;
                OnPropertyChanged();
            }
        }
        public bool Smcs
        {
            get
            {
                return _smcs;
            }

            set
            {
                if (_smcs == value)
                {
                    return;
                }

                _smcs = value;
                OnPropertyChanged();
            }
        }
        public string SmcsNumber
        {
            get
            {
                return _smcsNumber;
            }

            set
            {
                if (_smcsNumber == value)
                {
                    return;
                }

                _smcsNumber = value;
                OnPropertyChanged();
            }
        }
        public int SendNumber
        {
            get
            {
                return _sendNumber;
            }

            set
            {
                if (_sendNumber == value)
                {
                    return;
                }

                _sendNumber = value;
                OnPropertyChanged();
            }
        }      
        public bool CustomSendNumber
        {
            get
            {
                return _customSendNumber;
            }

            set
            {
                if (_customSendNumber == value)
                {
                    return;
                }

                _customSendNumber = value;
                OnPropertyChanged();
            }
        }
        public bool CustomDestinationPort
        {
            get
            {
                return _customDestinationPort;
            }

            set
            {
                if (_customDestinationPort == value)
                {
                    return;
                }

                _customDestinationPort = value;
                OnPropertyChanged();
            }
        }
        public string DestinationPort
        {
            get
            {
                return _destinationPort;
            }

            set
            {
                if (_destinationPort == value)
                {
                    return;
                }

                _destinationPort = value;
                OnPropertyChanged();
            }
        }
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
        #endregion
        #region Ctors
        public GsmConnection()
        {

        }
        public GsmConnection(ConnectionSettings conSettings)
        {
            ConnectionSettings = conSettings;
            Comm = new GsmCommMain(conSettings.PortName, conSettings.BaudRate, conSettings.TimeOut);
            Comm.Open();
        }
        #endregion
        #region Methods
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
                return ("\n PIN changed. \n");
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

        public string SendSms(string smsMessage,string number)
        {
            try
            {                
                // Send an SMS message
                SmsSubmitPdu pdu;
                if (!SendAsUnicode)
                {
                    // Send message in the default format
                    pdu = new SmsSubmitPdu(smsMessage, number);
                }
                else
                {
                    // Send message in Unicode format
                    byte dcs = (byte)DataCodingScheme.GeneralCoding.Alpha16Bit;
                    pdu = new SmsSubmitPdu(smsMessage, number, dcs);
                }

                // Request immediate display (alert)
                if (RequestImmediateDisplay)
                    pdu.DataCodingScheme |= (byte)DataCodingScheme.GeneralCoding.Class0;

                // Send message to a destination port
                if (CustomDestinationPort)
                {
                    ushort destinationPort = ushort.Parse(DestinationPort);
                    byte[] userDataHeader = SmartMessageFactory.CreatePortAddressHeader(destinationPort);
                    pdu.AddUserDataHeader(userDataHeader);
                }

                // Use an explicit SMSC if this is set
                if (Smcs)
                    pdu.SmscAddress = SmcsNumber;

                // If a status report should be generated, set that here
                if (RequestStatusReport)
                    pdu.RequestStatusReport = true;

                // Send the same message multiple times if this is set
                int times = CustomSendNumber ? SendNumber : 1;

                // If SMS batch mode should be activated, do it immediately before sending the first message
                if (EnableSmsBatchMode)
                    Comm.EnableTemporarySmsBatchMode();
                var res = "";
                // Send the message the specified number of times
                for (int i = 0; i < times; i++)
                {
                    Comm.SendMessage(pdu);
                    res += "\n Message {"+i + 1+"} of {"+times+"} sent.";                    
                }
                return res + "\n";
            }
            catch (Exception ex)
            {
                throw ex;
            }
            

        }
        #endregion
        #region Inpc related logic
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
