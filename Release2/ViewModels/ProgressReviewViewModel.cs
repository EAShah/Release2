/* 
	Description: This file declares the Progress Review ViewModel and its properties 
                 for the Progress Review Controller  and views.
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
using static Project._1.Models.ProgressReview;
using static Project._1.Models.SelfAssessment;

namespace Release2.ViewModels
{
    /// <summary>
    /// Progress Review view model from Progress Review model and used by Progress Review controller
    /// </summary>
    public class ProgressReviewViewModel
    {
        public int Id { get; set; }

        // Fields of the Progress Review

        [Required]
        [Display(Name = "Report")]
        public string EvalDescription { get; set; }

        [Display(Name = "Scores")]
        public int? TotalGrade { get; set; }

        public int? Grade { get; set; }

        public int CompetencyId { get; set; }
        public string CompetencyName { get; set; }

        public int DepartmentId { get; set; }
        public string Department { get; set; }


        [Required]
        [Display(Name = "Progress Review Evaluation")]
        public string PREvalDescription { get; set; }

        [Required]
        [Display(Name = "Self Evaluation")]
        public string SelfEvaluation { get; set; }

        [Column(TypeName = "date")]
        public DateTime? SASubmissionDate { get; set; }

        public int? HREvaluatesId { get; set; }

        public int? PRDHApprovesId { get; set; }

        public int LMId { get; set; }

        public int PCId { get; set; }

        public int? SelfAssessmentId { get; set; }

        public int? CreationPCId { get; set; }


        public string DHApproval { get; set; }

        public string HREvaluation { get; set; }

        public string LMCreates { get; set; }

        public string SelfAssessment { get; set; }

        public string ProbationaryColleague { get; set; }

        public string CreationPC { get; set; }
        

        //Status Properties

        [Display(Name = "Approval")]
        public ApprovalStatus PRDHApprovalStatus { get; set; }

        [Display(Name = "Status")]
        public CompletionStatus PRCompletionStatus { get; set; }

        [Display(Name = "Evaluation")]
        public EvaluationDecision PRHRAEvalDecision { get; set; }

        
        // Date Properties

        [Display(Name = "Report Submitted")]
        public DateTime? PRSubmissionDate { get; set; }

        [Display(Name = "Report Approved")]
        public DateTime? PRDHApproveDate { get; set; }

        [Display(Name = "Report Evaluated")]
        public DateTime? PRHRAEvalDate { get; set; }
    }
}