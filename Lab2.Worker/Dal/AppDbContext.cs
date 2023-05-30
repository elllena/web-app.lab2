using Lab2.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lab2.Worker.Dal
{
    public class AppDbContext: DbContext
    {
        public DbSet<Knife> Knifes { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Knife>().HasKey(p => p.Name);

            modelBuilder.Entity<Knife>().OwnsOne(p => p.Visual).OwnsOne(p => p.HandType);
        }
    }
}
