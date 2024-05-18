namespace MyProject.View;

public partial class RegisterPage : ContentPage
{
    RegisterViewModel viewModel;
    public RegisterPage(RegisterViewModel viewModel)
	{
        this.viewModel = viewModel;
		InitializeComponent();
        BindingContext = viewModel;
	}
}