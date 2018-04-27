/* 
	Description: This file declares the class 'ProgressReview' and its properties 
                 for the Probation Management System database.
	Author:  EAS
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
    public partial class ProgressReview 
    {
        public ProgressReview()
        {
            PerformanceCriterions = new HashSet<PerformanceCriterion>();
            //SelfAssessments = new HashSet<SelfAssessment>();
        }

        // Declaring attributes of the ProgressReview table as properties.
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReviewId { get; set; }

        public int? TotalScore { get; set; }

        [StringLength(500)]
        [Required]
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

        public int? HREvaluatesId { get; set; }

        public int? PRDHApprovesId { get; set; }

        public int LMId { get; set; }

        public int PCId { get; set; }

        public virtual Colleague DHApproval { get; set; }

        public virtual Colleague HREvaluation { get; set; }

        public virtual Colleague LMCreates { get; set; }

        //public virtual SelfAssessment SelfAssessment { get; set; }

        //public int? SelfAssessmentId { get; set; }

        public virtual ICollection<PerformanceCriterion> PerformanceCriterions { get; set; }

        public virtual ProbationaryColleague ProbationaryColleague { get; set; }

        // Assessment variables
        [StringLength(250)]
        public string PREvalDescription { get; set; }

        [StringLength(250)]
        public string SelfEvaluation { get; set; }

        [Column(TypeName = "date")]
        public DateTime? SASubmissionDate { get; set; }

        public virtual ProbationaryColleague CreationPC { get; set; }

        public int? CreationPCId { get; set; }

        public Status AssessmentStatus { get; set; }

        //public virtual ICollection<SelfAssessment> SelfAssessments { get; set; }



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


        public enum Status
        {
            Pending,

            Submitted
        }

    }
}
