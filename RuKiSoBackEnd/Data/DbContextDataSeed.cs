using Microsoft.EntityFrameworkCore;
using RuKiSoBackEnd.Models.Domains;

namespace RuKiSoBackEnd.Data
{
    public partial class RuKiSoDataContext
    {
        private void SeedData(ModelBuilder modelBuilder)
        {
            // Seed Products
            modelBuilder.Entity<Products>().HasData(
                new Products { Id = 1, Name = "Rượu trắng 45", Description = "Nếp đen, men thuốc bắc", Quantity = 500, Price = 50000 },
                new Products { Id = 2, Name = "Rượu trắng 40", Description = "Nếp cái hoa vàng, men thường", Quantity = 300, Price = 45000 },
                new Products { Id = 3, Name = "Rượu trắng 35", Description = "Gạo nếp, men lá", Quantity = 250, Price = 40000 },
                new Products { Id = 4, Name = "Đòng đòng 45", Description = "Đòng đòng, men thuốc bắc", Quantity = 80, Price = 75000 },
                new Products { Id = 5, Name = "Đòng đòng 40", Description = "Đòng đòng, men thường", Quantity = 100, Price = 70000 },
                new Products { Id = 6, Name = "Đòng đòng 35", Description = "Đòng đòng, men lá", Quantity = 50, Price = 60000 },
                new Products { Id = 7, Name = "Rượu bách nhật", Description = "Nếp cái hoa vàng, men lá", Quantity = 10, Price = 40000 },
                new Products { Id = 8, Name = "Rượu táo mèo", Description = "Nếp đen, táo mèo, men thuốc bắc", Quantity = 20, Price = 60000 }
            );

            // Seed Ingredients
            modelBuilder.Entity<Ingredients>().HasData(
                new Ingredients { Id = 1, Name = "Gạo nếp", Unit = "Kg", Quantity = 300, PurchasePrice = 20000 },
                new Ingredients { Id = 2, Name = "Nếp cái hoa vàng", Unit = "Kg", Quantity = 500, PurchasePrice = 25000 },
                new Ingredients { Id = 3, Name = "Nếp đen", Unit = "Kg", Quantity = 1000, PurchasePrice = 23000 },
                new Ingredients { Id = 4, Name = "Đòng đòng", Unit = "Kg", Quantity = 50, PurchasePrice = 30000 },
                new Ingredients { Id = 5, Name = "Men thuốc bắc", Unit = "Kg", Quantity = 3, PurchasePrice = 150000 },
                new Ingredients { Id = 6, Name = "Men thường", Unit = "Kg", Quantity = 1, PurchasePrice = 80000 },
                new Ingredients { Id = 7, Name = "Men lá", Unit = "Kg", Quantity = 2, PurchasePrice = 100000 },
                new Ingredients { Id = 8, Name = "Táo mèo", Unit = "Kg", Quantity = 10, PurchasePrice = 30000 }
            );

            // Seed Batches
            modelBuilder.Entity<Batches>().HasData(
                new { Id = 1, StartDate = new DateTime(2024, 1, 1), EstimateEndDate = new DateTime(2024, 3, 1), Yield = 0, ProductId = (int?)1 },
                new { Id = 2, StartDate = new DateTime(2024, 1, 15), EstimateEndDate = new DateTime(2024, 3, 15), Yield = 0, ProductId = (int?)2 },
                new { Id = 3, StartDate = new DateTime(2024, 2, 1), EstimateEndDate = new DateTime(2024, 4, 1), Yield = 0, ProductId = (int?)3 },
                new { Id = 4, StartDate = new DateTime(2024, 2, 15), EstimateEndDate = new DateTime(2024, 4, 15), Yield = 0, ProductId = (int?)4 },
                new { Id = 5, StartDate = new DateTime(2024, 3, 1), EstimateEndDate = new DateTime(2024, 5, 1), Yield = 0, ProductId = (int?)5 },
                new { Id = 6, StartDate = new DateTime(2024, 3, 15), EstimateEndDate = new DateTime(2024, 5, 15), Yield = 0, ProductId = (int?)6 },
                new { Id = 7, StartDate = new DateTime(2024, 4, 1), EstimateEndDate = new DateTime(2024, 6, 1), Yield = 0, ProductId = (int?)7 },
                new { Id = 8, StartDate = new DateTime(2024, 4, 15), EstimateEndDate = new DateTime(2024, 6, 15), Yield = 0, ProductId = (int?)8 }
            );

            // Seed BatchIngredients
            var batchIngredients = new[]
            {
                new { BatchId = 1, IngredientId = 3, Quantity = 50 },
                new { BatchId = 1, IngredientId = 5, Quantity = 1 },
                new { BatchId = 2, IngredientId = 2, Quantity = 45 },
                new { BatchId = 2, IngredientId = 6, Quantity = 1 },
                new { BatchId = 3, IngredientId = 1, Quantity = 40 },
                new { BatchId = 3, IngredientId = 7, Quantity = 1 },
                new { BatchId = 4, IngredientId = 4, Quantity = 30 },
                new { BatchId = 4, IngredientId = 5, Quantity = 1 },
                new { BatchId = 5, IngredientId = 4, Quantity = 25 },
                new { BatchId = 5, IngredientId = 6, Quantity = 1 },
                new { BatchId = 6, IngredientId = 4, Quantity = 20 },
                new { BatchId = 6, IngredientId = 7, Quantity = 1 },
                new { BatchId = 7, IngredientId = 2, Quantity = 35 },
                new { BatchId = 7, IngredientId = 7, Quantity = 1 },
                new { BatchId = 8, IngredientId = 3, Quantity = 40 },
                new { BatchId = 8, IngredientId = 8, Quantity = 2 },
                new { BatchId = 8, IngredientId = 5, Quantity = 1 }
            };

            modelBuilder.Entity<BatchIngredient>().HasData(batchIngredients);

            modelBuilder.Entity<Transactions>().HasData(
                // Tháng 3/2024
                new Transactions { Id = 1, Name = "Mua nếp đen", TranType = false, Quantity = 200, Value = 4600000, TranDate = new DateTime(2024, 3, 5), IngredientId = 3 },
                new Transactions { Id = 2, Name = "Mua men thuốc bắc", TranType = false, Quantity = 2, Value = 300000, TranDate = new DateTime(2024, 3, 5), IngredientId = 5 },
                new Transactions { Id = 3, Name = "Bán rượu trắng 45", TranType = true, Quantity = 100, Value = 5000000, TranDate = new DateTime(2024, 3, 20), ProductId = 1 },
                new Transactions { Id = 4, Name = "Bán đòng đòng 45", TranType = true, Quantity = 30, Value = 2250000, TranDate = new DateTime(2024, 3, 25), ProductId = 4 },

                // Tháng 4/2024
                new Transactions { Id = 5, Name = "Mua nếp cái hoa vàng", TranType = false, Quantity = 150, Value = 3750000, TranDate = new DateTime(2024, 4, 3), IngredientId = 2 },
                new Transactions { Id = 6, Name = "Mua đòng đòng", TranType = false, Quantity = 50, Value = 1500000, TranDate = new DateTime(2024, 4, 3), IngredientId = 4 },
                new Transactions { Id = 7, Name = "Bán rượu trắng 40", TranType = true, Quantity = 120, Value = 5400000, TranDate = new DateTime(2024, 4, 15), ProductId = 2 },
                new Transactions { Id = 8, Name = "Bán đòng đòng 40", TranType = true, Quantity = 40, Value = 2800000, TranDate = new DateTime(2024, 4, 28), ProductId = 5 },

                // Tháng 5/2024
                new Transactions { Id = 9, Name = "Mua gạo nếp", TranType = false, Quantity = 180, Value = 3600000, TranDate = new DateTime(2024, 5, 5), IngredientId = 1 },
                new Transactions { Id = 10, Name = "Mua men lá", TranType = false, Quantity = 2, Value = 200000, TranDate = new DateTime(2024, 5, 5), IngredientId = 7 },
                new Transactions { Id = 11, Name = "Bán rượu trắng 35", TranType = true, Quantity = 150, Value = 6000000, TranDate = new DateTime(2024, 5, 20), ProductId = 3 },
                new Transactions { Id = 12, Name = "Bán đòng đòng 35", TranType = true, Quantity = 35, Value = 2100000, TranDate = new DateTime(2024, 5, 25), ProductId = 6 },

                // Tháng 6/2024
                new Transactions { Id = 13, Name = "Mua nếp đen", TranType = false, Quantity = 220, Value = 5060000, TranDate = new DateTime(2024, 6, 2), IngredientId = 3 },
                new Transactions { Id = 14, Name = "Mua táo mèo", TranType = false, Quantity = 10, Value = 300000, TranDate = new DateTime(2024, 6, 2), IngredientId = 8 },
                new Transactions { Id = 15, Name = "Bán rượu trắng 45", TranType = true, Quantity = 130, Value = 6500000, TranDate = new DateTime(2024, 6, 18), ProductId = 1 },
                new Transactions { Id = 16, Name = "Bán rượu táo mèo", TranType = true, Quantity = 40, Value = 2400000, TranDate = new DateTime(2024, 6, 28), ProductId = 8 },

                // Tháng 7/2024
                new Transactions { Id = 17, Name = "Mua nếp cái hoa vàng", TranType = false, Quantity = 160, Value = 4000000, TranDate = new DateTime(2024, 7, 4), IngredientId = 2 },
                new Transactions { Id = 18, Name = "Mua men thuốc bắc", TranType = false, Quantity = 2, Value = 300000, TranDate = new DateTime(2024, 7, 4), IngredientId = 5 },
                new Transactions { Id = 19, Name = "Bán rượu trắng 40", TranType = true, Quantity = 140, Value = 6300000, TranDate = new DateTime(2024, 7, 15), ProductId = 2 },
                new Transactions { Id = 20, Name = "Bán bách nhật", TranType = true, Quantity = 45, Value = 1800000, TranDate = new DateTime(2024, 7, 25), ProductId = 7 },

                // Tháng 8/2024
                new Transactions { Id = 21, Name = "Mua đòng đòng", TranType = false, Quantity = 100, Value = 3000000, TranDate = new DateTime(2024, 8, 3), IngredientId = 4 },
                new Transactions { Id = 22, Name = "Mua men lá", TranType = false, Quantity = 3, Value = 300000, TranDate = new DateTime(2024, 8, 3), IngredientId = 7 },
                new Transactions { Id = 23, Name = "Bán đòng đòng 45", TranType = true, Quantity = 50, Value = 3750000, TranDate = new DateTime(2024, 8, 18), ProductId = 4 },
                new Transactions { Id = 24, Name = "Bán đòng đòng 40", TranType = true, Quantity = 60, Value = 4200000, TranDate = new DateTime(2024, 8, 28), ProductId = 5 },

                // Tháng 9/2024
                new Transactions { Id = 25, Name = "Mua nếp đen", TranType = false, Quantity = 180, Value = 4140000, TranDate = new DateTime(2024, 9, 5), IngredientId = 3 },
                new Transactions { Id = 26, Name = "Mua men thuốc bắc", TranType = false, Quantity = 2, Value = 300000, TranDate = new DateTime(2024, 9, 5), IngredientId = 5 },
                new Transactions { Id = 27, Name = "Bán rượu trắng 45", TranType = true, Quantity = 110, Value = 5500000, TranDate = new DateTime(2024, 9, 15), ProductId = 1 },
                new Transactions { Id = 28, Name = "Bán rượu táo mèo", TranType = true, Quantity = 50, Value = 3000000, TranDate = new DateTime(2024, 9, 25), ProductId = 8 },

                // Tháng 10/2024
                new Transactions { Id = 29, Name = "Mua nếp cái hoa vàng", TranType = false, Quantity = 170, Value = 4250000, TranDate = new DateTime(2024, 10, 4), IngredientId = 2 },
                new Transactions { Id = 30, Name = "Mua táo mèo", TranType = false, Quantity = 15, Value = 450000, TranDate = new DateTime(2024, 10, 4), IngredientId = 8 },
                new Transactions { Id = 31, Name = "Bán rượu trắng 40", TranType = true, Quantity = 130, Value = 5850000, TranDate = new DateTime(2024, 10, 18), ProductId = 2 },
                new Transactions { Id = 32, Name = "Bán bách nhật", TranType = true, Quantity = 40, Value = 1600000, TranDate = new DateTime(2024, 10, 28), ProductId = 7 },

                // Tháng 11/2024
                new Transactions { Id = 33, Name = "Mua gạo nếp", TranType = false, Quantity = 200, Value = 4000000, TranDate = new DateTime(2024, 11, 3), IngredientId = 1 },
                new Transactions { Id = 34, Name = "Mua men lá", TranType = false, Quantity = 2, Value = 200000, TranDate = new DateTime(2024, 11, 3), IngredientId = 7 },
                new Transactions { Id = 35, Name = "Bán rượu trắng 35", TranType = true, Quantity = 140, Value = 5600000, TranDate = new DateTime(2024, 11, 15), ProductId = 3 },
                new Transactions { Id = 36, Name = "Bán đòng đòng 35", TranType = true, Quantity = 45, Value = 2700000, TranDate = new DateTime(2024, 11, 25), ProductId = 6 },

                // Tháng 12/2024
                new Transactions { Id = 37, Name = "Mua đòng đòng", TranType = false, Quantity = 80, Value = 2400000, TranDate = new DateTime(2024, 12, 5), IngredientId = 4 },
                new Transactions { Id = 38, Name = "Mua men thuốc bắc", TranType = false, Quantity = 3, Value = 450000, TranDate = new DateTime(2024, 12, 5), IngredientId = 5 },
                new Transactions { Id = 39, Name = "Bán đòng đòng 45", TranType = true, Quantity = 45, Value = 3375000, TranDate = new DateTime(2024, 12, 20), ProductId = 4 },
                new Transactions { Id = 40, Name = "Bán rượu trắng 45", TranType = true, Quantity = 120, Value = 6000000, TranDate = new DateTime(2024, 12, 28), ProductId = 1 },

                new Transactions { Id = 41, Name = "Bán rượu trắng 40", TranType = true, Quantity = 50, Value = 2250000, TranDate = DateTime.Now.AddDays(-6), ProductId = 2 },
                new Transactions { Id = 42, Name = "Mua men thuốc bắc", TranType = false, Quantity = 1, Value = 150000, TranDate = DateTime.Now.AddDays(-5), IngredientId = 5 },
                new Transactions { Id = 43, Name = "Bán đòng đòng 40", TranType = true, Quantity = 30, Value = 2100000, TranDate = DateTime.Now.AddDays(-3), ProductId = 5 },
                new Transactions { Id = 44, Name = "Bán rượu táo mèo", TranType = true, Quantity = 25, Value = 1500000, TranDate = DateTime.Now.AddDays(-2), ProductId = 8 },
                new Transactions { Id = 45, Name = "Mua nếp đen", TranType = false, Quantity = 100, Value = 2300000, TranDate = DateTime.Now.AddDays(-1), IngredientId = 3 }
            );
        }
    }
}