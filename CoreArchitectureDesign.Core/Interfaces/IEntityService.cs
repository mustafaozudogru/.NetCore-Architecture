using CoreArchitectureDesign.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CoreArchitectureDesign.Core.Interfaces
{
    public interface IEntityService<T>
    {
        Result<T> GetById(int id);

        Result<T> Get(Expression<Func<T, bool>> filter = null);

        Result<IEnumerable<T>> GetList(Expression<Func<T, bool>> filter = null);

        Result<IEnumerable<T>> GetListPaging(Expression<Func<T, bool>> filter, out int total, int index = 0, int size = 10);

        Result<T> Add(T entity);

        Result<bool> AddRange(IEnumerable<T> listEntity);

        Result<T> Update(T entity);

        Result<bool> Delete(T entity);

        Result<bool> Delete(Expression<Func<T, bool>> filter);

        Result<bool> DeleteAll(Expression<Func<T, bool>> filter = null);
    }
}
