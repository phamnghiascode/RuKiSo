using CommunityToolkit.Maui;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RuKiSo.Features.Services;
using Syncfusion.Maui.Core.Hosting;
using RuKiSo.Utils.MVVM;
using RuKiSo.ViewModels;
using RuKiSo.Views;

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
            //var config = new ConfigurationBuilder()
            //                .SetBasePath(FileSystem.AppDataDirectory)
            //                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            //                .Build();
            //var apiClientOptions = config.GetSection("ApiClientOptions").Get<ApiClientOptions>();
            builder.Services.AddSingleton(new ApiClientOptions
            {
                ApiBaseAddress = "https://localhost:7184"
            });
            builder.Services.AddSingleton<IProductService, ProductService>();
            builder.Services.AddTransient<ProductViewModel>();
            builder.Services.AddTransient<ProductPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
