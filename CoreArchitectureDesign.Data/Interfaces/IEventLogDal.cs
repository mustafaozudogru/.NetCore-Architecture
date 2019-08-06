using CoreArchitectureDesign.Core.Interfaces;
using CoreArchitectureDesign.Core.Log;

namespace CoreArchitectureDesign.Data.Interfaces
{
    public interface IEventLogDal : IEntityRepository<EventLog>
    {
    }
}
