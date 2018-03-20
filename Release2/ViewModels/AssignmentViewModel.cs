using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static Project._1.Models.Assignment;
using static Project._1.Models.ProbationaryColleague;

namespace Release2.ViewModels
{
    public class AssignmentViewModel
    {
        public int Id { get; set; }

        [Display(Name = "User Name")]
        public string UserName { get; set; }

        public string Department { get; set; }

        [Display(Name = "Probation Type")]
        public ProbationTypes ProbationType { get; set; }

        public DateTime? AssignmentDate { get; set; }

        public AssignStatus AssignmentStatus { get; set; }

        public DateTime? AssignmentInspectionDate { get; set; }
    }
}