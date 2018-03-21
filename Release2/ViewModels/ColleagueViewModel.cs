/* 
	Description: This file declares the Colleague ViewModel and its properties 
                 for the Colleague Controller and views.
	Author:  EAS
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Release2.Models;
using Project._1.Models;

namespace Release2.ViewModels
{
    /// <summary>
    /// Colleague view model from Colleague model and used by colleague controller
    /// </summary>
    public class ColleagueViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Full Name")]
        public string FullName
        {
            get { return FirstName + ", " + LastName; }
        }

        [Required]
        [Display(Name ="Colleague Type")]
        public ColleagueType ColleagueType { get; set; }

        [Required]
        [Display(Name ="Employee Type")]
        public EmploymentType EmploymentType { get; set; }

        [Required]
        [Display(Name = "Region")]
        public ColleagueRegion ColleagueRegion { get; set; }

        public int DepartmentId { get; set; }
        public string Department { get; set; }

        // To display a list of roles
        public string Roles { get; set; }

    }
}
