using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MyProject
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .UseMauiCommunityToolkitMediaElement()
                .UseMicrocharts()

                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif
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

            builder.Services.AddDbContext<DataAccessService>(e => e.UseSqlServer($"Server=(localdB)\\MSSQLLocalDB;Database=MyDatabase5;Trusted_Connection=True"));

            return builder.Build();

            
        }
    }
}
