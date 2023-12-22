using DAL.EntityFramework;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext dbContext;

        public UserRepository(ApplicationDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public void DeleteUser(long id)
        {
            var user = dbContext.Users.FirstOrDefault(x => x.Id == id);
            dbContext.Users.Remove(user);
            dbContext.SaveChanges();
        }

        public User GetUser(long id)
        {
           return dbContext.Users.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<User> GetUsers()
        {
            return dbContext.Users.ToList();
        }

        public void InsertUser(User user)
        {
            dbContext.Users.Add(user);
            dbContext.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            var userToUpdate = dbContext.Users.FirstOrDefault(x => x.Id == user.Id);
            userToUpdate.UserName = user.UserName;
            userToUpdate.Email = user.Email;
            userToUpdate.Password = user.Password;
           
            dbContext.Users.Update(userToUpdate);
            dbContext.SaveChanges();
        }
    }
}
