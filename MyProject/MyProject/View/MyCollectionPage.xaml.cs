namespace MyProject.View;

public partial class MyCollectionPage : ContentPage
{
    MyCollectionViewModel viewModel;
    public MyCollectionPage(MyCollectionViewModel viewModel)
    {
        this.viewModel = viewModel;
        InitializeComponent();
        BindingContext = viewModel;
    }
}