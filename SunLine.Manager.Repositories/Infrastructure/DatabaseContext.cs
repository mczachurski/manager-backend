using Microsoft.Data.Entity;
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
                //Database.AsRelational().ApplyMigrations();
                _created = true;
            }
        }            

        public DbSet<User> Users { get; set; }

        public DbSet<TEntity> GetDbSet<TEntity>() where TEntity : class
        {
            return Set<TEntity>();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
