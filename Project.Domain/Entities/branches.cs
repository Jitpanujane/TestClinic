namespace Project.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("branchs")]
    public partial class branches
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int branch_id { get; set; }

        [Required]
        [StringLength(50)]
        public string branch_code { get; set; }

        [Required]
        [StringLength(50)]
        public string branch_name_th { get; set; }

        [Required]
        [StringLength(50)]
        public string branch_name_en { get; set; }

        [Required]
        [StringLength(50)]
        public string email { get; set; }

        public int branch_number { get; set; }

        public string country_code { get; set; }

        public string phone { get; set; }

        public string fax { get; set; }

        public string address { get; set; }

        public string alley { get; set; }

        public string road { get; set; }

        public string city { get; set; }

        public string province { get; set; }

        public string postcode { get; set; }

        public string facebook { get; set; }

        public string line { get; set; }

        public string instagram { get; set; }

        public string tax_number { get; set; }

        public string logo { get; set; }

        public string image { get; set; }

        public string status { get; set; }

        public DateTime updateAt { get; set; }

        public DateTime createAt { get; set; }
    }
}
