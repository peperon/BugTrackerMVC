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

        public IQueryable<Models.Error> Errors
        {
            get { return _dbContext.Errors; }
        }

        public void SaveError(Models.Error error)
        {
            if (error.ErrorId == 0)
                _dbContext.Errors.Add(error);
            _dbContext.SaveChanges();
        }

        public void DeleteError(Models.Error error)
        {
            _dbContext.Errors.Remove(error);
            _dbContext.SaveChanges();
        }
    }
}
