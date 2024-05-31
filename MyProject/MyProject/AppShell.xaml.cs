namespace MyProject
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // Je crée les liaisons entre les pages
            Routing.RegisterRoute(nameof(ConnectPage), typeof(ConnectPage));
            Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(ScanElement), typeof(ScanElement));
            Routing.RegisterRoute(nameof(MyCollectionPage), typeof(MyCollectionPage));
            Routing.RegisterRoute(nameof(RepresentationPage), typeof(RepresentationPage));
        }
    }
}
