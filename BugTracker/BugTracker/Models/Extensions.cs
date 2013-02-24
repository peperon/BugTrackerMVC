using BugTracker.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BugTracker.Models
{
    public static class Extensions
    {
        public static ErrorEditModel ToErrorEdit(this Error error)
        {
            return new ErrorEditModel
            {
                Id = error.ErrorId,
                DateCreation = error.DateCreation,
                Description = error.Description,
                State = error.State,
                Priority = error.Priority,
                Owner = error.User.UserName,
                Project = error.Project.ProjectName
            };
        }

        public static ErrorModel ToErrorModel(this Error error)
        {
            return new ErrorModel
            {
                Id = error.ErrorId,
                DateCreation = error.DateCreation,
                Priority = ((ErrorPriority)error.Priority).ToString(),
                UserName = (error.UserId == null) ? "no author" : error.User.UserName,
            };
        }

        public static AllActiveErrorsReportModel ToAllActiveErrorModel(this Error error)
        {
            return new AllActiveErrorsReportModel
            {
                ShortDescription = (error.Description.Length <= 50) ?
                    new string(error.Description.Take(50).ToArray()) :
                    new string(error.Description.Take(50).ToArray()) + "...",
                Owner = (error.UserId == null) ? "no author" : error.User.FirstName + " " + error.User.LastName,
                Priority = ((ErrorPriority)error.Priority).ToString(),
                Project = error.Project.ProjectName,
                State = ((ErrorState)error.State).ToString(),
            };
        }

        public static SelectListItem ToSelectListItem(this Project project)
        {
            return new SelectListItem { Text = project.ProjectName, Value = project.ProjectId.ToString() };
        }

        public static UserActivityModel ToUserActivity(this User user)
        {
            return new UserActivityModel
            {
                UserName = user.UserName,
                Name = user.FirstName + " " + user.LastName,
                NumberOfErrors = user.Errors.Count(err => err.State != (int)ErrorState.Deleted),
                NumberOfProjects = user.Errors.Select(err => err.ProjectId).Distinct().Count(),
                ActivityDate = user.LastActivityDate,
                LastAction = user.LastActivity,
            };
        }
    }
}