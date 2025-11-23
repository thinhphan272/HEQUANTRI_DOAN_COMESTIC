using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace DoAnHQT_DATABASE.Models
{
    public partial class QL_BANHANG_ONLINE : DbContext
    {
        public QL_BANHANG_ONLINE()
            : base("name=QL_BANHANG_ONLINE")
        {
        }

        public virtual DbSet<Brand> Brand { get; set; }
        public virtual DbSet<Discount> Discount { get; set; }
        public virtual DbSet<GoodsReceiptNote> GoodsReceiptNote { get; set; }
        public virtual DbSet<GoodsReceiptNoteDetail> GoodsReceiptNoteDetail { get; set; }
        public virtual DbSet<OrderDetail> OrderDetail { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductType> ProductType { get; set; }
        public virtual DbSet<Rating> Rating { get; set; }
        public virtual DbSet<ShoppingCart> ShoppingCart { get; set; }
        public virtual DbSet<ShoppingCartItem> ShoppingCartItem { get; set; }
        public virtual DbSet<Supplier> Supplier { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Brand>()
                .Property(e => e.BrandID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Discount>()
                .Property(e => e.DiscountID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Discount>()
                .Property(e => e.ProductID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<GoodsReceiptNote>()
                .Property(e => e.GoodsReceiptNoteID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<GoodsReceiptNote>()
                .Property(e => e.SupplierID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<GoodsReceiptNote>()
                .HasMany(e => e.GoodsReceiptNoteDetail)
                .WithRequired(e => e.GoodsReceiptNote)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GoodsReceiptNoteDetail>()
                .Property(e => e.ProductID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<GoodsReceiptNoteDetail>()
                .Property(e => e.GoodsReceiptNoteID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<GoodsReceiptNoteDetail>()
                .Property(e => e.UnitPrice)
                .HasPrecision(19, 4);

            modelBuilder.Entity<OrderDetail>()
                .Property(e => e.OrderID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<OrderDetail>()
                .Property(e => e.ProductID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<OrderDetail>()
                .Property(e => e.UnitPrice)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Orders>()
                .Property(e => e.OrderID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Orders>()
                .Property(e => e.UserID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Orders>()
                .HasMany(e => e.OrderDetail)
                .WithRequired(e => e.Orders)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.ProductID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.ProductTypeID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.BrandID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.GoodsReceiptNoteDetail)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.OrderDetail)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.ShoppingCartItem)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProductType>()
                .Property(e => e.ProductTypeID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ProductType>()
                .Property(e => e.ProductTypeParentID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Rating>()
                .Property(e => e.RatingID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Rating>()
                .Property(e => e.UserID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Rating>()
                .Property(e => e.ProductID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ShoppingCart>()
                .Property(e => e.ShoppingCartID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ShoppingCart>()
                .Property(e => e.UserID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ShoppingCart>()
                .HasMany(e => e.ShoppingCartItem)
                .WithRequired(e => e.ShoppingCart)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ShoppingCartItem>()
                .Property(e => e.ShoppingCartID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ShoppingCartItem>()
                .Property(e => e.ProductID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Supplier>()
                .Property(e => e.SupplierID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.UserID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.Password)
                .IsUnicode(false);
        }
    }
}
