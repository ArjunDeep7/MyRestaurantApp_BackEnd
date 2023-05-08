using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MyRestaurant.Models
{
    public partial class RestaurantDBContext : DbContext
    {
        public RestaurantDBContext()
        {
        }

        public RestaurantDBContext(DbContextOptions<RestaurantDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<CategoryDish> CategoryDishes { get; set; } = null!;
        public virtual DbSet<Dish> Dishes { get; set; } = null!;
        public virtual DbSet<Menu> Menus { get; set; } = null!;
        public virtual DbSet<MenuCategory> MenuCategories { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=M5387495\\SQLEXPRESS;Database=RestaurantDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.CatId)
                    .HasName("PK__Category__17B6DD066176AA19");

                entity.ToTable("Category");

                entity.Property(e => e.CatId).HasColumnName("catId");

                entity.Property(e => e.CatImage)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("catImage");

                entity.Property(e => e.CatName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("catName");

                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");
            });

            modelBuilder.Entity<CategoryDish>(entity =>
            {
                entity.ToTable("Category_Dish");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CatId).HasColumnName("catId");

                entity.Property(e => e.DishId).HasColumnName("dishId");

                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

                entity.HasOne(d => d.Cat)
                    .WithMany(p => p.CategoryDishes)
                    .HasForeignKey(d => d.CatId)
                    .HasConstraintName("FK__Category___catId__571DF1D5");

                entity.HasOne(d => d.Dish)
                    .WithMany(p => p.CategoryDishes)
                    .HasForeignKey(d => d.DishId)
                    .HasConstraintName("FK__Category___dishI__5812160E");
            });

            modelBuilder.Entity<Dish>(entity =>
            {
                entity.ToTable("Dish");

                entity.Property(e => e.DishId).HasColumnName("dishId");

                entity.Property(e => e.DishDescription)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("dishDescription");

                entity.Property(e => e.DishImage)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("dishImage");

                entity.Property(e => e.DishName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("dishName");

                entity.Property(e => e.DishNature)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("dishNature");

                entity.Property(e => e.DishPrice).HasColumnName("dishPrice");

                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.ToTable("Menu");

                entity.Property(e => e.MenuId).HasColumnName("menuId");

                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

                entity.Property(e => e.MenuImage)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("menuImage");

                entity.Property(e => e.MenuName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("menuName");
            });

            modelBuilder.Entity<MenuCategory>(entity =>
            {
                entity.ToTable("Menu_Category");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CatId).HasColumnName("catId");

                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

                entity.Property(e => e.MenuId).HasColumnName("menuId");

                entity.HasOne(d => d.Cat)
                    .WithMany(p => p.MenuCategories)
                    .HasForeignKey(d => d.CatId)
                    .HasConstraintName("FK__Menu_Cate__catId__5070F446");

                entity.HasOne(d => d.Menu)
                    .WithMany(p => p.MenuCategories)
                    .HasForeignKey(d => d.MenuId)
                    .HasConstraintName("FK__Menu_Cate__menuI__4F7CD00D");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("userId");

                entity.Property(e => e.Password)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("password");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
