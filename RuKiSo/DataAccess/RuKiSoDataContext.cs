using AutoMapper;
using RuKiSo.Entities;

namespace RuKiSo.DataAccess
{
    public class RuKiSoDataContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public DbSet<Batch> Batches { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Product> Products { get; set; }

        public RuKiSoDataContext(
            IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public RuKiSoDataContext(DbContextOptions<RuKiSoDataContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Ingredient>()
                .HasOne(i => i.Batch)
                .WithMany(b => b.Ingredients)
                .HasForeignKey(i => i.BatchId);

            modelBuilder.Entity<Batch>()
                .HasOne(b => b.Product)
                .WithMany(p => p.Batches)
                .HasForeignKey(b => b.ProductId);
        }
    }
}
