using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyProject.ViewModel
{
    // ViewModel pour la page d'inscription
    public partial class RegisterViewModel : BaseViewModel
    {
        // Propriété observée pour le nom d'utilisateur
        [ObservableProperty]
        private string username;

        // Propriété observée pour le mot de passe
        [ObservableProperty]
        private string password;

        DataAccessService MyDBService;

        // Commande pour l'inscription
        public ICommand RegisterCommand { get; set; }

        // Constructeur
        public RegisterViewModel(DataAccessService MyDBService)
        {
            // Initialise le service de base de données
            this.MyDBService = MyDBService;
            // Initialise la commande d'inscription avec une action asynchrone pour enregistrer les utilisateurs dans la base de données
            RegisterCommand = new Command(async () => await SaveUsersInDB());
        }

        // Méthode asynchrone pour enregistrer les utilisateurs dans la base de données
        private async Task SaveUsersInDB()
        {
            IsBusy = true;

            // Vérifie si les champs sont vides
            if (Username.IsNullOrEmpty() || Password.IsNullOrEmpty())
            {
                await Shell.Current.DisplayAlert("Champs vide", "Un ou plusieurs n'ont pas été remplis", "OK");
            }
            else
            {
                // Crée un nouvel identifiant unique pour l'utilisateur
                Globals.idUserConected = Guid.NewGuid().ToString();
                // Crée un nouvel utilisateur
                User newUser = new User(Globals.idUserConected, Username, Password);

                try
                {
                    // Ajoute l'utilisateur à la base de données
                    await MyDBService.Users.AddAsync(newUser);
                    // Sauvegarde les modifications dans la base de données
                    await MyDBService.SaveChangesAsync();

                    await Shell.Current.DisplayAlert("Utilisateur ajouté", "Votre compte a bien été créé", "OK");
                }
                catch (Exception ex)
                {
                    await Shell.Current.DisplayAlert("Erreur", "Une erreur est survenue lors de l'enregistrement de votre compte dans la base de données", "OK");
                }

                // Retourne à la page précédente après l'inscription réussie
                await Shell.Current.GoToAsync("..", true);
            }

            IsBusy = false;
        }
    }
}
