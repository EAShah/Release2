/* 
	Description: This file declares the class 'SelfAssessment' and its properties
                 for the Probation Management System database.
	Author: EAS
*/

namespace Project._1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using static Project._1.Models.ProgressReview;

    // Creation of the SelfAssessment table as a class.
    [Table("SelfAssessment")]
    public partial class SelfAssessment
    {
        //public SelfAssessment()
        //{
        //    SelfAssessmentSubmissions = new HashSet<SelfAssessmentSubmission>();
        //}

        // Declaring attributes of the SelfAssessment table as properties.
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AssessmentId { get; set; }

        [StringLength(250)]
        public string PREvalDescription { get; set; }

        [StringLength(250)]
        public string SelfEvaluation { get; set; }

        [Column(TypeName = "date")]
        public DateTime? SASubmissionDate { get; set; }

        public virtual ProbationaryColleague CreationPC { get; set; }

        public int? CreationPCId { get; set; }



        //These properties have been commented because they are not required.

        //public ApprovalStatus SAApprovalStatus { get; set; }
        //public CompletionStatus SACompletionStatus { get; set; }
        //public ApprovalStatus SAHRAReviewDecision { get; set; }
        //[Column(TypeName = "date")]
        //public DateTime? SAHRAReviewDate { get; set; }
        //[Column(TypeName = "date")]
        //public DateTime? SADHApproveDate { get; set; }
        //public int? HRReviewsId { get; set; }
        //public int? DHApprovesId { get; set; }
        //public virtual Colleague DHApprovals { get; set; }
        //public virtual Colleague HRReviews { get; set; }

        //public virtual ICollection<SelfAssessmentSubmission> SelfAssessmentSubmissions { get; set; }


    }
}
