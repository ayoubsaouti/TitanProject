namespace MyProject.View;

public partial class RepresentationPage : ContentPage
{
    RepresentationViewModel viewModel;
    public RepresentationPage(RepresentationViewModel viewModel)
    {
        this.viewModel = viewModel;
        InitializeComponent();
        BindingContext = viewModel;
    }
}