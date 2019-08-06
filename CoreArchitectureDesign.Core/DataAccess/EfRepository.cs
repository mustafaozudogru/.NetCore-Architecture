using CoreArchitectureDesign.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CoreArchitectureDesign.Core.DataAccess
{
    public class EfRepository<TEntity> : IEntityRepository<TEntity> where TEntity : class, IEntity, new()
    {
        /// <summary>
        /// The context
        /// </summary>
        protected DbContext Context;

        /// <summary>
        /// The dbset
        /// </summary>
        protected DbSet<TEntity> Dbset;

        public EfRepository(DbContext context)
        {
            Context = context;
            Dbset = Context.Set<TEntity>();
        }

        public virtual TEntity Add(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            return Dbset.Add(entity).Entity;
        }

        public virtual void AddRange(IEnumerable<TEntity> listEntity)
        {
            if (listEntity == null)
            {
                throw new ArgumentNullException("listEntity");
            }

            foreach (var entity in listEntity)
            {
                Dbset.Add(entity);
            }
        }

        public void CustomSaveChanges()
        {
            Context.SaveChanges();
        }

        public virtual void Delete(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var entry = Context.Entry(entity);
            if (entry.State == EntityState.Detached)
                Dbset.Attach(entity);
            Dbset.Remove(entity);
        }

        public virtual void Delete(Expression<Func<TEntity, bool>> filter)
        {
            var entity = Get(filter);
            if (entity == null) return;

            var entry = Context.Entry(entity);

            if (entry.State == EntityState.Detached)
            {
                Dbset.Attach(entity);
            }

            Dbset.Remove(entity);
        }

        public virtual void DeleteAll(Expression<Func<TEntity, bool>> filter = null)
        {
            if (filter == null) return;

            var entityList = GetList(filter);

            foreach (var item in entityList)
            {
                Dbset.Remove(item);
            }
        }

        public void Dispose()
        {
            Context?.Dispose();
            GC.SuppressFinalize(this);
        }

        public virtual TEntity Get(Expression<Func<TEntity, bool>> filter = null)
        {
            return Dbset.FirstOrDefault(filter);
        }

        public virtual TEntity GetById(int id)
        {
            return Dbset.Find(id);
        }

        public virtual IQueryable<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null)
        {
            return filter == null ? Dbset : Dbset.Where(filter);
        }

        public virtual IQueryable<TEntity> GetPagingList(Expression<Func<TEntity, bool>> filter, out int total, int index, int size)
        {
            var skipCount = index * size;
            var resetSet = filter != null ? Dbset.Where(filter).AsQueryable() : Dbset.AsQueryable();

            resetSet = skipCount == 0 ? resetSet.Take(size) : resetSet.Skip(skipCount).Take(size);

            total = resetSet.Count();

            return resetSet;
        }

        public virtual TEntity Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            var updateEntity = Context.Entry(entity);
            updateEntity.State = EntityState.Modified;

            return updateEntity.Entity;
        }
    }
}
