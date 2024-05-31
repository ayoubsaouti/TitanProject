using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Services
{
    public partial class DeviceOrientationService
    {
        SerialPort? mySerialPort;

        // Méthode pour configurer le scanner
        public partial void ConfigureScanner()
        {
            // Création d'un nouveau port série
            this.mySerialPort = new SerialPort();

            // Configuration des paramètres du port série
            mySerialPort.BaudRate = 9600;
            mySerialPort.PortName = Globals.portConnected;
            mySerialPort.Parity = Parity.None;
            mySerialPort.DataBits = 8;
            mySerialPort.StopBits = StopBits.One;
            mySerialPort.ReadTimeout = 1000;
            mySerialPort.WriteTimeout = 1000;

            // Gestionnaire d'événement pour la réception de données
            mySerialPort.DataReceived += new SerialDataReceivedEventHandler(DataHandler);

            try
            {
                // Ouverture du port série
                mySerialPort.Open();
            }
            catch (Exception ex)
            {
                // Affichage d'une alerte en cas d'erreur
                Shell.Current.DisplayAlert("Error!", ex.ToString(), "OK");
            }
        }

        // Méthode pour gérer les données reçues
        private void DataHandler(object sender, EventArgs arg)
        {
            SerialPort sp = (SerialPort)sender;

            // Ajout des données reçues à la file d'attente
            SerialBuffer.Enqueue(sp.ReadTo("\r"));
        }
    }
}
