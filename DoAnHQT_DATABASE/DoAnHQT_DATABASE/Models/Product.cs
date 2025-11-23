namespace DoAnHQT_DATABASE.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("Product")]
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            Discount = new HashSet<Discount>();
            GoodsReceiptNoteDetail = new HashSet<GoodsReceiptNoteDetail>();
            OrderDetail = new HashSet<OrderDetail>();
            Rating = new HashSet<Rating>();
            ShoppingCartItem = new HashSet<ShoppingCartItem>();
        }

        [StringLength(10)]
        public string ProductID { get; set; }

        [StringLength(10)]
        public string ProductTypeID { get; set; }

        [StringLength(30)]
        public string ProductName { get; set; }

        [StringLength(10)]
        public string BrandID { get; set; }

        [Column(TypeName = "money")]
        public decimal? Price { get; set; }

        [StringLength(30)]
        public string Origin { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        [StringLength(100)]
        public string Image { get; set; }

        public double? Capacity { get; set; }

        public int? Quantity { get; set; }

        public DateTime? ExpirationDate { get; set; }

        public int? IsAvailable { get; set; }

        public DateTime? CreatedAt { get; set; }

        [StringLength(30)]
        public string CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        [StringLength(30)]
        public string UpdatedBy { get; set; }





        public virtual Brand Brand { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Discount> Discount { get; set; }

        [NotMapped]
        public double? DiscountRate
        {
            get
            {
                if (Discount != null)
                {
                    return Discount.First().DiscountRate;
                }
                else
                {
                    return 0;
                }
            }
            set;
        }

        [NotMapped]
        public double? GiaDaGiam
        {
            get
            {
                if (Discount != null)
                {
                    return (double)Price * (100 - DiscountRate) / 100;
                }
                else
                {
                    return (double)Price;
                }
            }
            set;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GoodsReceiptNoteDetail> GoodsReceiptNoteDetail { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetail { get; set; }

        public virtual ProductType ProductType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Rating> Rating { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShoppingCartItem> ShoppingCartItem { get; set; }
    }
}
