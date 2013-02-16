using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Domain.Models
{
    public class Project
    {
        public int ProjectId { get; set; }

        [Required]
        [Display(Name="Project name")]
        public string ProjectName { get; set; }
        public string Description { get; set; }

        public virtual List<Error> Errors { get; set; }
    }
}
