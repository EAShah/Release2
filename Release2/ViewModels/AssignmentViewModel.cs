/* 
	Description: This file declares the Assignment ViewModel and its properties 
                 for the Assignment Controller and views.
	Author:  EAS
*/

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using static Project._1.Models.Assignment;
using static Project._1.Models.ProbationaryColleague;

namespace Release2.ViewModels
{
    /// <summary>
    /// Assignment view model from Assignment model and used by Assignment controller
    /// </summary>
    public class AssignmentViewModel
    {
        public int Id { get; set; }

        public int DepartmentId { get; set; }
        public string Department { get; set; }

        [Display(Name = "Probation Type")]
        public ProbationTypes ProbationType { get; set; }

        [Display(Name = "Assignment Creation")]
        [Column(TypeName = "date")]
        public DateTime? AssignmentDate { get; set; }

        [Display(Name = "Assignement Status")]
        public AssignStatus AssignmentStatus { get; set; }

        [Display(Name = "Inspection Date")]
        [Column(TypeName = "date")]
        public DateTime? AssignmentInspectionDate { get; set; }

        public int HRAssignId { get; set; }

        public int LMAssignId { get; set; }

        public int? LMInspectId { get; set; }

        public int PCId { get; set; }

        [Display(Name = "HR Associate")]
        public string HRAssigns { get; set; }

        [Display(Name = "Assigned to")]
        public string LMAssigned { get; set; }

        [Display(Name = "Inspected by")]
        public string LMInspects { get; set; }

        [Display(Name = "Probationary Colleague")]
        public string ProbationaryColleague { get; set; }
    }
}