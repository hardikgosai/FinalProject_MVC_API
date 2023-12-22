using DAL.EntityFramework;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repository
{
    public class UserProfileRepository : IUserProfileRepository
    {
        private readonly ApplicationDbContext dbContext;

        public UserProfileRepository(ApplicationDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public UserProfile GetUserProfile(long id)
        {
            return dbContext.UserProfiles.FirstOrDefault(x => x.Id == id);
        }
    }
}
