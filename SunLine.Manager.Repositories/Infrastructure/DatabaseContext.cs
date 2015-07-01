using Microsoft.Data.Entity;
using SunLine.Manager.Entities;
using SunLine.Manager.Entities.Core;

namespace SunLine.Manager.Repositories.Infrastructure
{
    public class DatabaseContext : DbContext
    {
        private static bool _created = false;

        public DatabaseContext()
        {
            // Create the database and schema if it doesn't exist
            // This is a temporary workaround to create database until Entity Framework database migrations
            // are supported in ASP.NET 5
            if (!_created)
            {
                Database.AsRelational().ApplyMigrations();
                
                _created = true;
            }
        }            

        protected override void OnConfiguring(EntityOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=Anemone.arvixe.com,1433;Initial Catalog=manager_test;Persist Security Info=True;User ID=manager_test;Password=<PASSWORD>");
        }

        public DbSet<User> Managers { get; set; }

        public DbSet<TEntity> GetDbSet<TEntity>() where TEntity : class
        {
            return Set<TEntity>();
        }

        public void SetModifiedEntityState(BaseEntity entity)
        {            
            Entry(entity).State = EntityState.Modified;
        }

        public bool IsDetachedentityState(BaseEntity entity)
        {
            return Entry(entity).State == EntityState.Detached;
        }

        public bool IsNewEntity(BaseEntity entity)
        {
            return Entry(entity).State == EntityState.Added;
        }

        public virtual void Commit()
        {
            SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            builder.Entity<User>(x => {
                x.Key(e => e.Id);
                x.Property(e => e.Id).ForSqlServer().UseIdentity();
                x.Property(e => e.Id).ForSqlServer().UseSequence();
            });
        }
    }
}
