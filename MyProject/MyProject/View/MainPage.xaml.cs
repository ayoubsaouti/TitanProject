namespace MyProject.View;

public partial class MainPage : ContentPage
{
    MainViewModel viewModel;
    public MainPage(MainViewModel viewModel)
    {
        this.viewModel = viewModel;
        InitializeComponent();
        BindingContext = viewModel;

        // Remplir la liste d�roulante avec les ports COM disponibles
        FillPortPicker();
    }

    // M�thode pour remplir la liste d�roulante avec les ports COM disponibles
    private void FillPortPicker()
    {
        // Code pour remplir la liste d�roulante (portPicker) avec les ports COM disponibles
        // ...
    }

    // M�thode appel�e lorsque l'utilisateur clique sur le bouton "Connect"
    private async void OnConnectClicked(object sender, EventArgs e)
    {
        string selectedPort = portPicker.SelectedItem?.ToString();
        if (!string.IsNullOrEmpty(selectedPort))
        {
            // Appeler la m�thode dans le ViewModel pour configurer le scanner avec le port s�lectionn�
            await viewModel.SetupPortCOM(selectedPort);
        }
        else
        {
            // Afficher un message d'erreur si aucun port n'a �t� s�lectionn�
            await DisplayAlert("Error", "Please select a COM port.", "OK");
        }
    }
}