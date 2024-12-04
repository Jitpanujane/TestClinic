namespace Project.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("branch_bank")]
    public partial class branch_bank
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int branch_bank_id { get; set; }

        public int branch_id { get; set; }

        public string bank_code { get; set; }

        public string bank_number { get; set; }
    }
}
