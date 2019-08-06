using CoreArchitectureDesign.Core.Common;
using CoreArchitectureDesign.Core.Enums;
using CoreArchitectureDesign.Core.Helpers;
using CoreArchitectureDesign.Core.Interfaces;
using CoreArchitectureDesign.Core.Log;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CoreArchitectureDesign.Core.Services
{
    public abstract class EntityService<T> : IEntityService<T>, IDisposable where T : class, IEntity, new()
    {
        private string ClassFullName => typeof(T).FullName;

        private readonly IUnitOfWork unitOfWork;
        private readonly IEntityRepository<T> repository;
        private readonly ILogger logger;

        protected EntityService(IEntityRepository<T> repository, IUnitOfWork unitOfWork, ILogger logger = null)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }


        public virtual Result<T> Add(T entity)
        {
            var result = new Result<T>();

            try
            {
                this.logger?.LogInformation(LoggingEvents.GetItem, $"Add()EntityService ClassName => {ClassFullName}");
                result.ResultObject = this.repository.Add(entity);
                this.unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                this.logger?.LogError(LoggingEvents.GetItem, ex.WriteLog("Add", ClassFullName));
                result.ResultCode = (int)ResultStatusCodes.InternalServerError;
                result.ResultMessage = ex.Message;
                result.ResultInnerMessage = ex.InnerException?.ToString();
                result.ResultStatus = false;
            }

            return result;
        }

        public virtual Result<bool> AddRange(IEnumerable<T> listEntity)
        {
            var result = new Result<bool>();

            try
            {
                this.logger?.LogInformation(LoggingEvents.GetItem, $"AddRange()EntityService ClassName => {ClassFullName}");
                this.repository.AddRange(listEntity);
                this.unitOfWork.Commit();
                result.ResultObject = true;
            }
            catch (Exception ex)
            {
                this.logger?.LogError(LoggingEvents.GetItem, ex.WriteLog("AddRange", ClassFullName));
                result.ResultCode = (int)ResultStatusCodes.InternalServerError;
                result.ResultMessage = ex.Message;
                result.ResultInnerMessage = ex.InnerException?.ToString();
                result.ResultStatus = false;
            }

            return result;
        }

        public virtual Result<bool> Delete(T entity)
        {
            var result = new Result<bool>();

            try
            {
                this.logger?.LogInformation(LoggingEvents.GetItem, $"Delete(entity)EntityService ClassName => {ClassFullName}");
                this.repository.Delete(entity);
                this.unitOfWork.Commit();
                result.ResultObject = true;
            }
            catch (Exception ex)
            {
                this.logger?.LogError(LoggingEvents.GetItem, ex.WriteLog("Delete(entity)", ClassFullName));
                result.ResultCode = (int)ResultStatusCodes.InternalServerError;
                result.ResultMessage = ex.Message;
                result.ResultInnerMessage = ex.InnerException?.ToString();
                result.ResultStatus = false;
            }

            return result;
        }

        public virtual Result<bool> Delete(Expression<Func<T, bool>> filter)
        {
            var result = new Result<bool>();

            try
            {
                this.logger?.LogInformation(LoggingEvents.GetItem, $"Delete(filter)EntityService ClassName => {ClassFullName}");
                this.repository.Delete(filter);
                this.unitOfWork.Commit();
                result.ResultObject = true;
            }
            catch (Exception ex)
            {
                this.logger?.LogError(LoggingEvents.GetItem, ex.WriteLog("Delete(filter)", ClassFullName));
                result.ResultCode = (int)ResultStatusCodes.InternalServerError;
                result.ResultMessage = ex.Message;
                result.ResultInnerMessage = ex.InnerException?.ToString();
                result.ResultStatus = false;
            }

            return result;
        }

        public virtual Result<bool> DeleteAll(Expression<Func<T, bool>> filter = null)
        {
            var result = new Result<bool>();

            try
            {
                this.logger?.LogInformation(LoggingEvents.GetItem, $"DeleteAll(filter)EntityService ClassName => {ClassFullName}");
                this.repository.DeleteAll(filter);
                this.unitOfWork.Commit();
                result.ResultObject = true;
            }
            catch (Exception ex)
            {
                this.logger?.LogError(LoggingEvents.GetItem, ex.WriteLog("DeleteAll(filter)", ClassFullName));
                result.ResultCode = (int)ResultStatusCodes.InternalServerError;
                result.ResultMessage = ex.Message;
                result.ResultInnerMessage = ex.InnerException?.ToString();
                result.ResultStatus = false;
            }

            return result;
        }

        public void Dispose()
        {
            this.repository?.Dispose();
            GC.SuppressFinalize(this);
        }

        public virtual Result<T> Get(Expression<Func<T, bool>> filter = null)
        {
            var result = new Result<T>();

            try
            {
                this.logger?.LogInformation(LoggingEvents.GetItem, $"Get()EntityService ClassName => {ClassFullName}");
                result.ResultObject = this.repository.Get();
            }
            catch (Exception ex)
            {
                this.logger?.LogError(LoggingEvents.GetItem, ex.WriteLog("Get", ClassFullName));
                result.ResultCode = (int)ResultStatusCodes.InternalServerError;
                result.ResultMessage = ex.Message;
                result.ResultInnerMessage = ex.InnerException?.ToString();
                result.ResultStatus = false;
            }

            return result;
        }

        public virtual Result<T> GetById(int id)
        {
            var result = new Result<T>();

            try
            {
                this.logger?.LogInformation(LoggingEvents.GetItem, $"GetById()EntityService Id => {id}");
                result.ResultObject = this.repository.GetById(id);
            }
            catch (Exception ex)
            {
                this.logger?.LogError(LoggingEvents.GetItem, ex.WriteLog("GetById", ClassFullName));
                result.ResultCode = (int)ResultStatusCodes.InternalServerError;
                result.ResultMessage = ex.Message;
                result.ResultInnerMessage = ex.InnerException?.ToString();
                result.ResultStatus = false;
            }

            return result;
        }

        public virtual Result<IEnumerable<T>> GetList(Expression<Func<T, bool>> filter = null)
        {
            var result = new Result<IEnumerable<T>>();

            try
            {
                this.logger?.LogInformation(LoggingEvents.GetItem, $"GetList()EntityService ClassName => {ClassFullName}");
                result.ResultObject = this.repository.GetList(filter).ToList();
            }
            catch (Exception ex)
            {
                this.logger?.LogError(LoggingEvents.GetItem, ex.WriteLog("GetList", ClassFullName));
                result.ResultCode = (int)ResultStatusCodes.InternalServerError;
                result.ResultMessage = ex.Message;
                result.ResultInnerMessage = ex.InnerException?.ToString();
                result.ResultStatus = false;
            }

            return result;
        }

        public virtual Result<IEnumerable<T>> GetListPaging(Expression<Func<T, bool>> filter, out int total, int index = 0, int size = 10)
        {
            var result = new Result<IEnumerable<T>>();

            try
            {
                this.logger?.LogInformation(LoggingEvents.GetItem, $"GetListPaging()EntityService ClassName => {ClassFullName}");
                result.ResultObject = this.repository.GetPagingList(filter, out total, index, size).ToList();
            }
            catch (Exception ex)
            {
                total = 0;
                this.logger?.LogError(LoggingEvents.GetItem, ex.WriteLog("GetListPaging", ClassFullName));
                result.ResultCode = (int)ResultStatusCodes.InternalServerError;
                result.ResultMessage = ex.Message;
                result.ResultInnerMessage = ex.InnerException?.ToString();
                result.ResultStatus = false;
            }

            return result;
        }

        public Result<T> Update(T entity)
        {
            var result = new Result<T>();

            try
            {
                this.logger?.LogInformation(LoggingEvents.GetItem, $"Update()EntityService ClassName => {ClassFullName}");
                result.ResultObject = this.repository.Update(entity);
                this.unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                this.logger?.LogError(LoggingEvents.GetItem, ex.WriteLog("Update", ClassFullName));
                result.ResultCode = (int)ResultStatusCodes.InternalServerError;
                result.ResultMessage = ex.Message;
                result.ResultInnerMessage = ex.InnerException?.ToString();
                result.ResultStatus = false;
            }

            return result;
        }
    }
}
