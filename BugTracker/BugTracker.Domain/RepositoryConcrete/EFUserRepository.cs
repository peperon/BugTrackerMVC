using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugTracker.Domain.RepositoryInterfaces;
using BugTracker.Domain.Models;

namespace BugTracker.Domain.RepositoryConcrete
{
    public class EFUserRepository : IUserRepository
    {
        private EFDbContext _dbContext;

        public EFUserRepository()
        {
            _dbContext = new EFDbContext();
        }

        public void SaveUser(User user)
        {
            if (user.UserId == 0)
                _dbContext.Users.Add(user);
            else if (_dbContext.Entry<User>(user).State != System.Data.EntityState.Modified)
                _dbContext.Entry<User>(user).State = System.Data.EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void DeleteUser(User user)
        {
            _dbContext.Users.Remove(user);
            _dbContext.SaveChanges();
        }

        public User GetUser(string name, string password)
        {
            return _dbContext.Users.FirstOrDefault(u => u.UserName == name && u.Password == password);
        }


        public User GetUser(string name)
        {
            return _dbContext.Users.FirstOrDefault(u => u.UserName == name);
        }


        public User GetUser(int id)
        {
            return _dbContext.Users.FirstOrDefault(u => u.UserId == id);
        }


        public IEnumerable<User> GetUsers()
        {
            return _dbContext.Users.Where(user => user.Role != (int)UserRole.Admin).ToList();
        }
    }
}
