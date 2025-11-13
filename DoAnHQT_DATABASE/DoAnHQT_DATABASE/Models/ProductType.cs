namespace DoAnHQT_DATABASE.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProductType")]
    public partial class ProductType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProductType()
        {
            Product = new HashSet<Product>();
        }

        [StringLength(10)]
        public string ProductTypeID { get; set; }

        [StringLength(50)]
        public string ProductTypeName { get; set; }

        [StringLength(10)]
        public string ProductTypeParentID { get; set; }

        public int? IsDeleted { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> Product { get; set; }
    }
}
