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
        public Chart myObservableChart = new RadarChart();

        public ObservableCollection<ChartEntry> Entries { get; } = new ObservableCollection<ChartEntry>();



        private void LoadData()
        {
            // Supposons que vous ayez une liste de Titans
            var titans = GetTitansFromSomewhere();

            // Pour chaque Titan, créez un ChartEntry avec Name et Allegiance
            foreach (var titan in titans)
            {
                Entries.Add(new ChartEntry(1) // La valeur n'importe pas pour un radar
                {
                    Label = titan.Name,
                    ValueLabel = titan.Allegiance,
                    Color = SKColor.Parse("#b455b6")
                });
            }
        }
        
            
        

        // Méthode factice pour récupérer une liste de Titans
        private List<Titan> GetTitansFromSomewhere()
        {
            // Implementez cette méthode pour récupérer vos données Titan
            return new List<Titan>();
        }

        public RepresentationViewModel()
        {
            LoadData();
            Entries = new ObservableCollection<ChartEntry>();


            Chart myChart = new RadarChart
            {
                Entries = Entries
            };

            MyObservableChart = myChart;
        }





    }
}
