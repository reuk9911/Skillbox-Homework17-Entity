using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skillbox_Homework17_Entity.Model
{
    public class Purchase
    {
        public int Id { get; set; }

        public string UserEmail { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }

        public virtual Client Clients { get; set; }
    }
}
