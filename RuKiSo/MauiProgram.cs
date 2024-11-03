using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

namespace RuKiSo
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiCommunityToolkit()
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("fa-brands-400.ttf", "FaBrands");
                    fonts.AddFont("fa-regular-400.ttf", "FaRegu");
                    fonts.AddFont("fa-solid-900.ttf", "FaSolid");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
