using BugTracker.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BugTracker.Models;
using BugTracker.Domain.Models;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

namespace BugTracker.Controllers
{
    [Authorize]
    public class ReportsController : Controller
    {
        private IErrorRepository _errorRepo;
        private IProjectRepository _projectRepo;
        private IUserRepository _userRepo;

        public ReportsController(IErrorRepository errorRepo, IProjectRepository projectRepo, IUserRepository userRepo)
        {
            _errorRepo = errorRepo;
            _projectRepo = projectRepo;
            _userRepo = userRepo;
        }

        public ActionResult UsersActivity()
        {
            var model = from user in _userRepo.Users.ToList()
                        select new UserActivityModel
                        {
                           UserName = user.UserName,
                           Name = user.FirstName + " " + user.LastName,
                           NumberOfErrors = user.Errors.Count(err => err.State != (int)ErrorState.Deleted),
                           NumberOfProjects = user.Errors.Select(err => err.ProjectId).Distinct().Count(),
                           ActivityDate = user.LastActivityDate,
                           LastAction = user.LastActivity,
                        };
            return View(model);
        }

        public ActionResult AllActiveErrors()
        {
            var model = ActiveErrors();
            return View(model);
        }

        public ActionResult AllActiveErrorsForProject()
        {
            var model = _projectRepo.Projects.ToList()
                .Select(p => new SelectListItem { Text = p.ProjectName, Value = p.ProjectId.ToString() });
            return View(model);
        }

        public ActionResult Errors([DataSourceRequest] DataSourceRequest request, int projectId)
        {
            var model = ActiveErrors(projectId);
            return Json(model.ToDataSourceResult(request));
        }

        private void FillViewBag()
        {
            ViewBag.Projects = _projectRepo.Projects.ToList()
                .Select(project =>
                    new SelectListItem
                    {
                        Text = project.ProjectName,
                        Value = project.ProjectId.ToString(),
                    });
        }

        private IEnumerable<AllActiveErrorsReportModel> ActiveErrors(int projectId = 0)
        {
            var model = from error in _errorRepo.Errors.ToList()
                        where error.State != (int)ErrorState.Closed && error.State != (int)ErrorState.Deleted
                        where projectId == 0 || error.ProjectId == projectId
                        select new AllActiveErrorsReportModel
                        {
                            ShortDescription = (error.Description.Length <= 50) ?
                                        new string(error.Description.Take(50).ToArray()) :
                                        new string(error.Description.Take(50).ToArray()) + "...",
                            Owner = error.User.FirstName + " " + error.User.LastName,
                            Priority = ((ErrorPriority)error.Priority).ToString(),
                            Project = error.Project.ProjectName,
                            State = ((ErrorState)error.State).ToString(),
                        };

            return model;
        }
    }
}
