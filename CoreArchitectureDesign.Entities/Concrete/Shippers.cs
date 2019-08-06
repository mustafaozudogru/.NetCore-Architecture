using CoreArchitectureDesign.Core.Interfaces;
using System.Collections.Generic;

namespace CoreArchitectureDesign.Entities
{
    public partial class Shippers : BaseEntity, IEntity
    {
        public Shippers()
        {
            Orders = new HashSet<Orders>();
        }

        public int ShipperId { get; set; }
        public string CompanyName { get; set; }
        public string Phone { get; set; }

        public virtual ICollection<Orders> Orders { get; set; }
    }
}
