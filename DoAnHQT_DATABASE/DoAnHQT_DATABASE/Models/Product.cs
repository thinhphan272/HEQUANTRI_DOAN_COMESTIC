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

        // Khai báo biến lưu trữ giá trị (Backing fields)
        private double? _discountRate;
        private double? _giaDaGiam;

        [NotMapped]
        public double? DiscountRate
        {
            get
            {
                // 1. Ưu tiên: Nếu đã được gán giá trị (từ Service/SQL), trả về giá trị đó
                if (_discountRate.HasValue)
                {
                    return _discountRate.Value;
                }

                // 2. Dự phòng: Nếu chưa gán, thử tính toán theo logic Entity Framework (kiểm tra list Discount)
                if (Discount != null && Discount.Count() > 0)
                {
                    return Discount.First().DiscountRate;
                }

                return 0;
            }
            set
            {
                _discountRate = value;
            }
        }

        [NotMapped]
        public double? GiaDaGiam
        {
            get
            {
                // 1. Ưu tiên: Nếu đã được gán giá trị (từ Service/SQL), trả về giá trị đó
                if (_giaDaGiam.HasValue)
                {
                    return _giaDaGiam.Value;
                }

                // 2. Dự phòng: Tính toán dựa trên Price và DiscountRate hiện tại
                if (Price.HasValue)
                {
                    double rate = DiscountRate ?? 0; // Gọi lại getter DiscountRate ở trên
                    return (double)Price.Value * (100 - rate) / 100;
                }

                return 0;
            }
            set
            {
                _giaDaGiam = value;
            }
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
