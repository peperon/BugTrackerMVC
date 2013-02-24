using BugTracker.Domain.Models;
using BugTracker.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BugTracker.Controllers
{
    [Authorize(Roles="Admin")]
    public class ProjectsController : Controller
    {
        private IProjectRepository _projectRepo;

        public ProjectsController(IProjectRepository projectRepo)
        {
            _projectRepo = projectRepo;
        }

        //
        // GET: /Projects/

        public ActionResult Index()
        {
            return View(_projectRepo.Projects);
        }

        //
        // GET: /Projects/Details/5

        public ActionResult Details(int id)
        {

            return View();
        }

        //
        // GET: /Projects/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Projects/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Project project)
        {
            try
            {
                // TODO: Add insert logic here
                return SaveProject(project);
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Projects/Edit/5

        public ActionResult Edit(int id)
        {
            var project = _projectRepo.Projects.FirstOrDefault(p => p.ProjectId == id);
            return View(project);
        }

        //
        // POST: /Projects/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Project project)
        {
            try
            {
                // TODO: Add update logic here
                return SaveProject(project);
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Projects/Delete/5

        public ActionResult Delete(int id)
        {
            var project = _projectRepo.Projects.FirstOrDefault(p => p.ProjectId == id);

            return View(project);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteProject(int id)
        {
            var project = _projectRepo.Projects.First(p => p.ProjectId == id);

            _projectRepo.DeleteProject(project);
            return RedirectToAction("Index");
        }

        //
        // POST: /Projects/Delete/5

        private ActionResult SaveProject(Project project)
        {
            var existingProject = _projectRepo.Projects.FirstOrDefault(p => p.ProjectName == project.ProjectName);
            if (existingProject != null && project.ProjectId != existingProject.ProjectId)
                ModelState.AddModelError("ProjectName", "Project name is already used");
            if (!ModelState.IsValid)
                return View(project);

            if (existingProject != null)
            {
                existingProject.Description = project.Description;
                _projectRepo.SaveProject(existingProject);
            }
            else
                _projectRepo.SaveProject(project);
            return RedirectToAction("Index");
        }
    }
}
