using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace WallpaperAPI
{
    public class WallpaperDbContext : DbContext
    {
        public WallpaperDbContext(DbContextOptions<WallpaperDbContext> options) : base(options) { }

        public DbSet<Category> Category { get; set; }
        public DbSet<Wallpaper> Wallpapers { get; set; }
        public DbSet<WallpaperCategory> WallpaperCategory { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Tablo isimlerini doğru şekilde eşle
            modelBuilder.Entity<Category>().ToTable("Category");
            modelBuilder.Entity<Wallpaper>().ToTable("Wallpapers");
            modelBuilder.Entity<WallpaperCategory>().ToTable("WallpaperCategory");

            // İlişki ayarları
            modelBuilder.Entity<WallpaperCategory>()
                .HasKey(wc => wc.WallpaperCategoryID);

            modelBuilder.Entity<WallpaperCategory>()
                .HasOne(wc => wc.Category)
                .WithMany(c => c.WallpaperCategories)
                .HasForeignKey(wc => wc.CategoryID);

            modelBuilder.Entity<WallpaperCategory>()
                .HasOne(wc => wc.Wallpaper)
                .WithMany(w => w.WallpaperCategories)
                .HasForeignKey(wc => wc.WallpaperID);

            base.OnModelCreating(modelBuilder);
        }

    }
}
