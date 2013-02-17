using BugTracker.Domain.Models;
using BugTracker.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BugTracker.Infrastructure
{
    public class ActionLogger
    {
        public static void Log(string message, string username)
        {
            var userRepo = DependencyResolver.Current.GetService<IUserRepository>();
            var user = userRepo.Users.FirstOrDefault(u =>
                u.UserName == username);

            user.LastActivity = message;
            user.LastActivityDate = DateTime.Now;

            userRepo.SaveUser(user);
        }
    }
}