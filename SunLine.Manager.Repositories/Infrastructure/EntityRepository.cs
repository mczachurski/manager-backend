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

            DbSet.Add(entity);
            return entity;
        }
            
        public void Update(T entity)
        {
            if(_databaseContext.IsNewEntity(entity))
            {
                return;
            }

            entity.Version++;
            entity.ModificationDate = DateTime.UtcNow;

            DbSet.Attach(entity);
            _databaseContext.SetModifiedEntityState(entity);
        }
            
        public void Delete(T entity)
        {
            if (_databaseContext.IsDetachedentityState(entity))
            {
                DbSet.Attach(entity);
            }
            DbSet.Remove(entity);
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
