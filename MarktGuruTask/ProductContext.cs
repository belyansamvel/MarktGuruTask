using MarktGuruTask.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MarktGuruTask
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired();

                entity.Property(e => e.Available)
                    .HasDefaultValue(false);

                entity.Property(e => e.Price)
                    .HasPrecision(9, 6);

                entity.HasIndex(e => e.Name).IsUnique();
            });

            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity);

            foreach (var entry in entries)
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.Property("DateCreated").IsModified = false;
                        break;
                    case EntityState.Added:
                        ((BaseEntity)entry.Entity).DateCreated = DateTime.UtcNow;
                        break;
                    case EntityState.Detached:
                        break;
                    case EntityState.Unchanged:
                        break;
                    case EntityState.Deleted:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
