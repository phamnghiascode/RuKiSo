using RuKiSo.Views;

namespace RuKiSo
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("Features/Products/Views/Products", typeof(ProductPage));
            Routing.RegisterRoute("Features/Products/Views/Batches", typeof(BatchePage));
            Routing.RegisterRoute("Features/Products/Views/Ingredients", typeof(IngredientPage));
        }
    }
}
