namespace MyProject.View;

public partial class Representation : ContentPage
{
    RepresentationViewModel viewModel;
    public Representation(RepresentationViewModel viewModel)
    {
        this.viewModel = viewModel;
        InitializeComponent();
        BindingContext = viewModel;
    }
}