using Microsoft.EntityFrameworkCore;

namespace BackEndTestTask.Models.Database.Context
{
    public class AppDatabaseContext : DbContext
    {
        public DbSet<Node> Nodes { get; set; }
        public DbSet<ExceptionJournal> ExceptionsJournal { get; set; }

        public AppDatabaseContext(DbContextOptions<AppDatabaseContext> options) 
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Node>(entity =>
            {
                entity.Property(p => p.Id)
                    .IsRequired()
                    .ValueGeneratedOnAdd();

                entity.Property(p => p.Name)
                    .IsRequired();

                entity.Property(p => p.ParentId)
                    .IsRequired(false);

                entity.HasKey(p => p.Id);

                entity.HasIndex(p => p.Id)
                    .IsUnique();

                entity.HasMany(n => n.Children)
                    .WithOne(n => n.Parent)
                    .HasForeignKey(n => n.ParentId);

                entity.HasOne(n => n.Parent)
                    .WithMany(n => n.Children)
                    .HasForeignKey(n => n.ParentId);
            });

            modelBuilder.Entity<ExceptionJournal>(entity =>
            {
                entity.HasKey(p => p.EventId);
            });
        }
    }
}