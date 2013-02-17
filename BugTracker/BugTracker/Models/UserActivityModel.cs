using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BugTracker.Models
{
    public class UserActivityModel
    {
        [Display(Name="User name")]
        public string UserName { get; set; }

        public string Name { get; set; }

        [Display(Name="Number of projects")]
        public int NumberOfProjects { get; set; }

        [Display(Name="Number of errors")]
        public int NumberOfErrors { get; set; }

        [Display(Name="Activity date")]
        public DateTime? ActivityDate { get; set; }

        [Display(Name="Last action")]
        public string LastAction { get; set; }
    }
}