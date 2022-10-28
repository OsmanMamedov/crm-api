using General.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace General.Core.DataAccess.EntityFramework
{
    public class EfBaseEntityRepository<TEntity, TContext> : IBaseEntityRepository<TEntity>
        where TEntity : class, IBaseEntity, new()
        where TContext : DbContext, new()
    {

        public TEntity Add(TEntity entity)
        {
            using var context = new TContext();
            var addedEntity = context.Add(entity);
            addedEntity.State = EntityState.Added;
            if (context.SaveChanges() > 0)
                return entity;
            else
                return entity;
        }

        public bool Delete(TEntity entity)
        {
            using var context = new TContext();
            var DeletedEntity = context.Remove(entity);
            DeletedEntity.State = EntityState.Deleted;
            if (context.SaveChanges() > 0)
                return true;
            else
                return false;
        }

        public TEntity Update(TEntity entity)
        {
            using var context = new TContext();
            var updatedEntity = context.Update(entity);
            updatedEntity.State = EntityState.Modified;
            if (context.SaveChanges() > 0)
                return entity;
            else
                return entity;
        }
    }
}
