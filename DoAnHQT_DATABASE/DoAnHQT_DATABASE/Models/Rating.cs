namespace DoAnHQT_DATABASE.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Rating")]
    public partial class Rating
    {
        [StringLength(10)]
        public string RatingID { get; set; }

        [StringLength(10)]
        public string UserID { get; set; }

        [StringLength(10)]
        public string ProductID { get; set; }

        public int? Star { get; set; }

        [StringLength(500)]
        public string Comment { get; set; }

        public DateTime? CreatedAt { get; set; }

        [StringLength(30)]
        public string CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        [StringLength(30)]
        public string UpdatedBy { get; set; }

        public virtual Product Product { get; set; }

        public virtual Users Users { get; set; }
    }
}
