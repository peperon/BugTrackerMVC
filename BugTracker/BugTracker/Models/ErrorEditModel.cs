using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BugTracker.Models
{
    public class ErrorEditModel
    {
        public int Id { get; set; }
        public DateTime DateCreation { get; set; }

        [Required]
        public string Description { get; set; }
        public int Priority { get; set; }
        public int State { get; set; }

        public string Owner { get; set; }
        public string Project { get; set; }
    }
}