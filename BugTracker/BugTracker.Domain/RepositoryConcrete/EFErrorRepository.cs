using BugTracker.Domain.Models;
using BugTracker.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Domain.RepositoryConcrete
{
    public class EFErrorRepository : IErrorRepository
    {
        private EFDbContext _dbContext;

        public EFErrorRepository()
        {
            _dbContext = new EFDbContext();
        }

        public void SaveError(Models.Error error)
        {
            if (error.ErrorId == 0)
                _dbContext.Errors.Add(error);
            else if (_dbContext.Entry<Error>(error).State != System.Data.EntityState.Modified)
                _dbContext.Entry<Error>(error).State = System.Data.EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void DeleteError(Models.Error error)
        {
            _dbContext.Errors.Remove(error);
            _dbContext.SaveChanges();
        }

        public IEnumerable<Error> GetErrors()
        {
            return _dbContext.Errors.ToList();
        }

        public Error GetError(int id)
        {
            return _dbContext.Errors.FirstOrDefault(error => error.ErrorId == id);
        }


        public IEnumerable<Error> GetActiveErrorsForProject(int projectId)
        {
            return from error in _dbContext.Errors.ToList()
                   where error.State != (int)ErrorState.Closed && error.State != (int)ErrorState.Deleted
                   where projectId == 0 || error.ProjectId == projectId
                   select error;
        }
    }
}
