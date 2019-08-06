using CoreArchitectureDesign.Core.Interfaces;

namespace CoreArchitectureDesign.Entities
{
    public partial class CustomerCustomerDemo : BaseEntity, IEntity
    {
        public string CustomerId { get; set; }
        public string CustomerTypeId { get; set; }

        public virtual Customers Customer { get; set; }
        public virtual CustomerDemographics CustomerType { get; set; }
    }
}
