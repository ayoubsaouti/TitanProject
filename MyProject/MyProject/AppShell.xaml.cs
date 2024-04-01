namespace MyProject
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            //CREATION DE LA LIAISON DES DEUX PAGES 
            Routing.RegisterRoute(nameof(ScanElement), typeof(ScanElement)); 
            Routing.RegisterRoute(nameof(MyCollectionPage), typeof(MyCollectionPage));
            Routing.RegisterRoute(nameof(ConnectPage), typeof(ConnectPage));
            Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));

        }
    }
}
