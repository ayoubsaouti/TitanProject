using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyProject.Services
{
    // Service interne pour la gestion des opérations JSON
    internal class JSONServices
    {
        // Méthode pour lire les Titans à partir d'un fichier JSON sur le bureau
        public async Task GetTitans()
        {
            // Chemin du fichier JSON sur le bureau
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Titans.json");

            try
            {
                // Ouvrir le fichier en mode lecture
                using var stream = File.Open(filePath, FileMode.Open);
                using var reader = new StreamReader(stream);

                // Lire le contenu du fichier
                var contents = await reader.ReadToEndAsync();

                // Désérialiser le contenu en une liste de Titans
                Globals.myTitans = JsonSerializer.Deserialize<List<Titan>>(contents);

                // Afficher une alerte indiquant la réussite de l'importation
                await Shell.Current.DisplayAlert("Importation du JSON", "Importation réussie !", "OK");
            }
            catch (Exception ex)
            {
                // Afficher une alerte en cas d'erreur
                await Shell.Current.DisplayAlert("Pas de fichier JSON trouvé", ex.Message, "OK");
            }
        }

        // Méthode pour écrire les Titans dans un fichier JSON sur le bureau
        public async Task SetTitans()
        {
            // Chemin du fichier JSON sur le bureau
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Titans.json");

            try
            {
                // Créer le fichier (ou remplacer s'il existe)
                using FileStream fileStream = File.Create(filePath);

                // Sérialiser la liste des Titans dans le fichier
                await JsonSerializer.SerializeAsync(fileStream, Globals.myTitans);

                // Libérer les ressources du fileStream de manière asynchrone
                await fileStream.DisposeAsync();

                // Afficher une alerte indiquant la réussite de l'ajout
                await Shell.Current.DisplayAlert("Ajout du Titan", "Réussi !", "OK");
            }
            catch (Exception ex)
            {
                // Afficher une alerte en cas d'erreur d'écriture
                await Shell.Current.DisplayAlert("Erreur d'écriture JSON !", ex.Message, "OK");
            }
        }
    }
}
