using General.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace General.Core.DataAccess
{
    public interface IEntityRepository<T> : IBaseRepository where T : class, IEntity, new()
    {
        T Get(Request request);
        IEnumerable<T> GetList(Request request);
        T Add(T entity);
        T Update(T entity);
        bool Delete(T entity);
        bool Any(T entity);
    }
}
