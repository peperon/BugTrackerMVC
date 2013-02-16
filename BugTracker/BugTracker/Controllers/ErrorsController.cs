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

namespace BugTracker.Controllers
{
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

        //
        // GET: /Errors/

        public ActionResult Index()
        {
            var model = _projectRepo.Projects.ToList()
                .Select(p => new SelectListItem { Text = p.ProjectName, Value = p.ProjectId.ToString() });
            return View(model);
        }

        public ActionResult Errors([DataSourceRequest] DataSourceRequest request, int projectId)
        {
            var errors = from error in _errorRepo.Errors.ToList()
                         where error.ProjectId == projectId
                         select new ErrorModel
                         {
                             DateCreation = error.DateCreation,
                             Priority = error.Priority.ToString(),
                             UserName = error.User.UserName,
                         };
            return Json(errors.ToDataSourceResult(request));
        }

        //
        // GET: /Errors/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Errors/Create

        public ActionResult Create(int id)
        {
            FillViewBag(id);
            return View();
        }

        private void FillViewBag(int id)
        {
            ViewBag.Priorities = new List<SelectListItem>
            {
                new SelectListItem { Text = "Low", Value = ((int)ErrorPriority.Low).ToString() },
                new SelectListItem { Text = "Normal", Value = ((int)ErrorPriority.Normal).ToString() },
                new SelectListItem { Text = "Hight", Value = ((int)ErrorPriority.Hight).ToString() },
                new SelectListItem { Text = "Critical", Value = ((int)ErrorPriority.Critical).ToString() },
            };

            ViewBag.Projects = _projectRepo.Projects.ToList()
                .Select(project =>
                    new SelectListItem { Text = project.ProjectName, Value = project.ProjectId.ToString(), Selected = project.ProjectId == id }
                    );

        }

        //
        // POST: /Errors/Create

        [HttpPost]
        public ActionResult Create(Error error)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    FillViewBag(error.ProjectId);
                    return View(error);
                }

                // TODO: Add insert logic here
                var currentUser = _userRepo.Users.FirstOrDefault(user => user.UserName == User.Identity.Name);
                if (currentUser == null)
                    throw new Exception();

                error.State = (int)ErrorState.New;
                error.UserId = currentUser.UserId;
                error.DateCreation = DateTime.Now;

                _errorRepo.SaveError(error);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Errors/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Errors/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Errors/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Errors/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
