using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Services
{
    // Service pour gérer l'orientation de l'appareil et les fonctionnalités liées au scanner
    public partial class DeviceOrientationService
    {
        // Buffer de file d'attente pour stocker les données du scanner
        public QueueBuffer SerialBuffer = new();

        // Méthode partielle pour configurer le scanner
        public partial void ConfigureScanner();

        // Classe dérivée de Queue pour ajouter un événement de changement
        public class QueueBuffer : Queue
        {
            // Événement déclenché lorsqu'un élément est ajouté à la file d'attente
            public event EventHandler? Changed;

            // Surcharger la méthode Enqueue pour déclencher l'événement Changed
            public override void Enqueue(object? obj)
            {
                base.Enqueue(obj);
                Changed?.Invoke(this, EventArgs.Empty); // Déclencher l'événement
            }
        }
    }
}
