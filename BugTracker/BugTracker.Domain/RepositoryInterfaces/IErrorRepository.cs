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
        IQueryable<Error> Errors { get; }
        void SaveError(Error error);
        void DeleteError(Error error);
    }
}
