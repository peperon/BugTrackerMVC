using BugTracker.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Domain.RepositoryInterfaces
{
    public interface IUserRepository
    {
        void SaveUser(User user);
        void DeleteUser(User user);

        User GetUser(string name, string password);
        User GetUser(string name);
        User GetUser(int id);
        IEnumerable<User> GetUsers();
    }
}
