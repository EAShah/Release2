/* 
	Description: This file declares the ProbationaryColleague ViewModel and its properties 
                 for the ProbationaryColleague Controller  and views.
	Author:  EAS
*/

using Project._1.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using static Project._1.Models.ProbationaryColleague;

namespace Release2.ViewModels
{
    public class ProbationaryColleagueViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email{ get; set; }

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
        [Display(Name = "Region")]
        public ColleagueRegion ColleagueRegion { get; set; }

        public int DepartmentId { get; set; }
        public string Department { get; set; }

        [Required]
        [Display(Name = "Level")]
        public Levels Level { get; set; }

        [Required]
        [Display(Name = "City of Probation")]
        public Cities CityOfProbation { get; set; }

        [Required]
        [Display(Name = "Probation Type")]
        public ProbationTypes ProbationType { get; set; }

        [Required]
        [Display(Name = "Date Joined")]
        public DateTime? JoinDate { get; set; }


      

        // To display a list of roles
        public string Roles { get; set; }
    }
}