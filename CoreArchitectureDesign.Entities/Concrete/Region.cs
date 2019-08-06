using CoreArchitectureDesign.Core.Interfaces;
using System.Collections.Generic;

namespace CoreArchitectureDesign.Entities
{
    public partial class Region : BaseEntity, IEntity
    {
        public Region()
        {
            Territories = new HashSet<Territories>();
        }

        public int RegionId { get; set; }
        public string RegionDescription { get; set; }

        public virtual ICollection<Territories> Territories { get; set; }
    }
}
