using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ShopOnline.Models.DBObjects;

namespace ShopOnline.Data
{
    public partial class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

       // public virtual DbSet<AspNetRole> AspNetRoles { get; set; } = null!;
       // public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; } = null!;
       // public virtual DbSet<AspNetUser> AspNetUsers { get; set; } = null!;
       // public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; } = null!;
       // public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; } = null!;
       // public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; } = null!;
       public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderDetail> OrderDetails { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<ShoppingCartItem> ShoppingCartItems { get; set; } = null!;
        public virtual DbSet<Tva> Tvas { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=DefaultConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<AspNetUserLogin>().HasKey(l => new { l.LoginProvider, l.ProviderKey });
            modelBuilder.Entity<AspNetUserToken>().HasKey(t => new { t.UserId, t.LoginProvider, t.Name });
            /*  modelBuilder.Entity<AspNetRole>(entity =>
              {
                  entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                      .IsUnique()
                      .HasFilter("([NormalizedName] IS NOT NULL)");

                  entity.Property(e => e.Name).HasMaxLength(256);

                  entity.Property(e => e.NormalizedName).HasMaxLength(256);
              });

              modelBuilder.Entity<AspNetRoleClaim>(entity =>
              {
                  entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

                  entity.HasOne(d => d.Role)
                      .WithMany(p => p.AspNetRoleClaims)
                      .HasForeignKey(d => d.RoleId);
              });

              modelBuilder.Entity<AspNetUser>(entity =>
              {
                  entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

                  entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                      .IsUnique()
                      .HasFilter("([NormalizedUserName] IS NOT NULL)");

                  entity.Property(e => e.Email).HasMaxLength(256);

                  entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                  entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                  entity.Property(e => e.UserName).HasMaxLength(256);

                  entity.HasMany(d => d.Roles)
                      .WithMany(p => p.Users)
                      .UsingEntity<Dictionary<string, object>>(
                          "AspNetUserRole",
                          l => l.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                          r => r.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                          j =>
                          {
                              j.HasKey("UserId", "RoleId");

                              j.ToTable("AspNetUserRoles");

                              j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                          });
              });

              modelBuilder.Entity<AspNetUserClaim>(entity =>
              {
                  entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

                  entity.HasOne(d => d.User)
                      .WithMany(p => p.AspNetUserClaims)
                      .HasForeignKey(d => d.UserId);
              });

              modelBuilder.Entity<AspNetUserLogin>(entity =>
              {
                  entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                  entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

                  entity.Property(e => e.LoginProvider).HasMaxLength(128);

                  entity.Property(e => e.ProviderKey).HasMaxLength(128);

                  entity.HasOne(d => d.User)
                      .WithMany(p => p.AspNetUserLogins)
                      .HasForeignKey(d => d.UserId);
              });

              modelBuilder.Entity<AspNetUserToken>(entity =>
              {
                  entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                  entity.Property(e => e.LoginProvider).HasMaxLength(128);

                  entity.Property(e => e.Name).HasMaxLength(128);

                  entity.HasOne(d => d.User)
                      .WithMany(p => p.AspNetUserTokens)
                      .HasForeignKey(d => d.UserId);
              });*/

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.IdCategory)
                    .HasName("PK__Category__CBD74706EBAC0C65");

                entity.ToTable("Category");

                entity.Property(e => e.IdCategory).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(350);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.IdOrder)
                    .HasName("PK__Orders__C38F3009249DAC2A");

                entity.Property(e => e.IdOrder).ValueGeneratedNever();

                entity.Property(e => e.Address).HasColumnType("text");

                entity.Property(e => e.FirstName).HasMaxLength(350);

                entity.Property(e => e.IdUser).HasMaxLength(450);

                entity.Property(e => e.LastName).HasMaxLength(350);

                entity.Property(e => e.OrderDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasKey(e => e.IdOrderDetails)
                    .HasName("PK__OrderDet__8D4849D63E6CEA57");

                entity.Property(e => e.IdOrderDetails).ValueGeneratedNever();

                entity.Property(e => e.IdTva).HasColumnName("IdTVA");

                entity.Property(e => e.PriceTotal).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.PriceTva)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("PriceTVA");

                entity.HasOne(d => d.IdOrderNavigation)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.IdOrder)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderDetails_ToTable");

                entity.HasOne(d => d.IdProductNavigation)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.IdProduct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderDetails_ToTable_1");

                entity.HasOne(d => d.IdTvaNavigation)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.IdTva)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderDetails_ToTable_2");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.IdProduct)
                    .HasName("PK__tmp_ms_x__2E8946D4E65D2460");

                entity.Property(e => e.IdProduct).ValueGeneratedNever();

                entity.Property(e => e.Description).HasColumnType("text");

                entity.Property(e => e.Image).HasColumnType("text");

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.IdCategoryNavigation)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.IdCategory)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Products_ToTable");
            });

            modelBuilder.Entity<ShoppingCartItem>(entity =>
            {
                entity.ToTable("ShoppingCartItem");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.TotalPrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.UserId).HasMaxLength(450);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ShoppingCartItems)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ShoppingCartItem_Product");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ShoppingCartItems)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ShoppingCartItem_User");
            });

            modelBuilder.Entity<Tva>(entity =>
            {
                entity.HasKey(e => e.IdTva)
                    .HasName("PK__TVA__2BC4BEA3F6EED925");

                entity.ToTable("TVA");

                entity.Property(e => e.IdTva)
                    .ValueGeneratedNever()
                    .HasColumnName("IdTVA");

                entity.Property(e => e.Tva1)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("TVA");

                entity.HasOne(d => d.IdCategoryNavigation)
                    .WithMany(p => p.Tvas)
                    .HasForeignKey(d => d.IdCategory)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TVA_ToTable");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
