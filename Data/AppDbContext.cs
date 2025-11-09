using Microsoft.EntityFrameworkCore;
using DevPortfolioAPI.Models;

namespace DevPortfolioAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Project> Projects => Set<Project>();
        public DbSet<Link> Links => Set<Link>();
        public DbSet<Tag> Tags => Set<Tag>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Project>()
                .HasMany(p => p.Links)
                .WithOne(l => l.Project)
                .HasForeignKey(l => l.ProjectId);

            modelBuilder.Entity<Project>()
                .HasMany(p => p.Tags)
                .WithMany(t => t.Projects);

            modelBuilder.Entity<User>().Property(u => u.Email).HasMaxLength(256);
            modelBuilder.Entity<Tag>().Property(t => t.Name).HasMaxLength(100);
            modelBuilder.Entity<Project>().Property(p => p.Title).HasMaxLength(200);
        }
    }
}
