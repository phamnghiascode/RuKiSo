using Microsoft.EntityFrameworkCore;
using RuKiSoBackEnd.Models.Domains;

namespace RuKiSoBackEnd.Data
{
    public partial class RuKiSoDataContext : DbContext
    {
        public RuKiSoDataContext(DbContextOptions contextOptions) : base(contextOptions) { }

        public DbSet<Products> Products { get; set; }
        public DbSet<Ingredients> Ingredients { get; set; }
        public DbSet<Batches> Batches { get; set; }
        public DbSet<Transactions> Transactions { get; set; }
        public DbSet<BatchIngredient> BatchIngredients { get; set; }

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

            modelBuilder.Entity<Ingredients>(entity =>
            {
                entity.HasKey(i => i.Id);
            });

            modelBuilder.Entity<Batches>(entity =>
            {
                entity.HasKey(b => b.Id);
            });

            modelBuilder.Entity<BatchIngredient>(entity =>
            {
                entity.HasKey(bi => new { bi.BatchId, bi.IngredientId });

                entity.HasOne(bi => bi.Batch)
                    .WithMany(b => b.BatchIngredients)
                    .HasForeignKey(bi => bi.BatchId);

                entity.HasOne(bi => bi.Ingredient)
                    .WithMany(i => i.BatchIngredients)
                    .HasForeignKey(bi => bi.IngredientId);
            });

            SeedData(modelBuilder);
        }
    }
}