using Microsoft.EntityFrameworkCore;
using RuKiSoBackEnd.Models.Domains;

namespace RuKiSoBackEnd.Data
{
    public partial class RuKiSoDataContext : DbContext
    {
        public RuKiSoDataContext(DbContextOptions contextOptions) : base(contextOptions)
        {
                
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transactions>(entity =>
            {
                entity.HasKey(t => t.Id);
                entity.HasOne(t => t.Ingredient)
                    .WithMany(i => i.Transactions)
                    .HasForeignKey(t => t.IngredientId)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.SetNull);
                entity.HasOne(t => t.Product)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(t => t.ProductId)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<Products>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.HasMany(p => p.Batches)
                    .WithOne(b => b.Product)
                    .HasForeignKey(b => b.ProductId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<Batches>(entity =>
            {
                entity.HasKey(b => b.Id);
                entity.HasMany(b => b.Ingredients)
                    .WithOne(i => i.Batch)
                    .HasForeignKey(i => i.BatchId)
                    .OnDelete(DeleteBehavior.SetNull); 
            });

            modelBuilder.Entity<Ingredients>(entity =>
            {
                entity.HasKey(i => i.Id);
                entity.HasMany(i => i.Transactions)
                    .WithOne(t => t.Ingredient)
                    .HasForeignKey(t => t.IngredientId)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.SetNull);
            });
            SeedData(modelBuilder);
        }

        public DbSet<Products> Products { get; set; }
        public DbSet<Ingredients> Ingredients { get; set; }
        public DbSet<Batches> Batches { get; set; }
        public DbSet<Transactions> Transactions { get; set; }
    }
}
