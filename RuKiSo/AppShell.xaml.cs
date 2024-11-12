using RuKiSo.Views;

namespace RuKiSo
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(DashboardPage), typeof(DashboardPage));
            Routing.RegisterRoute(nameof(ProductPage), typeof(ProductPage));
            Routing.RegisterRoute(nameof(BatchPage), typeof(BatchPage));
            Routing.RegisterRoute(nameof(IngredientPage), typeof(IngredientPage));
            Routing.RegisterRoute(nameof(TransactionPage), typeof(TransactionPage));
        }
    }
}
