using AkilliTarimUygulamasi.Models;
using Microsoft.EntityFrameworkCore;

namespace AkilliTarimUygulaması.Models
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Field> Fields { get; set; }
        public DbSet<Crop> Crops { get; set; }
        public DbSet<Sale> Sales { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        /*protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relationship configurations
            modelBuilder.Entity<Field>()
                .HasOne(f => f.User)
                .WithMany(u => u.Fields)
                .HasForeignKey(f => f.UserId);

            modelBuilder.Entity<Crop>()
                .HasOne(c => c.Field)
                .WithMany(f => f.Crops)
                .HasForeignKey(c => c.FieldId);

            modelBuilder.Entity<Sale>()
                .HasOne(s => s.User)
                .WithMany(u => u.Sales)
                .HasForeignKey(s => s.UserId);

            modelBuilder.Entity<Sale>()
                .HasOne(s => s.Crop)
                .WithMany(c => c.Sales)
                .HasForeignKey(s => s.CropId);

            modelBuilder.Entity<Sale>()
                .HasOne(s => s.Field)
                .WithMany(f => f.Sales)
                .HasForeignKey(s => s.FieldId);
        }*/
    }
}