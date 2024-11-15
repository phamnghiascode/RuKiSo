using CommunityToolkit.Maui;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RuKiSo.DataAccess;
using Syncfusion.Maui.Core.Hosting;

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
                .ConfigureSyncfusionCore()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("fa-brands-400.ttf", "FaBrands");
                    fonts.AddFont("fa-regular-400.ttf", "FaRegu");
                    fonts.AddFont("fa-solid-900.ttf", "FaSolid");
                });

            builder.Services.AddDbContext<RuKiSoDataContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("MyDb")));

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
