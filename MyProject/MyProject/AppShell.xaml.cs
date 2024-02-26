namespace MyProject
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            //CREATION DE LA LIAISON DES DEUX PAGES 
            Routing.RegisterRoute(nameof(NewPage), typeof(NewPage)); 
            Routing.RegisterRoute(nameof(MyCollectionPage), typeof(MyCollectionPage));
        }
    }
}
