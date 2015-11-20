using Microsoft.Data.Entity;
using SunLine.Manager.Entities.Core;
using SunLine.Manager.Entities.Football;

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
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase();
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<Stadium> Stadiums { get; set; }
        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<UserSession> UserSessions { get; set; }
        public virtual DbSet<ExternalClient> ExternalClients { get; set; }

        public DbSet<TEntity> GetDbSet<TEntity>() where TEntity : class
        {
            return Set<TEntity>();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {           
            base.OnModelCreating(builder);
            
            builder.Entity<User>().HasOne(m => m.Team).WithOne(x => x.User).HasForeignKey<User>(x => x.TeamId);
            builder.Entity<Team>().HasOne(m => m.Stadium).WithOne(x => x.Team).HasForeignKey<Team>(x => x.StadiumId);
            
            builder.Entity<Player>().HasOne(m => m.Team).WithMany(x => x.Players).HasForeignKey(x => x.TeamId);
            builder.Entity<Team>().HasOne(m => m.CurrentLeague).WithMany(x => x.Teams).HasForeignKey(x => x.CurrentLeagueId);
            builder.Entity<Team>().HasMany(m => m.Leagues);
        }
    }
}
