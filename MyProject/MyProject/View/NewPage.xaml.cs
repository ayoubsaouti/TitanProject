namespace MyProject.View;

public partial class NewPage : ContentPage
{
    NewViewModel viewModel;
    public NewPage(NewViewModel viewModel)
    {
        this.viewModel = viewModel;
        InitializeComponent();
        BindingContext = viewModel;
    }
}