using System.Linq;
using SunLine.Manager.Entities.Core;
using SunLine.Manager.Repositories.Infrastructure;

namespace SunLine.Manager.Repositories.Core
{   
    public class UserRepository : EntityRepository<User>, IUserRepository
    {
        public UserRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
            
        }
        
        public User FindByEmail(string email)
        {
            return _databaseContext.Users.FirstOrDefault(x => x.Email == email);
        }
    }
}
