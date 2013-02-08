using BugTracker.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Domain.RepositoryInterfaces
{
    public interface IUsersRepository
    {
        IQueryable<User> Users { get; }
        void SaveUser(User user);
        void DeleteUser(User user);
    }
}
