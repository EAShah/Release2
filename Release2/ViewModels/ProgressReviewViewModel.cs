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
        public ProgressReviewViewModel()
        {
            Competencies = new List<CompetencyViewModel>();
            //Scores = new List<int>();
        }
        // no performance criterion to progress review source found; MAKE SURE PERFORMANCECRITERION IS BEING USED.


        //BADGES
        //EMAIL NOTIFS
        //JQUERY&AJAX 


        public int Id { get; set; }

        // Fields of the Progress Review

        [DataType(DataType.MultilineText)]
        [Display(Name = "Report")]
        public string EvalDescription { get; set; }

        [Display(Name = "Scores")]
        public int? TotalScore { get; set; }

        public int? Score { get; set; }

        public Levels Level { get; set; }

        public int DepartmentId { get; set; }
        public string Department { get; set; }


       
        public int? HREvaluatesId { get; set; }

        public int? PRDHApprovesId { get; set; }

        public int LMId { get; set; }

        public int PCId { get; set; }

        public int? SelfAssessmentId { get; set; }

       

        [Display(Name = "Approved by Department Head:")]
        public string DHApproval { get; set; }

        [Display(Name = "Evaluated by HR Associate:")]
        public string HREvaluation { get; set; }

        [Display(Name = "Created by Line Manager:")]
        public string LMCreates { get; set; }

        public string SelfAssessment { get; set; }

        [Display(Name = "Probationary Colleague")]
        public string ProbationaryColleague { get; set; }

        

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

        // add list of grades and competecnies

        public virtual List<PerformanceCriterion> PerformanceReviews { get; set; }

        public virtual List<CompetencyViewModel> Competencies { get; set; }

        //public virtual List<int> Scores { get; set; }
        //public virtual ICollection<PerformanceCriterion> PerformanceCriteria { get; set; }


    }
}