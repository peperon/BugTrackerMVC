using BugTracker.Domain.Models;
using BugTracker.Domain.RepositoryInterfaces;
using BugTracker.Domain.Utilities;
using BugTracker.Infrastructure;
using BugTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BugTracker.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private IUserRepository _userRepo;

        public AccountController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            var model = new LoginModel();
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]        
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var password = PasswordUtility.HashPassword(model.Password);
            var user = _userRepo.Users.FirstOrDefault(u => u.UserName == model.UserName && u.Password == password);
            if (user == null)
            {
                ModelState.AddModelError("PasswordUsernameError", "Invalid Username or Password!");
                return View(model);
            }            

            ApplicationSecurity.AddAuthenticationCookie(user.UserName, ((UserRole)user.Role).ToString(), model.RememberMe);          
            ActionLogger.Log("Login", model.UserName);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            ApplicationSecurity.SignOut();
            return RedirectToAction("Login", "Account");
        }


        public ActionResult Create()
        {
            return View();                 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            var existingUser = _userRepo.Users.FirstOrDefault(u => u.UserName == user.UserName);
            if (existingUser != null)
                ModelState.AddModelError("UserName", "User name is already used");
            if (!ModelState.IsValid)
                return View(user);                
            
            user.Role = (int)UserRole.QualityAssurance;
            user.Password = PasswordUtility.HashPassword(user.Password);
            _userRepo.SaveUser(user);

            return RedirectToAction("Details", new { id = user.UserId });
        }

        public ActionResult Details(int id)
        {
            var user = _userRepo.Users.FirstOrDefault(u => u.UserId == id);
            return View(user);
        }

        public ActionResult Edit(int id)
        {
            var user = _userRepo.Users.FirstOrDefault(u => u.UserId == id);
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, User user)
        {
            if (!ModelState.IsValid)
                return View(user);

            var userDb = _userRepo.Users.FirstOrDefault(u => u.UserId == id);

            userDb.FirstName = user.FirstName;
            userDb.LastActivity = user.LastName;
            userDb.Email = user.Email;
            userDb.Phone = user.Phone;
            if (userDb.Password != user.Password)
                userDb.Password = PasswordUtility.HashPassword(user.Password);

            _userRepo.SaveUser(userDb);

            return RedirectToAction("Index");
        }

        public ActionResult Index()
        {
            var users = _userRepo.Users.Where(user => user.Role != (int)UserRole.Admin);
            return View(users);
        }

        public ActionResult Delete(int id)
        {
            var user = _userRepo.Users.FirstOrDefault(u => u.UserId == id);
            foreach (var error in user.Errors)
                error.UserId = null;

            _userRepo.DeleteUser(user);

            return RedirectToAction("Index");
        }
    }
}
