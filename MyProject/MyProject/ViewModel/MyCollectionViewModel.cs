using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SkiaSharp;

namespace MyProject.ViewModel
{
    // ViewModel pour la page de la collection de titans
    public partial class MyCollectionViewModel : BaseViewModel
    {
        // Collection observable des titans
        public ObservableCollection<Titan> myObservableTitans { get; } = new();

        // Service d'exportation CSV
        private CSVExportServices csvExportServices;

        // Liste des attributs sélectionnés
        private List<string> selectedAttributes;

        DataAccessService MyDBService;

        // Constructeur
        public MyCollectionViewModel(DataAccessService MyDBService)
        {
            // Initialise le service de base de données
            this.MyDBService = MyDBService;
            // Affiche les titans depuis la base de données
            afficheFromDb();
            // Initialise le service d'exportation CSV
            csvExportServices = new CSVExportServices();
            // Initialise la liste des attributs sélectionnés
            selectedAttributes = new List<string>();
        }

        // Méthode asynchrone pour afficher les titans depuis la base de données
        public async Task afficheFromDb()
        {
            IsBusy = true;
            try
            {
                // Récupère les titans de l'utilisateur actuellement connecté depuis la base de données
                var titans = await MyDBService.Titans.Where(e => e.IdUser == Globals.idUserConected).ToListAsync();
                // Ajoute les titans à la collection observable
                foreach (var titan in titans)
                {
                    myObservableTitans.Add(titan);
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Erreur", "Une erreur est survenue lors de la tentative de connexion", "OK");
            }
            IsBusy = false;
        }

        // Méthode pour exporter les titans vers un fichier CSV
        [RelayCommand]
        private async Task ExportToCSV()
        {
            // Vérifie si des attributs ont été sélectionnés, sinon utilise les attributs par défaut
            if (selectedAttributes.Count == 0)
            {
                string[] defaultAttributes = { "Name", "Height", "Abilities", "Current_inheritor", "Former_inheritor", "Allegiance" };
                selectedAttributes.AddRange(defaultAttributes);
            }

            IsBusy = true;
            try
            {
                // Sélectionne le dossier pour l'exportation CSV
                var result = await FilePicker.PickAsync(new PickOptions
                {
                    PickerTitle = "Sélectionnez un dossier",
                });

                if (result != null)
                {
                    string folderPath = result.FullPath;

                    // Exporte les titans vers un fichier CSV
                    csvExportServices.ExportToCSV(myObservableTitans, folderPath, selectedAttributes);
                    await Shell.Current.DisplayAlert("Export en fichier CSV", "Exportation réussie !", "OK");
                }
                else
                {
                    await Shell.Current.DisplayAlert("Erreur", "Aucun dossier sélectionné", "OK");
                }
            }
            catch (Exception e)
            {
                await Shell.Current.DisplayAlert("Erreur", "Échec de l'exportation CSV : " + e.Message, "OK");
            }
            IsBusy = false;
            // Efface la liste des attributs sélectionnés et décoche toutes les cases de sélection
            selectedAttributes.Clear();
            UncheckAll();
        }

        // Méthode pour décocher toutes les cases de sélection des attributs
        private void UncheckAll()
        {
            IsNameChecked = false;
            IsAbilitiesChecked = false;
            IsCurrentInheritorChecked = false;
            IsHeightChecked = false;
            IsFormerInheritorChecked = false;
            IsAllegianceChecked = false;
        }

        // Propriétés observées pour les cases de sélection des attributs
        [ObservableProperty]
        private bool isNameChecked;

        partial void OnIsNameCheckedChanged(bool value)
        {
            if (value)
            {
                selectedAttributes.Add("Name");
            }
            else
            {
                selectedAttributes.Remove("Name");
            }
        }

        [ObservableProperty]
        private bool isAbilitiesChecked;

        partial void OnIsAbilitiesCheckedChanged(bool value)
        {
            if (value)
            {
                selectedAttributes.Add("Abilities");
            }
            else
            {
                selectedAttributes.Remove("Abilities");
            }
        }

        [ObservableProperty]
        private bool isCurrentInheritorChecked;

        partial void OnIsCurrentInheritorCheckedChanged(bool value)
        {
            if (value)
            {
                selectedAttributes.Add("Current_inheritor");
            }
            else
            {
                selectedAttributes.Remove("Current_inheritor");
            }
        }

        [ObservableProperty]
        private bool isHeightChecked;

        partial void OnIsHeightCheckedChanged(bool value)
        {
            if (value)
            {
                selectedAttributes.Add("Height");
            }
            else
            {
                selectedAttributes.Remove("Height");
            }
        }

        [ObservableProperty]
        private bool isFormerInheritorChecked;

        partial void OnIsFormerInheritorCheckedChanged(bool value)
        {
            if (value)
            {
                selectedAttributes.Add("Former_inheritor");
            }
            else
            {
                selectedAttributes.Remove("Former_inheritor");
            }
        }

        [ObservableProperty]
        private bool isAllegianceChecked;

        partial void OnIsAllegianceCheckedChanged(bool value)
        {
            if (value)
            {
                selectedAttributes.Add("Allegiance");
            }
            else
            {
                selectedAttributes.Remove("Allegiance");
            }
        }
    }
}
