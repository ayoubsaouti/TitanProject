using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Model
{
    public class MySerialPort
    {
        private SerialPort mySerialPort;

        public MySerialPort(string portName)
        {
             mySerialPort = new SerialPort(portName);
        }

        public void Open()
        {
             mySerialPort.Open();
        }

        public void Close()
        {
             mySerialPort.Close();
        }

        public void Write(string data)
        {
             mySerialPort.Write(data);
        }

        public string Read()
        {
            return  mySerialPort.ReadExisting();
        }

        public string ReadLine()
        {
            return  mySerialPort.ReadLine();
        }

        public event SerialDataReceivedEventHandler DataReceived
        {
            add {  mySerialPort.DataReceived += value; }
            remove {  mySerialPort.DataReceived -= value; }
        }

        public bool IsOpen
        {
            get { return  mySerialPort.IsOpen; }
        }

        public int BaudRate
        {
            get { return  mySerialPort.BaudRate; }
            set {  mySerialPort.BaudRate = value; }
        }

        public Parity Parity
        {
            get { return  mySerialPort.Parity; }
            set {  mySerialPort.Parity = value; }
        }

        public int DataBits
        {
            get { return  mySerialPort.DataBits; }
            set {  mySerialPort.DataBits = value; }
        }

        public StopBits StopBits
        {
            get { return  mySerialPort.StopBits; }
            set {  mySerialPort.StopBits = value; }
        }

        public void Dispose()
        {
            if ( mySerialPort != null)
            {
                 mySerialPort.Dispose();
                 mySerialPort = null;
            }
        }
    }
}
