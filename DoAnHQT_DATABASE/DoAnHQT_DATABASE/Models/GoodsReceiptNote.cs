namespace DoAnHQT_DATABASE.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("GoodsReceiptNote")]
    public partial class GoodsReceiptNote
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GoodsReceiptNote()
        {
            GoodsReceiptNoteDetail = new HashSet<GoodsReceiptNoteDetail>();
        }

        [StringLength(10)]
        public string GoodsReceiptNoteID { get; set; }

        [StringLength(10)]
        public string SupplierID { get; set; }

        public DateTime? ReceiptDate { get; set; }

        public int? IsDeleted { get; set; }

        public DateTime? CreatedAt { get; set; }

        [StringLength(30)]
        public string CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        [StringLength(30)]
        public string UpdatedBy { get; set; }

        public virtual Supplier Supplier { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GoodsReceiptNoteDetail> GoodsReceiptNoteDetail { get; set; }

        public double TongThanhTien
        {
            get
            {
                return (double)GoodsReceiptNoteDetail.Sum(t => t.Quantity.Value * t.UnitPrice.Value);
            }
        }
    }
}
