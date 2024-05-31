using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkiaSharp;

namespace MyProject.ViewModel
{
    // ViewModel pour la représentation graphique
    public partial class RepresentationViewModel : BaseViewModel
    {
        // Propriété observée pour le graphique en ligne
        [ObservableProperty]
        public Chart myObservableChart;

        // Propriété observée pour le graphique en donut
        [ObservableProperty]
        public Chart myObservableChartDonut;

        // Liste des entrées de graphique
        List<ChartEntry> chartEntries = new List<ChartEntry>();

        // Générateur de nombres aléatoires
        Random random = new Random();

        // Constructeur
        public RepresentationViewModel()
        {
            // Si la liste des titans est disponible, ajoute les personnages aux graphiques
            if (Globals.myTitans != null)
                AddCharacterToChartList();
        }

        // Méthode privée pour ajouter les personnages à la liste des entrées de graphique
        private void AddCharacterToChartList()
        {
            // Dictionnaire pour compter le nombre de fois que chaque allégeance apparaît
            Dictionary<string, int> allegianceCounts = new Dictionary<string, int>();

            // Parcourir les titans pour compter les allégeances
            foreach (Titan titan in Globals.myTitans)
            {
                string allegiance = titan.Allegiance;
                if (allegianceCounts.ContainsKey(allegiance))
                {
                    allegianceCounts[allegiance]++;
                }
                else
                {
                    allegianceCounts[allegiance] = 1;
                }
            }

            // Créer des entrées de graphique à partir des comptes d'allégeance
            List<ChartEntry> chartEntries = new List<ChartEntry>();
            foreach (var id in allegianceCounts)
            {
                // Générer une couleur aléatoire pour chaque entrée
                SKColor randomColor = new SKColor((byte)random.Next(0, 256), (byte)random.Next(0, 256), (byte)random.Next(0, 256));
                ChartEntry entry = new ChartEntry(id.Value)
                {
                    Label = id.Key,
                    ValueLabel = id.Value.ToString(),
                    Color = randomColor
                };
                chartEntries.Add(entry);
            }

            // Créer les graphiques avec les entrées créées
            MyObservableChart = new LineChart { Entries = chartEntries.ToArray() };
            myObservableChartDonut = new DonutChart { Entries = chartEntries.ToArray() };
        }
    }
}
