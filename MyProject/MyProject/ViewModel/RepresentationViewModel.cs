using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkiaSharp;


namespace MyProject.ViewModel
{
    public partial class RepresentationViewModel : BaseViewModel
    {


        [ObservableProperty]
        public Chart myObservableChart;

        [ObservableProperty]
        public Chart myObservableChartDonut;

        List<ChartEntry> chartEntries = new List<ChartEntry>();
        Random random = new Random();


        public RepresentationViewModel()
        {
            if (Globals.myTitans != null) addCharacterToChartList();
        }


        private void addCharacterToChartList()
        {
            Dictionary<string, int> allegianceCounts = new Dictionary<string, int>();

            // Compter le nombre de fois que chaque allégeance apparaît
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