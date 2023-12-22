using DAL.EntityFramework;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repository
{
    public class LoginRepository : ILoginRepository
    {
        private readonly ApplicationDbContext dbContext;

        public LoginRepository(ApplicationDbContext _dbContext)
        {
            dbContext = _dbContext;
        }
        public UserLogin AuthenticateUser(string email, string password)
        {
            return dbContext.UserLogin.Where(x => x.Email == email && x.Password == password).FirstOrDefault();
        }
    }
}
