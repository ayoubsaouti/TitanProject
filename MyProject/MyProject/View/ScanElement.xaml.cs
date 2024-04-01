namespace MyProject.View;

public partial class ScanElement : ContentPage
{
    ScanElementViewModel viewModel;
    public ScanElement(ScanElementViewModel viewModel)
    {
        this.viewModel = viewModel;
        InitializeComponent();
        BindingContext = viewModel;
    }
}