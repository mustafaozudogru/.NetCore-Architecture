using CoreArchitectureDesign.Core.Interfaces;

namespace CoreArchitectureDesign.Entities
{
    public class OrderModel : BaseEntity, IEntity
    {
        public int Id { get; set; }
        public string ContactName { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Region { get; set; }

        public OrderModel() { }
    }
}
