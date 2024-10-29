using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RuKiSo.Entities;

namespace RuKiSo.DataAccess
{
    public class RuKiSoDataContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public DbSet<Batches> Batches { get; set; }
        public DbSet<Ingredients> Ingredients { get; set; }
        public DbSet<Products> Products { get; set; }

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

            modelBuilder.Entity<Ingredients>()
                .HasOne<Batches>()
                .WithMany(b => b.Ingredients)
                .HasForeignKey(i => i.BatchId);

            modelBuilder.Entity<Batches>()
                .HasOne(b => b.Product)
                .WithOne(p => p.Batch)
                .HasForeignKey<Products>(p => p.BatchId);
        }
    }
}
