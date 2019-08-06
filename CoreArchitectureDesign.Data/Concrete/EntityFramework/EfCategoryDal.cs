using CoreArchitectureDesign.Core.DataAccess;
using CoreArchitectureDesign.Data.Interfaces;
using CoreArchitectureDesign.Entities;

namespace CoreArchitectureDesign.Data.Concrete.EntityFramework
{
    public class EfCategoryDal : EfRepository<Categories>, ICategoryDal
    {
        public EfCategoryDal(NorthwndContext context) : base(context)
        {
        }
    }
}
