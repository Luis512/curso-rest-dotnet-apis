using Microsoft.EntityFrameworkCore;

namespace eMarketDB.Infrastructure.Data
{
    public partial class eMarketDBContext : DbContext
    {
        public eMarketDBContext()
        {
        }

        public eMarketDBContext(DbContextOptions<eMarketDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<ProductByOrder> ProductByOrder { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<Reviews> Reviews { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categories>(entity =>
            {
                entity.Property(e => e.Id).HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.Property(e => e.Id).HasColumnType("numeric(18, 0)");

                entity.Property(e => e.IdUser).HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Status)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Total).HasColumnType("numeric(18, 0)");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orders_User");
            });

            modelBuilder.Entity<ProductByOrder>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.IdOrder)
                    .HasColumnName("idOrder")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.IdProduct)
                    .HasColumnName("idProduct")
                    .HasColumnType("numeric(18, 0)");

                entity.HasOne(d => d.IdOrderNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdOrder)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductByOrder_Orders");

                entity.HasOne(d => d.IdProductNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdProduct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductByOrder_Products");
            });

            modelBuilder.Entity<Products>(entity =>
            {
                entity.Property(e => e.Id).HasColumnType("numeric(18, 0)");

                entity.Property(e => e.IdCategory).HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Stock).HasColumnType("numeric(18, 0)");

                entity.HasOne(d => d.IdCategoryNavigation)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.IdCategory)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Products_Categories");
            });

            modelBuilder.Entity<Reviews>(entity =>
            {
                entity.Property(e => e.Id).HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IdProduct).HasColumnType("numeric(18, 0)");

                entity.Property(e => e.IdUser).HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Rating).HasColumnType("numeric(5, 0)");

                entity.HasOne(d => d.IdProductNavigation)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.IdProduct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Reviews_Products");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Reviews_User");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Country)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Direction).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
