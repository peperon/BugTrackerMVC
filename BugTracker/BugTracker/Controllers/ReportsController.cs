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
            var model = _userRepo.GetUsers().Select(user => user.ToUserActivity());
            return View(model);
        }

        public ActionResult AllActiveErrors()
        {
            var model = ActiveErrors();
            return View(model);
        }

        public ActionResult AllActiveErrorsForProject()
        {
            var model = _projectRepo.GetProjects().Select(project => project.ToSelectListItem());
            return View(model);
        }

        public ActionResult Errors([DataSourceRequest] DataSourceRequest request, int projectId)
        {
            var model = ActiveErrors(projectId);
            return Json(model.ToDataSourceResult(request));
        }

        private void FillViewBag()
        {
            ViewBag.Projects = _projectRepo.GetProjects()
                .Select(project => project.ToSelectListItem());
        }

        private IEnumerable<AllActiveErrorsReportModel> ActiveErrors(int projectId = 0)
        {
            var model = _errorRepo.GetActiveErrorsForProject(projectId).Select(error => error.ToAllActiveErrorModel());
            return model;
        }
    }
}
