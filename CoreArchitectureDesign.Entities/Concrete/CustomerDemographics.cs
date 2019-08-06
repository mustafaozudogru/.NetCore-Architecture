using CoreArchitectureDesign.Core.Interfaces;
using System.Collections.Generic;

namespace CoreArchitectureDesign.Entities
{
    public partial class CustomerDemographics : BaseEntity, IEntity
    {
        public CustomerDemographics()
        {
            CustomerCustomerDemo = new HashSet<CustomerCustomerDemo>();
        }

        public string CustomerTypeId { get; set; }
        public string CustomerDesc { get; set; }

        public virtual ICollection<CustomerCustomerDemo> CustomerCustomerDemo { get; set; }
    }
}
