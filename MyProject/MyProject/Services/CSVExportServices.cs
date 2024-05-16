using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Services
{

    internal class CSVExportServices
    {
        public void ExportToCSV(ObservableCollection<Titan> titans, string filePath, List<string> attributes)
        {
            // Créer le contenu CSV
            StringBuilder csvContent = new StringBuilder();

            // Ajouter l'en-tête avec les noms des Titans
            csvContent.Append("\t");
            foreach (var titan in titans)
            {
                csvContent.Append($"; {titan.Name}");
            }
            csvContent.AppendLine(";");


            // Ajouter l'en-tête avec les attributs des Titans
            foreach (var attribute in attributes)
            {
                csvContent.Append($"{attribute}");
                foreach (var titan in titans)
                {
                    string value = GetValueForAttribute(titan, attribute);
                    csvContent.Append($"; {value}");
                }
                csvContent.AppendLine(";");
            }


            // Écrire le contenu dans le fichier CSV
            File.WriteAllText(filePath, csvContent.ToString(), Encoding.UTF8);
        }
        private string GetValueForAttribute(Titan titan, string attribute)
        {
            switch (attribute)
            {
                case "Name":
                    return titan.Name;
                case "Picture":
                    return titan.Picture;
                case "Height":
                    return titan.Height;
                case "Abilities":
                    return titan.Abilities;
                case "Current_inheritor":
                    return titan.Current_inheritor;
                case "Former_inheritor":
                    return titan.Former_inheritor;
                case "Allegiance":
                    return titan.Allegiance;
                default:
                    return string.Empty;
            }
        }
    }
}
