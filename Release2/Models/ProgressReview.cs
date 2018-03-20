/* 
	Description: This file declares the class 'ProgressReview' and its properties 
                 for the Probation Management System database.
	Author: Elaf Shah/ EAS
	Due date: 27/02/2018
*/

namespace Project._1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    // Creation of the ProgressReview table as a class.
    [Table("ProgressReview")]
    public partial class ProgressReview : SelfAssessment
    {
        public ProgressReview()
        {
            PerformanceCriterions = new HashSet<PerformanceCriterion>();
            SelfAssessmentSubmissions = new HashSet<SelfAssessmentSubmission>();
        }

        // Declaring attributes of the ProgressReview table as properties.
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReviewId { get; set; }

        public int? TotalGrade { get; set; }

        [StringLength(500)]
        public string EvalDescription { get; set; }

        public CompletionStatus PRCompletionStatus { get; set; }

        public ApprovalStatus PRDHApprovalStatus { get; set; }

        public EvaluationDecision PRHRAEvalDecision { get; set; }

        [Column(TypeName = "date")]
        public DateTime? PRSubmissionDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? PRDHApproveDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? PRHRAEvalDate { get; set; }

        public int? HREvaluatesID { get; set; }

        public int? PRDHApprovesID { get; set; }

        public int LMID { get; set; }

        public int PCID { get; set; }

        public int MeetingID { get; set; }

        public virtual Colleague DHApproval { get; set; }

        public virtual Colleague HREvaluation { get; set; }

        public virtual Colleague LMCreates { get; set; }

        //public virtual Meeting Meeting { get; set; }

        public virtual ICollection<PerformanceCriterion> PerformanceCriterions { get; set; }

        public virtual ProbationaryColleague ProbationaryColleague { get; set; }

        public virtual ICollection<SelfAssessmentSubmission> SelfAssessmentSubmissions { get; set; }



        public enum CompletionStatus
        {
            Incomplete,

            Complete
        }

        public enum EvaluationDecision
        {
            Pending,

            Approved,

            Disapproved
        }

        public enum ApprovalStatus
        {
            Pending,

            Approved
        }

    }
}
