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

namespace Release2.ViewModels
{
    /// <summary>
    /// Progress Review view model from Progress Review model and used by Progress Review controller
    /// </summary>
    public class ProgressReviewViewModel
    {
        public ProgressReviewViewModel()
        {
            Competencies = new List<CompetencyViewModel>();
            PerformanceReviews = new List<PerformanceCriterion>();
        }
    
        public int Id { get; set; }

        // Fields of the Progress Review

        [DataType(DataType.MultilineText)]
        [Display(Name = "Report")]
        [Required]
        public string EvalDescription { get; set; }

        [Display(Name = "Scores")]
        public int? TotalScore { get; set; }

        public int? Score { get; set; }

        public Levels Level { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Progress Review Evaluation")]
        public string PREvalDescription { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Self Evaluation")]
        public string SelfEvaluation { get; set; }

        public int DepartmentId { get; set; }
        public string Department { get; set; }

        public int? HREvaluatesId { get; set; }

        public int? PRDHApprovesId { get; set; }

        public int LMId { get; set; }

        public int PCId { get; set; }

        public int? CreationPCId { get; set; }


        [Display(Name = "Probationary Colleague")]
        public string CreationPC { get; set; }

        [Display(Name = "Approved by Department Head")]
        public string DHApproval { get; set; }

        [Display(Name = "Evaluated by HR Associate")]
        public string HREvaluation { get; set; }

        [Display(Name = "Created by Line Manager")]
        public string LMCreates { get; set; }

        [Display(Name = "Probationary Colleague")]
        public string ProbationaryColleague { get; set; }

        //Status Properties

        [Display(Name = "Approval")]
        public ApprovalStatus PRDHApprovalStatus { get; set; }

        [Display(Name = "Status")]
        public CompletionStatus PRCompletionStatus { get; set; }

        [Display(Name = "Evaluation")]
        public EvaluationDecision PRHRAEvalDecision { get; set; }


        [Display(Name = "Assessment Status")]
        public Status AssessmentStatus { get; set; }

        // Date Properties

        [Display(Name = "Report Submitted")]
        public DateTime? PRSubmissionDate { get; set; }

        [Display(Name = "Report Approved")]
        public DateTime? PRDHApproveDate { get; set; }

        [Display(Name = "Report Evaluated")]
        public DateTime? PRHRAEvalDate { get; set; }

        [Display(Name = "Assessment Submission Date")]
        [Column(TypeName = "date")]
        public DateTime? SASubmissionDate { get; set; }

        // add list of grades and competecnies

        public virtual List<PerformanceCriterion> PerformanceReviews { get; set; }

        public virtual List<CompetencyViewModel> Competencies { get; set; }

    }
}