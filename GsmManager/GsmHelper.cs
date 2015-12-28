using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GsmComm.GsmCommunication;

namespace GsmManager
{
    public static class GsmHelper
    {
        public static List<string> GetAvailablePortNamesInDevice()
        {
            return SerialPort.GetPortNames().ToList();
        }
        public static List<string> GetUsualPortNames()
        {
            return new List<string>()
            {
                "COM1",
                "COM2",
                "COM3",
                "COM4",
                "COM5",
                "COM6",
                "COM7",
                "COM8",
                "COM9",
                "COM10",
                "COM11",
                "COM12",
                "COM13",
                "COM14",
                "COM15",
                "COM16",
                "COM17",
            };
        }
        public static List<int> GetUsualBaudRate()
        {
            return new List<int>()
            {
                9600,
                19200,
                38400,
                57600,
                115200
            };
        }
        public static List<int> GetUsualTimeOuts()
        {
            return new List<int>()
            {
                150,
                300,
                600,
                900,
                1200,
                1500,
                1800,
                2000
            };
        }
        public static ConnectionSettings EnterNewSettings(string port, string baudRate, string timeOut)
        {
            int newBaudRate;
            int newTimeout;
            if (port.Length == 0)
                throw new FormatException("Invalid port name");
            var newPortName = port;
            try
            {
                newBaudRate = int.Parse(baudRate);
            }
            catch (Exception)
            {
                throw new FormatException("Invalid baud rate");

            }
            try
            {
                newTimeout = int.Parse(timeOut);
            }
            catch (Exception)
            {
                throw new FormatException("Invalid timeout value");

            }
            return new ConnectionSettings()
            {
                PortName = newPortName,
                BaudRate = newBaudRate,
                TimeOut = newTimeout
            };
        }        
        public static bool TestConnection(ConnectionSettings tcon)
        {            
            GsmCommMain comm = new GsmCommMain(tcon.PortName, tcon.BaudRate, tcon.TimeOut);
            try
            {
                comm.Open();
                if (!comm.IsConnected())
                {
                    comm.Close();
                    return false;
                }

                comm.Close();
            }
            catch (Exception ex)
            {
                comm.Close();
                throw new Exception("Connection error: " + ex.Message);                
            }
            return true; //true:  if connection succeeded
        }
    }
}
