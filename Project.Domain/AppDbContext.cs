namespace Project.Domain
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Project.Domain.Entities;

    public partial class AppDbContext : DbContext
    {
        public AppDbContext()
            : base("name=AppDbContext")
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public virtual DbSet<branches> branchs { get; set; }

        public virtual DbSet<branch_bank> branch_bank { get; set; }

        public virtual DbSet<banks> banks { get; set; }
    }
}
