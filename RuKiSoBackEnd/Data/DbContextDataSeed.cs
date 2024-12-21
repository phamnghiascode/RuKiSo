using Microsoft.EntityFrameworkCore;
using RuKiSoBackEnd.Models.Domains;

namespace RuKiSoBackEnd.Data
{
    public partial class RuKiSoDataContext
    {
        private void SeedData(ModelBuilder modelBuilder)
        {
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
                new Batches { Id = 1, StartDate = new DateTime(2024, 1, 1), EstimateEndDate = new DateTime(2024, 3, 1), Yield = 0, ProductId = 1 },
                new Batches { Id = 2, StartDate = new DateTime(2024, 2, 1), EstimateEndDate = new DateTime(2024, 4, 1), Yield = 0, ProductId = 2 },
                new Batches { Id = 3, StartDate = new DateTime(2024, 3, 1), EstimateEndDate = new DateTime(2024, 5, 1), Yield = 0, ProductId = 3 },
                new Batches { Id = 4, StartDate = new DateTime(2024, 4, 1), EstimateEndDate = new DateTime(2024, 6, 1), Yield = 0, ProductId = 4 },
                new Batches { Id = 5, StartDate = new DateTime(2024, 5, 1), EstimateEndDate = new DateTime(2024, 7, 1), Yield = 0, ProductId = 5 },
                new Batches { Id = 6, StartDate = new DateTime(2024, 6, 1), EstimateEndDate = new DateTime(2024, 8, 1), Yield = 0, ProductId = 6 },
                new Batches { Id = 7, StartDate = new DateTime(2024, 7, 1), EstimateEndDate = new DateTime(2024, 9, 1), Yield = 0, ProductId = 7 },
                new Batches { Id = 8, StartDate = new DateTime(2024, 8, 1), EstimateEndDate = new DateTime(2024, 10, 1), Yield = 0, ProductId = 8 },
                new Batches { Id = 9, StartDate = new DateTime(2024, 9, 1), EstimateEndDate = new DateTime(2024, 11, 1), Yield = 0, ProductId = 1 },
                new Batches { Id = 10, StartDate = new DateTime(2024, 10, 1), EstimateEndDate = new DateTime(2024, 12, 1), Yield = 0, ProductId = 2 }
            );

            modelBuilder.Entity<BatchIngredient>().HasData(
                // Batch 1: Rượu trắng 45 (Nếp đen + Men thuốc bắc)
                new BatchIngredient { BatchId = 1, IngredientId = 3, Quantity = 50 },
                new BatchIngredient { BatchId = 1, IngredientId = 5, Quantity = 1 },

                // Batch 2: Rượu trắng 40 (Nếp cái hoa vàng + Men thường)
                new BatchIngredient { BatchId = 2, IngredientId = 2, Quantity = 45 },
                new BatchIngredient { BatchId = 2, IngredientId = 6, Quantity = 1 },

                // Batch 3: Rượu trắng 35 (Nếp đen + Men lá)
                new BatchIngredient { BatchId = 3, IngredientId = 3, Quantity = 40 },
                new BatchIngredient { BatchId = 3, IngredientId = 7, Quantity = 1 },

                // Batch 4: Đòng đòng 45 (Nếp cái hoa vàng + Men thuốc bắc)
                new BatchIngredient { BatchId = 4, IngredientId = 2, Quantity = 30 },
                new BatchIngredient { BatchId = 4, IngredientId = 5, Quantity = 1 },

                // Batch 5: Đòng đòng 40 (Gạo nếp + Men lá)
                new BatchIngredient { BatchId = 5, IngredientId = 1, Quantity = 25 },
                new BatchIngredient { BatchId = 5, IngredientId = 7, Quantity = 1 },

                // Batch 6: Đòng đòng 35 (Nếp đen + Men thường)
                new BatchIngredient { BatchId = 6, IngredientId = 3, Quantity = 20 },
                new BatchIngredient { BatchId = 6, IngredientId = 6, Quantity = 1 },

                // Batch 7: Rượu bách nhật (Đòng đòng + Men thuốc bắc)
                new BatchIngredient { BatchId = 7, IngredientId = 4, Quantity = 15 },
                new BatchIngredient { BatchId = 7, IngredientId = 5, Quantity = 1 },

                // Batch 8: Rượu táo mèo (Nếp đen + Táo mèo)
                new BatchIngredient { BatchId = 8, IngredientId = 3, Quantity = 20 },
                new BatchIngredient { BatchId = 8, IngredientId = 8, Quantity = 2 },

                // Batch 9: Rượu trắng 45 (Nếp cái hoa vàng + Men thường)
                new BatchIngredient { BatchId = 9, IngredientId = 2, Quantity = 50 },
                new BatchIngredient { BatchId = 9, IngredientId = 6, Quantity = 1 },

                // Batch 10: Rượu trắng 40 (Gạo nếp + Men thuốc bắc)
                new BatchIngredient { BatchId = 10, IngredientId = 1, Quantity = 45 },
                new BatchIngredient { BatchId = 10, IngredientId = 5, Quantity = 1 }
            );
        }
    }
}