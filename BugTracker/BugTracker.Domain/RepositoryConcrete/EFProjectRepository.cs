using BugTracker.Domain.Models;
using BugTracker.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Domain.RepositoryConcrete
{
    public class EFProjectRepository : IProjectRepository
    {
        private EFDbContext _dbContext;

        public EFProjectRepository()
        {
            _dbContext = new EFDbContext();
        }

        public void SaveProject(Models.Project project)
        {
            if (project.ProjectId == 0)
                _dbContext.Projects.Add(project);
            else if (_dbContext.Entry<Project>(project).State != System.Data.EntityState.Modified)
                _dbContext.Entry<Project>(project).State = System.Data.EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void DeleteProject(Models.Project project)
        {
            _dbContext.Projects.Remove(project);
            _dbContext.SaveChanges();
        }

        public IEnumerable<Project> GetProjects()
        {
            return _dbContext.Projects.ToList();
        }


        public Project GetProject(int id)
        {
            return _dbContext.Projects.FirstOrDefault(project => project.ProjectId == id);
        }


        public Project GetProject(string projectName)
        {
            return _dbContext.Projects.FirstOrDefault(project => project.ProjectName == projectName);
        }
    }
}
