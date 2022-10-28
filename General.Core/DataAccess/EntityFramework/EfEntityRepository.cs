using General.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace General.Core.DataAccess.EntityFramework
{
    public class EfEntityRepository<TEntity, TContext> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext, new()
    {

        public TEntity Add(TEntity entity)
        {
            using var context = new TContext();
            entity.CreateDate = DateTime.Now;
            entity.Deleted = false;
            var addedEntity = context.Add(entity);
            addedEntity.State = EntityState.Added;
            if (context.SaveChanges() > 0)
                return entity;
            else
                return entity;
        }

        public virtual bool Any(TEntity entity)
        {
            using var context = new TContext();
            return context.Set<TEntity>().Any(x => x.Id == entity.Id);
        }

        public bool Delete(TEntity entity)
        {
            using var context = new TContext();
            entity.Deleted = true;
            entity.ModifyDate = DateTime.Now;
            var DeletedEntity = context.Update(entity);
            DeletedEntity.State = EntityState.Modified;
            if (context.SaveChanges() > 0)
                return true;
            else
                return false;
        }

        public virtual TEntity Get(Request request)
        {
            using var context = new TContext();
            return context.Set<TEntity>().FirstOrDefault(x => x.Id == request.Id);
        }

        public virtual IEnumerable<TEntity> GetList(Request request)
        {
            using var context = new TContext();
            var contexts = context.Set<TEntity>()
                .Where(x => x.Deleted != true)
                .AsQueryable();

            if (request != null && request.CreateBy > 0)
                contexts = contexts.Where(x => x.CreateBy == request.CreateBy);

            if (request != null && request.RowCount > 0)
                contexts = contexts.Skip(request.PageCount * request.RowCount)
                                   .Take(request.RowCount);

            return contexts.ToList();
        }

        public TEntity Update(TEntity entity)
        {
            using var context = new TContext();
            entity.ModifyDate = DateTime.Now;
            var updatedEntity = context.Update(entity);
            updatedEntity.State = EntityState.Modified;
            if (context.SaveChanges() > 0)
                return entity;
            else
                return entity;
        }
    }
}
