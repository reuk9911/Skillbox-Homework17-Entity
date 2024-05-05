using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using Skillbox_Homework17_Entity.Model;

namespace Skillbox_Homework17_Entity.Data
{
    public partial class ApplicationContext : DbContext
    {
        public ApplicationContext(): base("name=DbConnection")
        {
        }

        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Purchase> Purchases { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>()
                .Property(e => e.phone)
                .IsUnicode(false);
        }
    }
}
