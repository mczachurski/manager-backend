using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.Data.Entity;
using SunLine.Manager.Entities;

namespace SunLine.Manager.Repositories.Infrastructure
{
    public class EntityRepository<T> where T : BaseEntity
    {
        protected DatabaseContext _databaseContext { get; private set; }

        public DbSet<T> DbSet { get; set; }

        public EntityRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            DbSet = _databaseContext.GetDbSet<T>();
        }
            
        public T Create(T entity)
        {
            entity.Version = 1;
            entity.CreationDate = DateTime.UtcNow;

            _databaseContext.Add(entity);
            return entity;
        }
            
        public void Update(T entity)
        {
            entity.Version++;
            entity.ModificationDate = DateTime.UtcNow;

            _databaseContext.Update(entity);
        }
            
        public void Delete(T entity)
        {
            _databaseContext.Remove(entity);
        }
            
        public IQueryable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }
            
        public T FindById(int id)
        {
            return DbSet.FirstOrDefault(o => o.Id == id);
        }
            
        public IQueryable<T> FindAll()
        {
            return DbSet;
        }
    }
}
