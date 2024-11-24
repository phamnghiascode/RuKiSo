using Microsoft.EntityFrameworkCore;
using RuKiSoBackEnd.Models.Domains;

namespace RuKiSoBackEnd.Data
{
    public partial class RuKiSoDataContext
    {
        private void SeedData(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<Ingredients>().HasData(
                new Ingredients { Id = 1, Name = "Gạo nếp", Unit = "Kg", Quantity = 300, PurchasePrice = 20000},
                new Ingredients { Id = 2, Name = "Nếp cái hoa vàng", Unit = "Kg", Quantity = 500, PurchasePrice = 25000 },
                new Ingredients { Id = 3, Name = "Nếp đen", Unit = "Kg", Quantity = 1000, PurchasePrice = 23000 },
                new Ingredients { Id = 4, Name = "Đòng đòng", Unit = "Kg", Quantity = 50, PurchasePrice = 30000 },
                new Ingredients { Id = 5, Name = "Men thuốc bắc", Unit = "Kg", Quantity = 3, PurchasePrice = 150000 },
                new Ingredients { Id = 6, Name = "Men thường", Unit = "Kg", Quantity = 1, PurchasePrice = 80000 },
                new Ingredients { Id = 7, Name = "Men lá", Unit = "Kg", Quantity = 2, PurchasePrice = 100000 },
                new Ingredients { Id = 8, Name = "Táo mèo", Unit = "Kg", Quantity = 10, PurchasePrice = 30000 }
                );
            modelBuilder.Entity<Products>().HasData(
                new Products { Id = 1, Name = "Rượu trắng 45", Description = "Nếp đen, men thuốc bắc", Quantity = 500, Price = 50000 },
                new Products { Id = 2, Name = "Rượu trắng 40", Description = "Nếp đen, men thuốc bắc", Quantity = 300, Price = 45000 },
                new Products { Id = 3, Name = "Rượu trắng 35", Description = "Nếp đen, men thuốc bắc", Quantity = 250, Price = 40000 },
                new Products { Id = 4, Name = "Đòng đòng 45", Description = "Nếp cái hoa vàng, men thuốc bắc", Quantity = 80, Price = 75000 },
                new Products { Id = 5, Name = "Đòng đòng 40", Description = "Nếp cái hoa vàng, men thuốc bắc", Quantity = 100, Price = 70000 },
                new Products { Id = 6, Name = "Đòng đòng 35", Description = "Nếp cái hoa vàng, men thuốc bắc", Quantity = 50, Price = 60000 },
                new Products { Id = 7, Name = "Rượu bách nhật", Description = "Gạo nếp, men lá", Quantity = 10, Price = 40000 },
                new Products { Id = 8, Name = "Rượu táo mèo", Description = "Nếp đen, táo mèo", Quantity = 20, Price = 60000 }
                );
            modelBuilder.Entity<Batches>().HasData(
                new Batches { Id = 1, StartDate = new DateTime(2024,1,1), EstimateEndDate = new DateTime(2024,1,30)}
                );
            modelBuilder.Entity<Transactions>().HasData();
        }

    }
}
