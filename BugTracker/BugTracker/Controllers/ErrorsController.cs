using BugTracker.Domain.RepositoryInterfaces;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BugTracker.Domain.Models;
using BugTracker.Models;
using BugTracker.Infrastructure;

namespace BugTracker.Controllers
{
    [Authorize]
    public class ErrorsController : Controller
    {
        private IProjectRepository _projectRepo;
        private IErrorRepository _errorRepo;
        private IUserRepository _userRepo;

        public ErrorsController(IProjectRepository projectRepo, IErrorRepository errorRepo, IUserRepository userRepo)
        {
            _projectRepo = projectRepo;
            _errorRepo = errorRepo;
            _userRepo = userRepo;
        }

        public ActionResult Index()
        {
            var model = _projectRepo.Projects.ToList()
                .Select(p => new SelectListItem { Text = p.ProjectName, Value = p.ProjectId.ToString() });
            return View(model);
        }

        public ActionResult Errors([DataSourceRequest] DataSourceRequest request, int projectId)
        {
            var errors = from error in _errorRepo.Errors.ToList()
                         where error.ProjectId == projectId && error.State != (int)ErrorState.Deleted
                         select new ErrorModel
                         {
                             Id = error.ErrorId,
                             DateCreation = error.DateCreation,
                             Priority = ((ErrorPriority)error.Priority).ToString(),
                             UserName = error.User.UserName,
                         };
            return Json(errors.ToDataSourceResult(request));
        }

        public ActionResult Details(int id)
        {
            var error = _errorRepo.Errors.FirstOrDefault(err => err.ErrorId == id);
            return View(error);
        }

        public ActionResult Create(int id)
        {
            FillViewBag(id);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Error error)
        {
            if (!ModelState.IsValid)
            {
                FillViewBag(error.ProjectId);
                return View(error);
            }

            var currentUser = _userRepo.Users.FirstOrDefault(user => user.UserName == User.Identity.Name);
            if (currentUser == null)
                throw new Exception();

            error.State = (int)ErrorState.New;
            error.UserId = currentUser.UserId;
            error.DateCreation = DateTime.Now;

            _errorRepo.SaveError(error);
            ActionLogger.Log("Created error #" + error.ErrorId, User.Identity.Name);

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var error = (from errSelector in _errorRepo.Errors
                         where errSelector.ErrorId == id
                         select new ErrorEditModel
                         {
                             Id = errSelector.ErrorId,
                             DateCreation = errSelector.DateCreation,
                             Description = errSelector.Description,
                             State = errSelector.State,
                             Priority = errSelector.Priority,
                             Owner = errSelector.User.UserName,
                             Project = errSelector.Project.ProjectName
                         }).FirstOrDefault();

            FillViewBagWithEnums();
            return View(error);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ErrorEditModel model)
        {
            if (!ModelState.IsValid)
            {
                FillViewBagWithEnums();
                return View(model);
            }

            var error = _errorRepo.Errors.FirstOrDefault(err => err.ErrorId == model.Id);

            error.Description = model.Description;
            error.Priority = model.Priority;
            error.State = model.State;

            _errorRepo.SaveError(error);
            ActionLogger.Log("Edited error #" + error.ErrorId, User.Identity.Name);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var error = _errorRepo.Errors.FirstOrDefault(err => err.ErrorId == id);
            error.State = (int)ErrorState.Deleted;
            _errorRepo.SaveError(error);

            ActionLogger.Log("Deleted error #" + id, User.Identity.Name);

            return RedirectToAction("Index");
        }

        private void FillViewBag(int id)
        {
            ViewBag.Projects = _projectRepo.Projects.ToList()
                .Select(project =>
                    new SelectListItem
                    {
                        Text = project.ProjectName,
                        Value = project.ProjectId.ToString(),
                        Selected = project.ProjectId == id
                    });
            FillViewBagWithEnums();
        }

        private void FillViewBagWithEnums()
        {
            ViewBag.Priorities = new List<SelectListItem>
            {
                new SelectListItem { Text = "Low", Value = ((int)ErrorPriority.Low).ToString() },
                new SelectListItem { Text = "Normal", Value = ((int)ErrorPriority.Normal).ToString() },
                new SelectListItem { Text = "Hight", Value = ((int)ErrorPriority.Hight).ToString() },
                new SelectListItem { Text = "Critical", Value = ((int)ErrorPriority.Critical).ToString() },
            };

            ViewBag.States = new List<SelectListItem>
            {
                new SelectListItem { Text = "New", Value = ((int)ErrorState.New).ToString() },
                new SelectListItem { Text = "In Progress", Value = ((int)ErrorState.InProgress).ToString() },
                new SelectListItem { Text = "Fixed", Value = ((int)ErrorState.Fixed).ToString() },
                new SelectListItem { Text = "Closed", Value = ((int)ErrorState.Closed).ToString() },
            };
        }
    }
}
