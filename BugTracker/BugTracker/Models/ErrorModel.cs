using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker.Models
{
    public class ErrorModel
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Priority { get; set; }

        public DateTime DateCreation { get; set; }
    }
}