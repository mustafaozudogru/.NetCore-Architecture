using CoreArchitectureDesign.Core.Interfaces;
using System.Collections.Generic;

namespace CoreArchitectureDesign.Entities
{
    public partial class Categories : BaseEntity, IEntity
    {
        public Categories()
        {
            Products = new HashSet<Products>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public byte[] Picture { get; set; }

        public virtual ICollection<Products> Products { get; set; }
    }
}
