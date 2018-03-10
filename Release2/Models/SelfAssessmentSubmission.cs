/* 
	Description: This file declares the class 'SelfAssessmentSubmission' and its properties 
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

    // Creation of the SelfAssessmentSubmission table as a class.
    [Table("SelfAssessmentSubmission")]
    public partial class SelfAssessmentSubmission
    {
        [Column(TypeName = "date")]
        public DateTime? SASubmissionDate { get; set; }

        // Declaring attributes of the SelfAssessmentSubmission table as properties.
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AssessmentId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PCID { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ReviewId { get; set; }

        public virtual ProbationaryColleague ProbationaryColleague { get; set; }

        public virtual ProgressReview ProgressReview { get; set; }

        public virtual SelfAssessment SelfAssessment { get; set; }
    }
}
