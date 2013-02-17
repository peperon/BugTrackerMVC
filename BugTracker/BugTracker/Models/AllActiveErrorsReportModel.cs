using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker.Models
{
    public class AllActiveErrorsReportModel
    {
        public string ShortDescription { get; set; }

        public string Priority { get; set; }

        public string Owner { get; set; }

        public string Project { get; set; }

        public string State { get; set; }
    }
}