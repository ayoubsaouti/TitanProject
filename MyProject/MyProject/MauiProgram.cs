using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MyProject
{
    public static class MauiProgram
    {
        // Je crée une méthode statique pour créer une application Maui
        public static MauiApp CreateMauiApp()
        {
            // Je crée un builder pour l'application Maui
            var builder = MauiApp.CreateBuilder();

            // J'ajoute les configurations nécessaires à l'application Maui
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .UseMauiCommunityToolkitMediaElement()
                .UseMicrocharts()
                .ConfigureFonts(fonts =>
                {
                    // Je configure les polices de caractères
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            // J'ajoute un logger pour les messages de débogage en mode DEBUG
            builder.Logging.AddDebug();
#endif

            // J'ajoute les services nécessaires avec leur scope approprié
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<MainViewModel>();

            builder.Services.AddTransient<ScanElement>();
            builder.Services.AddTransient<ScanElementViewModel>();

            builder.Services.AddTransient<MyCollectionPage>();
            builder.Services.AddTransient<MyCollectionViewModel>();

            builder.Services.AddTransient<ConnectPage>();
            builder.Services.AddTransient<ConnectViewModel>();

            builder.Services.AddTransient<RegisterPage>();
            builder.Services.AddTransient<RegisterViewModel>();

            builder.Services.AddTransient<RepresentationPage>();
            builder.Services.AddTransient<RepresentationViewModel>();

            builder.Services.AddTransient<JSONServices>();

            // Je configure le service d'accès aux données avec Entity Framework Core
            builder.Services.AddDbContext<DataAccessService>(e => e.UseSqlServer($"Server=(localdB)\\MSSQLLocalDB;Database=MyDatabase5;Trusted_Connection=True"));

            // Je retourne l'application construite
            return builder.Build();
        }
    }
}
