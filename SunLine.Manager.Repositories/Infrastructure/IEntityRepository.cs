using System;
using System.Linq;
using System.Linq.Expressions;
using SunLine.Manager.Entities;

namespace SunLine.Manager.Repositories.Infrastructure
{
    public interface IEntityRepository<T> where T : BaseEntity
    {
        T Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        IQueryable<T> Find(Expression<Func<T, bool>> predicate);
        IQueryable<T> FindAll();
        T FindById(int id);
    }
}