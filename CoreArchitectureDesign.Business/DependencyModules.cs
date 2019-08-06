using CoreArchitectureDesign.Business.Abstractions;
using CoreArchitectureDesign.Core.Interfaces;
using CoreArchitectureDesign.Core.Utilities;
using CoreArchitectureDesign.Data.Concrete.EntityFramework;
using CoreArchitectureDesign.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreArchitectureDesign.Business
{
    public static class BusinessServiceModule
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IEventLog, EventLogService>();
            services.AddScoped<ILogger, EventLogService>();

            return services;
        }
    }

    public static class DataLayerServiceModule
    {
        public static IServiceCollection AddDataLayerServices(this IServiceCollection services)
        {
            services.AddDbContext<NorthwndContext>(options => options.UseSqlServer(Utility.SqlDbConnStr));
            services.AddScoped<ICategoryDal, EfCategoryDal>();
            services.AddScoped<IEventLogDal, EfEventLogDal>();

            return services;
        }
    }
}
