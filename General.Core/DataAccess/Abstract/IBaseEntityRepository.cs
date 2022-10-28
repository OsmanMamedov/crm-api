using General.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace General.Core.DataAccess
{
    public interface IBaseEntityRepository<T> : IBaseRepository where T : class, IBaseEntity, new()
    {
        T Add(T entity);
        T Update(T entity);
        bool Delete(T entity);
    }
}
