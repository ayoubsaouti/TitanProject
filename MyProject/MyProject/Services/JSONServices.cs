using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyProject.Services
{
    internal class JSONServices
    {
        public async Task GetTitans()
        {

            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Titans.json");

            try
            {
                using var stream = File.Open(filePath, FileMode.Open);

                using var reader = new StreamReader(stream);
                var contents = await reader.ReadToEndAsync();
                Globals.myTitans = JsonSerializer.Deserialize<List<Titan>>(contents);
                await Shell.Current.DisplayAlert("Importation du JSON", "importation reussis !", "OK");
            }
            catch (Exception ex)
            {

                await Shell.Current.DisplayAlert("Pas de fichier JSON trouvé", ex.Message, "OK");
            }

        }

        public async Task SetTitans()
        {

            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Titans.json");

            try
            {
                using FileStream fileStream = File.Create(filePath);

                await JsonSerializer.SerializeAsync(fileStream, Globals.myTitans);
                await fileStream.DisposeAsync();
                await Shell.Current.DisplayAlert("Ajout du Titan", " reussis !", "OK");
            }
            catch (Exception ex)
            {

                await Shell.Current.DisplayAlert("JSON Write Error!", ex.Message, "OK");
            }

        }
    }
}
