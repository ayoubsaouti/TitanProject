using MyProject.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyProject.ViewModel
{
    // ViewModel pour la numérisation des éléments
    public partial class ScanElementViewModel : BaseViewModel
    {
        // Collection observable des titans
        public ObservableCollection<Titan> myObservableTitans { get; } = new();
        // Liste de tous les titans
        public List<Titan> allOfMyTitans = new List<Titan>();
        // Indicateur pour vérifier si le titan est déjà possédé
        public Boolean dejaPosseder = false;

        // Service de numérisation
        [ObservableProperty]
        DeviceOrientationService myScanner;
        // Chemin de l'image sélectionnée
        private String selectedImageSource;

        // Code-barres scanné
        [ObservableProperty]
        string? codeBar;

        // Propriétés des champs du titan
        [ObservableProperty]
        private String id;
        [ObservableProperty]
        private String name;
        [ObservableProperty]
        private String height;
        [ObservableProperty]
        private String abilities;
        [ObservableProperty]
        private String current_inheritor;
        [ObservableProperty]
        private String former_inheritor;
        [ObservableProperty]
        private String allegiance;

        DataAccessService MyDBService;

        // Propriété pour le chemin de l'image sélectionnée
        public string SelectedImageSource
        {
            get => selectedImageSource;
            set
            {
                selectedImageSource = value;
                OnPropertyChanged(nameof(SelectedImageSource));
            }
        }

        // Commande pour sélectionner une image
        public ICommand SelectImageCommand { get; }
        // Commande pour ajouter un titan
        public ICommand AddTitanCommand { get; set; }

        // Constructeur
        public ScanElementViewModel(DataAccessService MyDBService)
        {
            this.MyDBService = MyDBService;
            // Initialise la commande pour sélectionner une image
            SelectImageCommand = new Command(async () => await SelectImage());
            // Initialise la commande pour ajouter un titan
            AddTitanCommand = new Command(async () => await AddElement());

            try
            {
                // Initialise le scanner
                this.myScanner = new();
                myScanner.ConfigureScanner();
                myScanner.SerialBuffer.Changed += OnBarCodeScanned;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        // Méthode déclenchée lorsqu'un code-barres est scanné
        private void OnBarCodeScanned(object sender, EventArgs arg)
        {
            DeviceOrientationService.QueueBuffer MyLocalBuffer = (DeviceOrientationService.QueueBuffer)sender;
            CodeBar = MyLocalBuffer.Dequeue().ToString();
        }

        // Méthode pour sélectionner une image
        private async Task SelectImage()
        {
            try
            {
                var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
                {
                    Title = "Choisir une image"
                });

                if (result != null)
                {
                    SelectedImageSource = result.FileName;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        // Méthode pour ajouter un élément
        private async Task AddElement()
        {
            // Vérifie si tous les champs nécessaires sont remplis
            if (id != null && name != null && selectedImageSource != null && height != null && abilities != null
                && current_inheritor != null && former_inheritor != null && allegiance != null)
            {
                IsBusy = true;
                JSONServices MyService = new();

                // Crée un nouveau titan avec les données fournies
                Titan newTitan = new Titan(id, name, selectedImageSource, height, abilities, current_inheritor, former_inheritor, allegiance, Globals.idUserConected);

                // Vérifie si le titan est déjà possédé
                foreach (var mytitan in Globals.myTitans)
                {
                    if (mytitan.IdTitan.Equals(newTitan.IdTitan))
                    {
                        dejaPosseder = true;
                    }
                }
                if (dejaPosseder == true)
                {
                    await Shell.Current.DisplayAlert("Titan déjà possédé", "L'ID existe déjà dans la collection", "OK");
                    dejaPosseder = false;
                }
                else
                {
                    // Ajoute le nouveau titan à la collection
                    Globals.myTitans.Add(newTitan);
                    myObservableTitans.Add(newTitan);
                    await MyService.SetTitans();
                    // Ajoute à la base de données
                    try
                    {
                        await MyDBService.Titans.AddAsync(newTitan);
                        await MyDBService.SaveChangesAsync();

                    }
                    catch (Exception ex)
                    {
                        await Shell.Current.DisplayAlert("Erreur", "Une erreur s'est produite lors de l'ajout du titan à la base de données", "OK");
                    }

                }
                IsBusy = false;
            }
            else
            {
                await Shell.Current.DisplayAlert("Erreur", "Un ou plusieurs champ(s) vide(s)", "OK");

            }

        }
    }
}
