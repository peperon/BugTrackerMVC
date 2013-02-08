using BugTracker.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Domain.RepositoryInterfaces
{
    public interface IProjectRepository
    {
        IQueryable<Project> Projects { get; }
        void SaveProject(Project project);
        void DeleteProject(Project project);
    }
}
