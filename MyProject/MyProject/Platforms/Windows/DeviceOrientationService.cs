using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Service;
public partial class DeviceOrientationService
{
    SerialPort? mySerialPort;
    public partial void ConfigureScanner()
    {
        this.mySerialPort = new();

        mySerialPort.BaudRate = 9600;
        mySerialPort.PortName = "COM3";
        mySerialPort.Parity = Parity.None;
        mySerialPort.DataBits = 8;
        mySerialPort.StopBits = StopBits.One;
        mySerialPort.ReadTimeout = 1000;
        mySerialPort.WriteTimeout = 1000;

        mySerialPort.DataReceived += new SerialDataReceivedEventHandler(DataHandler);

        try
        {
            mySerialPort.Open();
        }
        catch (Exception ex)
        {
            Shell.Current.DisplayAlert("Error!", ex.ToString(), "OK");
        }
    }
    private void DataHandler(object sender, EventArgs arg)
    {
        SerialPort sp = (SerialPort)sender;

        SerialBuffer.Enqueue(sp.ReadTo("\r"));
    }
}
