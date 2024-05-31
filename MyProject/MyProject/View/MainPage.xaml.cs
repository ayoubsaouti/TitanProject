namespace MyProject.View;

public partial class MainPage : ContentPage
{
    MainViewModel viewModel;
    public MainPage(MainViewModel viewModel)
    {
        this.viewModel = viewModel;
        InitializeComponent();
        BindingContext = viewModel;

        // Remplir la liste déroulante avec les ports COM disponibles
        FillPortPicker();
    }

    // Méthode pour remplir la liste déroulante avec les ports COM disponibles
    private void FillPortPicker()
    {
        // Code pour remplir la liste déroulante (portPicker) avec les ports COM disponibles
        // ...
    }

    // Méthode appelée lorsque l'utilisateur clique sur le bouton "Connect"
    private async void OnConnectClicked(object sender, EventArgs e)
    {
        string selectedPort = portPicker.SelectedItem?.ToString();
        if (!string.IsNullOrEmpty(selectedPort))
        {
            // Appeler la méthode dans le ViewModel pour configurer le scanner avec le port sélectionné
            await viewModel.SetupPortCOM(selectedPort);
        }
        else
        {
            // Afficher un message d'erreur si aucun port n'a été sélectionné
            await DisplayAlert("Error", "Please select a COM port.", "OK");
        }
    }
}