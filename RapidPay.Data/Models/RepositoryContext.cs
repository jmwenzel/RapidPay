using Microsoft.EntityFrameworkCore;
using RapidPay.Models.Entities;

namespace RapidPay.Data.Models
{
    public partial class RepositoryContext : DbContext
    {
        public RepositoryContext()
        {

        }

        public RepositoryContext(DbContextOptions<RepositoryContext> options)
            : base(options)
        {

        }

        public virtual DbSet<Card> Card { get; set; }
        public virtual DbSet<Payment> Payment { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Card>(entity =>
            {
                entity.Property(e => e.Number)
                    .IsRequired()
                    .HasMaxLength(15);

            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.Property(e => e.Amount)
                    .IsRequired()
                    .HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Fee)
                    .IsRequired()
                    .HasColumnType("decimal(10, 2)");

                entity.Property(e => e.TotalAmount)
                    .IsRequired()
                    .HasColumnType("decimal(10, 2)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }

}
