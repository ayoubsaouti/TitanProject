using Azure.Identity;
using Microsoft.IdentityModel.Tokens;
using MyProject.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.ViewModel
{
    // ViewModel principal de l'application
    public partial class MainViewModel : BaseViewModel
    {
        // Propriété observée pour l'utilisateur connecté
        [ObservableProperty]
        private string userConnected;

        // Constructeur
        public MainViewModel()
        {
            // Initialise la chaîne de bienvenue avec le nom d'utilisateur global
            UserConnected = "Bienvenue  " + Globals.userConnected;
        }

        // Méthode pour aller à la page de numérisation
        [RelayCommand]
        private async Task GoToScanPage()
        {
            IsBusy = true;
            // Redirection vers une nouvelle page à partir du bouton
            await Shell.Current.GoToAsync("ScanElement", true);
            IsBusy = false;
        }

        // Méthode pour aller à la page de collection
        [RelayCommand]
        private async Task GoToCollectionPage()
        {
            IsBusy = true;
            // Redirection vers une nouvelle page à partir du bouton
            await Shell.Current.GoToAsync("MyCollectionPage", true);
            IsBusy = false;
        }

        // Méthode pour aller à la page de connexion
        [RelayCommand]
        private async Task GoToConnectPage()
        {
            IsBusy = true;
            // Redirection vers une nouvelle page à partir du bouton
            await Shell.Current.GoToAsync("ConnectPage", true);
            IsBusy = false;
        }

        // Méthode pour aller à la page de représentation graphique
        [RelayCommand]
        private async Task GoToChart()
        {
            IsBusy = true;
            // Redirection vers une nouvelle page à partir du bouton
            await Shell.Current.GoToAsync("RepresentationPage", true);
            IsBusy = false;
        }

        // Méthode pour charger des données à partir d'un fichier JSON
        [RelayCommand]
        private async Task LoadJSON()
        {
            IsBusy = true;
            JSONServices MyService = new();

            // Appel du service pour récupérer les titans à partir du fichier JSON
            await MyService.GetTitans();

            IsBusy = false;
        }

        // Méthode pour configurer le port COM sélectionné
        [RelayCommand]
        public async Task SetupPortCOM(string selectedPort)
        {
            IsBusy = true;
            try
            {
                // Configure le port COM sélectionné dans les paramètres globaux
                Globals.portConnected = selectedPort;
            }
            catch (Exception ex)
            {
                // Gère les éventuelles erreurs
                await Shell.Current.DisplayAlert("Erreur", ex.ToString(), "OK");
            }
            IsBusy = false;
        }
    }
}
