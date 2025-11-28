namespace DoAnHQT_DATABASE.Models
{
    using DoAnHQT_DATABASE.Areas.Admin.Service;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class Orders
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Orders()
        {
            OrderDetail = new HashSet<OrderDetail>();
        }

        [Key]
        [StringLength(10)]
        public string OrderID { get; set; }

        [StringLength(10)]
        public string UserID { get; set; }

        public DateTime? OrderDate { get; set; }

        [StringLength(100)]
        public string Address { get; set; }

        [StringLength(20)]
        public string Status { get; set; }

        [StringLength(30)]
        public string UserPaymentMethod { get; set; }

        public DateTime? CreatedAt { get; set; }

        [StringLength(30)]
        public string CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        [StringLength(30)]
        public string UpdatedBy { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetail { get; set; }

        public virtual Users Users { get; set; }

        public int TongSoLuong
        {
            get => OrderDetail.ToList().Sum(t => t.Quantity.Value);
        }
        public double TongThanhTien
        {
            //get => OrderDetail.Sum(t => (double)t.UnitPrice.Value * t.Quantity.Value);
            get
            {
                OrderService orderService = new OrderService();
                return orderService.TongThanhTienDonHang(OrderID);
            }
        }
    }
}
