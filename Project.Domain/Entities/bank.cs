namespace Project.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("banks")]
    public partial class banks
    {
        [Key]
        [StringLength(10)]
        public string bank_code { get; set; }

        [StringLength(100)]
        public string bank_name { get; set; }

        [StringLength(250)]
        public string bank_image { get; set; }
    }
}
