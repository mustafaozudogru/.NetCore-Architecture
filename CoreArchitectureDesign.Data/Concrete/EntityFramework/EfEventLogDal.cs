using CoreArchitectureDesign.Business.Abstractions;
using CoreArchitectureDesign.Core.DataAccess;
using CoreArchitectureDesign.Core.Log;
using CoreArchitectureDesign.Data.Interfaces;

namespace CoreArchitectureDesign.Data.Concrete.EntityFramework
{
    public class EfEventLogDal : EfRepository<EventLog>, IEventLogDal
    {
        public EfEventLogDal(NorthwndContext context) : base(context)
        {
        }
    }
}
