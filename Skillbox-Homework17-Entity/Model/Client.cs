namespace Skillbox_Homework17_Entity.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Client
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Client()
        {
            Purchases = new HashSet<Purchase>();
        }

        //[Required]
        [StringLength(20)]
        public string lastName { get; set; }

        //[Required]
        [StringLength(20)]
        public string firstName { get; set; }

        //[Required]
        [StringLength(20)]
        public string middleName { get; set; }

        [StringLength(12)]
        public string phone { get; set; }

        [Key]
        [StringLength(40)]
        public string email { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Purchase> Purchases { get; set; }
    }
}
