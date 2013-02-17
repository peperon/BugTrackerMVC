using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BugTracker.Domain.Models
{
    public class User
    {
        [HiddenInput(DisplayValue = false)]
        public int UserId { get; set; }

        [Required]
        [RegularExpression(@"[A-Za-z][A-Za-z0-9]{0,100}", 
            ErrorMessage = "User name can contain only letter and numbers and first charter must be letter.")]
        [Display(Name="User name")]
        [MinLength(3)]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [MinLength(3)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Phone")]
        public string Phone { get; set; }

        public int Role { get; set; }

        public DateTime? LastActivityDate { get; set; }

        public string LastActivity { get; set; }

        public virtual List<Error> Errors { get; set; }
    }
}
