using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyProject.ViewModel
{
    // ViewModel pour la page de connexion
    public partial class ConnectViewModel : BaseViewModel
    {
        // Propriété observée pour le nom d'utilisateur
        [ObservableProperty]
        private string username;

        // Propriété observée pour le mot de passe
        [ObservableProperty]
        private string password;

        // Propriété observée pour l'utilisateur connecté
        [ObservableProperty]
        private string userConnected;

        DataAccessService MyDBService;

        // Commande pour la connexion
        public ICommand LoginCommand { get; set; }

        // Constructeur
        public ConnectViewModel(DataAccessService MyDBService) 
        {
            // Initialise la chaîne affichée pour l'utilisateur connecté
            UserConnected = "Connecté en tant que : " + Globals.userConnected;
            this.MyDBService = MyDBService;

            // Initialise la commande de connexion avec une action asynchrone pour vérifier les utilisateurs dans la base de données
            LoginCommand = new Command(async () => await LoginUser());
        }

        // Méthode asynchrone pour la connexion de l'utilisateur
        private async Task LoginUser()
        {
            IsBusy = true;

            // Vérifie si les champs sont vides
            if (Username.IsNullOrEmpty() || Password.IsNullOrEmpty())
            {
                await Shell.Current.DisplayAlert("Champs vide", "Un ou plusieurs champs n'ont pas été remplis", "OK");
            }
            else
            {
                try
                {
                    // Recherche de l'utilisateur dans la base de données
                    var user = await MyDBService.Users.FirstOrDefaultAsync(u => u.Username == Username && u.Password == Password);

                    if (user != null)
                    {
                        // Met à jour l'utilisateur connecté et son identifiant global
                        Globals.userConnected = Username;
                        Globals.idUserConected = user.IdUser;
                        await Shell.Current.DisplayAlert("Connexion réussie", "Vous êtes connecté(e)", "OK");
                        await Shell.Current.GoToAsync("..", true);
                    }
                    else
                    {
                        await Shell.Current.DisplayAlert("Erreur de connexion", "Nom d'utilisateur ou mot de passe incorrect", "OK");
                    }
                }
                catch (Exception ex)
                {
                    await Shell.Current.DisplayAlert("Erreur", "Une erreur est survenue lors de la tentative de connexion", "OK");
                }
            }

            IsBusy = false;
        }

        // Méthode pour aller à la page d'inscription
        [RelayCommand]
        private async Task GoToRegisterPage()
        {
            IsBusy = true;
            // Redirection vers une nouvelle page à partir du bouton
            await Shell.Current.GoToAsync("RegisterPage", true);
            IsBusy = false;
        }

        // Méthode pour obtenir le nom d'utilisateur
        public string GetUsername()
        {
            return Username;
        }

        // Méthode pour obtenir le mot de passe
        public string GetPassword()
        {
            return Password;
        }
    }
}
