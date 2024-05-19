using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyProject.ViewModel;

public partial class ConnectViewModel : BaseViewModel
{

    [ObservableProperty]
    private string username;
    [ObservableProperty]
    private string password;
    [ObservableProperty]
    private string userConnected;

    DataAccessService MyDBService;


    /// Commande pour la connexion.
    public ICommand LoginCommand { get; set; }

    public ConnectViewModel(DataAccessService MyDBService) {
        UserConnected = Globals.userConnected;
        this.MyDBService = MyDBService;
        // Initialise la commande de connexion avec une action asynchrone pour vérifier les utilisateurs dans la base de données.
        LoginCommand = new Command(async () => await LoginUser());

    }

    

    private async Task LoginUser()
    {
        if (Globals.userConnected.IsNullOrEmpty())
        {
            await Shell.Current.GoToAsync("..", true); 
        }
        else
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
    }



    [RelayCommand]
    private async Task GoToRegisterPage()
    {
        IsBusy = true;
        //REDIRECTION VERS UNE NOUVELLE PAGE A PARTIR DU BUTTON
        await Shell.Current.GoToAsync("RegisterPage", true);
        IsBusy = false;
    }

    public string getUsername()
    {
        return Username;
    }
    public string getPassword()
    {
        return Password;
    }
}