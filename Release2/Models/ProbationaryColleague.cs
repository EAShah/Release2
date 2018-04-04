/* 
	Description: This file declares the class 'ProbationaryColleague' and its properties 
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
    using Release2.Models;

    // Creation of the ProbationaryColleague table as a class.
    [Table("ProbationaryColleague")]
    public partial class ProbationaryColleague : Colleague
    {
        public ProbationaryColleague()
        {
            Assignments = new HashSet<Assignment>();
            ExtensionRequests = new HashSet<ExtensionRequest>();
            ProgressReviews = new HashSet<ProgressReview>();
            SelfAssessments = new HashSet<SelfAssessment>();
        }

        // Declaring attributes of the ProbationaryColleague table as properties.
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public int ColleagueId { get; set; }

        public int? ProbationId { get; set; }

        public Levels Level { get; set; }

        public Cities CityOfProbation { get; set; }

        [Required]
        public ProbationTypes ProbationType { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Column(TypeName = "date")]
        public DateTime? JoinDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Column(TypeName = "date")]
        public DateTime? LeaveDate { get; set; }

        public ProbationSuccess ProbationSuccessStatus { get; set; }

        public virtual ICollection<Assignment> Assignments { get; set; }

        //public virtual Colleague Colleague { get; set; }

        public new ICollection<ExtensionRequest> ExtensionRequests { get; set; }

        public virtual ICollection<ProgressReview> ProgressReviews { get; set; }

        public virtual ICollection<SelfAssessment> SelfAssessments { get; set; }


        public enum Levels
        {
            [Display(Name = "1st Month")]
            First,

            [Display(Name = "2nd Month")]
            Second,

            [Display(Name = "3rd Month")]
            Third,

            [Display(Name = "4th Month")]
            Fourth,

            [Display(Name = "5th Month")]
            Five,

            [Display(Name = "6th Month")]
            Six
        }

        public enum ProbationTypes
        {
            Extended,

            Mandatory
        }

        public enum Cities
        {
            Jeddah,

            Dammam,

            Riyadh,

            Makkah,

            Madinah
        }
        
        public enum ProbationSuccess
        {
            Undefined,

            Successful,

            Unsuccessful
        }

    }
}
