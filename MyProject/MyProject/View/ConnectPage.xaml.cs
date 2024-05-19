namespace MyProject.View;

public partial class ConnectPage : ContentPage
{
    ConnectViewModel viewModel;

    public ConnectPage(ConnectViewModel viewModel)
	{
        this.viewModel = viewModel;
        InitializeComponent();
        BindingContext = viewModel;

    }
}