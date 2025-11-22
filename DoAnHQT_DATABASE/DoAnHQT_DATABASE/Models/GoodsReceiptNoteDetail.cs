namespace DoAnHQT_DATABASE.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GoodsReceiptNoteDetail")]
    public partial class GoodsReceiptNoteDetail
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        public string ProductID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string GoodsReceiptNoteID { get; set; }

        [Column(TypeName = "money")]
        public decimal? UnitPrice { get; set; }

        public int? Quantity { get; set; }

        public virtual GoodsReceiptNote GoodsReceiptNote { get; set; }

        public virtual Product Product { get; set; }
    }
}
