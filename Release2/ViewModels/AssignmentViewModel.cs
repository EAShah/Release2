/* 
	Description: This file declares the Assignment ViewModel and its properties 
                 for the Assignment Controller and views.
	Author:  EAS
*/

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

        public int DepartmentId { get; set; }
        public string Department { get; set; }

        [Display(Name = "Probation Type")]
        public ProbationTypes ProbationType { get; set; }

        public DateTime? AssignmentDate { get; set; }

        public AssignStatus AssignmentStatus { get; set; }

        public DateTime? AssignmentInspectionDate { get; set; }

        public int HRAssignID { get; set; }

        public int LMAssignID { get; set; }

        public int? LMInspectID { get; set; }

        public int PCID { get; set; }

        public string HRAssigns { get; set; }

        public string LMAssigned { get; set; }

        public string LMInspects { get; set; }

        public string ProbationaryColleague { get; set; }
    }

}