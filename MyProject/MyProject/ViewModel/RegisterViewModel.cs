using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyProject.ViewModel;

public partial class RegisterViewModel : BaseViewModel
{

    [ObservableProperty]
    private string username;
    [ObservableProperty]
    private string password;

    DataAccessService MyDBService;

    /// Commande pour l'inscription.
    public ICommand RegisterCommand { get; set; }
    public RegisterViewModel(DataAccessService MyDBService) {
        this.MyDBService = MyDBService;
        // Initialise la commande d'inscription avec une action asynchrone pour enregistrer les utilisateurs dans la base de données.
        RegisterCommand = new Command(async () => await SaveUsersInDB());
    }


    

    
    private async Task SaveUsersInDB()
    {
        IsBusy = true;

        //verifie si les champs sont vide
        if (Username.IsNullOrEmpty() || Password.IsNullOrEmpty())
        {
            await Shell.Current.DisplayAlert("Champs vide", "Un ou plusieurs n'ont pas été remplis", "OK");
        }
        else
        {
            //c'est pas vide donc on créer l'utilisateur et Guid.NewGuid().ToString() pour créer un id unique
            User newUser = new User(Guid.NewGuid().ToString(), Username, Password);

            try
            {
                //Ajout de l'utilisateur a la db
                await MyDBService.Users.AddAsync(newUser);
                //Save la db
                await MyDBService.SaveChangesAsync();

                await Shell.Current.DisplayAlert("Utilisateur ajouté", "Votre compte a bien été crée", "OK");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Erreur", "Une erreur est survenu lors de l'enregistrement de votre compte à la base de donnée", "OK");
            }

            await Shell.Current.GoToAsync("...", true);
        }

        IsBusy = false;

    }

  
}