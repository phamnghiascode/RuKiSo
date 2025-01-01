using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using RuKiSo.Features.Models;
using RuKiSo.Features.Services;
using RuKiSo.Utils.MVVM;
using RuKiSo.ViewModels;
using RuKiSo.Views;
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
            builder.Services.AddSingleton(new ApiClientOptions
            {
                ApiBaseAddress = "https://localhost:7184"
            });

            builder.Services.AddSingleton<IGenericService<IngredientRespone, IngredientRequest>, IngredientService>();
            builder.Services.AddSingleton<IGenericService<ProductRespone, ProductRequest>, ProductService>();
            builder.Services.AddSingleton<IGenericService<TransactionResponse, TransactionRequest>, TransactionService>();
            builder.Services.AddSingleton<IGenericService<BatchResponse, BatchRequest>, BatchService>();

            builder.Services.AddTransient<ProductViewModel>();
            builder.Services.AddTransient<IngredientViewModel>();
            builder.Services.AddTransient<TransactionViewModel>();
            builder.Services.AddTransient<BatchViewModel>();
            builder.Services.AddTransient<DashBoardViewModel>();
            

            builder.Services.AddTransient<ProductPage>();
            builder.Services.AddTransient<IngredientPage>();
            builder.Services.AddTransient<TransactionPage>();
            builder.Services.AddTransient<BatchPage>();
            builder.Services.AddTransient<DashboardPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
