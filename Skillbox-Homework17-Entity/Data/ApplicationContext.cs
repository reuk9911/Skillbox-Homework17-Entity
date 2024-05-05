using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Skillbox_Homework17_Entity.Model;
using System.Data.SqlClient;

namespace Skillbox_Homework17_Entity.Data
{
    public class ApplicationContext: DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Purchase> Purchases { get; set; }


        public ApplicationContext(): base("DbConnection")
        {
            if (!Database.Exists())
                Database.Create();
        }

    }
}
