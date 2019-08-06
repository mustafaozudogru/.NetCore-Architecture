using CoreArchitectureDesign.Business.Abstractions;
using CoreArchitectureDesign.Core.Interfaces;
using CoreArchitectureDesign.Core.Services;
using CoreArchitectureDesign.Data.Interfaces;
using Microsoft.Extensions.Logging;
using CoreArchitectureDesign.Entities;

namespace CoreArchitectureDesign.Business
{
    public class CategoryService : EntityService<Categories>, ICategoryService
    {
        private ICategoryDal categoryDal;
        private IUnitOfWork unitOfWork;
        private ILogger logger;

        public CategoryService(ICategoryDal categoryDal, ILogger logger, IEventLogDal eventLog, IUnitOfWork unitOfWork)
            : base(categoryDal, unitOfWork, logger)
        {
            this.categoryDal = categoryDal;
            this.logger = logger;
            this.unitOfWork = unitOfWork;
        }
    }
}
