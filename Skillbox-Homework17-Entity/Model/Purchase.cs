namespace Skillbox_Homework17_Entity.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Purchase
    {
        public int id { get; set; }

        [Required]
        [StringLength(40)]
        public string email { get; set; }

        [StringLength(10)]
        public string productCode { get; set; }

        [StringLength(40)]
        public string productName { get; set; }

        public virtual Client Client { get; set; }
    }
}
