using CoreArchitectureDesign.Core.Interfaces;

namespace CoreArchitectureDesign.Entities
{
    public partial class EmployeeTerritories : BaseEntity, IEntity
    {   
        public int EmployeeId { get; set; }
        public string TerritoryId { get; set; }

        public virtual Employees Employee { get; set; }
        public virtual Territories Territory { get; set; }
    }
}
