using BugTracker.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Domain.RepositoryInterfaces
{
    public interface IErrorRepository
    {
        void SaveError(Error error);
        void DeleteError(Error error);

        IEnumerable<Error> GetErrors();
        Error GetError(int id);
        IEnumerable<Error> GetActiveErrorsForProject(int projectId);
    }
}
