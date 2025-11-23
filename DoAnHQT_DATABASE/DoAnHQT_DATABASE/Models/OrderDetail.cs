namespace DoAnHQT_DATABASE.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrderDetail")]
    public partial class OrderDetail
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        public string OrderID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string ProductID { get; set; }

        public int? Quantity { get; set; }

        [Column(TypeName = "money")]
        public decimal? UnitPrice { get; set; }

        public virtual Orders Orders { get; set; }

        public virtual Product Product { get; set; }
    }
}
