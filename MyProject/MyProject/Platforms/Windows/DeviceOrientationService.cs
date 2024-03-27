using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Management;

namespace MyProject
{
    public partial class DeviceOrientationServices
    {
        SerialPort mySerialPort;
        Queue<string> SerialBuffer = new Queue<string>(); // Déclaration et initialisation de SerialBuffer

        public bool ConfigureScanner(bool connect)
        {
            this.mySerialPort = new SerialPort();

            string ComPort = "";

            using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PnPEntity WHERE Caption like '%(COM%'"))
            {
                var ports = searcher.Get().Cast<ManagementBaseObject>().ToList().Select(p => p["Caption"].ToString());

                var portList = SerialPort.GetPortNames().Select(n => n + " - " + ports.FirstOrDefault(s => s.Contains(n))).ToList();

                foreach (string s in portList)
                {
                    if (s.Contains("GMAS"))
                    {
                        string[] data = s.Split(" - ");
                        ComPort = data[0];
                    }
                }
            }

            try
            {
                if (connect && !mySerialPort.IsOpen)
                {
                    mySerialPort.PortName = ComPort;
                    mySerialPort.BaudRate = 9600;
                    mySerialPort.Parity = Parity.None;
                    mySerialPort.DataBits = 8;
                    mySerialPort.StopBits = StopBits.One;

                    mySerialPort.ReadTimeout = 1000;
                    mySerialPort.WriteTimeout = 1000;

                    mySerialPort.DataReceived += new SerialDataReceivedEventHandler(DataHandler);
                    mySerialPort.Open();
                }
                else if (!connect)
                {
                    if (mySerialPort.IsOpen) mySerialPort.Close();
                }

                connect = true;

                return connect;
            }
            catch
            {
                // Gérer l'erreur ici
                connect = false;
                return connect;
            }
        }

        private void DataHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;

            SerialBuffer.Enqueue(sp.ReadTo("\r"));
        }
    }
}
