using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Domain.Models
{
    public class Error
    {
        public int ErrorId { get; set; }

        [Display(Name="Date found")]
        public DateTime DateCreation { get; set; }

        [Required]
        public string Description { get; set; }
        public int Priority { get; set; }
        public int State { get; set; }

        public int? UserId { get; set; }
        public int ProjectId { get; set; }
        public virtual User User { get; set; }
        public virtual Project Project { get; set; }
    }
}
