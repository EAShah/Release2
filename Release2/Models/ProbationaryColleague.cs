/* 
	Description: This file declares the class 'ProbationaryColleague' and its properties 
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

    // Creation of the ProbationaryColleague table as a class.
    [Table("ProbationaryColleague")]
    public partial class ProbationaryColleague
    {
        public ProbationaryColleague()
        {
            Assignments = new HashSet<Assignment>();
            ExtensionSubmissions = new HashSet<ExtensionSubmission>();
            ProgressReviews = new HashSet<ProgressReview>();
            SelfAssessmentSubmissions = new HashSet<SelfAssessmentSubmission>();
        }

        // Declaring attributes of the ProbationaryColleague table as properties.
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ColleagueId { get; set; }

        public int? ProbationID { get; set; }

        public int Level { get; set; }

        [StringLength(50)]
        public string CityOfProbation { get; set; }

        [Required]
        [StringLength(10)]
        public string ProbationType { get; set; }

        [Column(TypeName = "date")]
        public DateTime? JoinDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? LeaveDate { get; set; }

        public virtual ICollection<Assignment> Assignments { get; set; }

        public virtual Colleague Colleague { get; set; }

        public virtual ICollection<ExtensionSubmission> ExtensionSubmissions { get; set; }

        public virtual ICollection<ProgressReview> ProgressReviews { get; set; }

        public virtual ICollection<SelfAssessmentSubmission> SelfAssessmentSubmissions { get; set; }
    }
}
