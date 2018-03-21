/* 
	Description: This file declares the class 'Colleague' and its properties 
                 for the Probation Management System database.
	Author:  EAS
*/

namespace Project._1.Models
{
    using Release2.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    // Creation of the Colleague table as a class.
    [Table("Colleague")]
    public partial class Colleague: ApplicationUser
    {
        public Colleague()
        {
            CreatedAssignments = new HashSet<Assignment>();
            ReceivedAssignment = new HashSet<Assignment>();
            InspectionAssignment = new HashSet<Assignment>();
            ExtensionSubmissions = new HashSet<ExtensionSubmission>();
            ExtensionRequests = new HashSet<ExtensionRequest>();
            ApprovedProgressReviews = new HashSet<ProgressReview>();
            EvaluatedProgressReviews = new HashSet<ProgressReview>();
            CreatedProgressReviews = new HashSet<ProgressReview>();
            ApprovedSelfAssessments = new HashSet<SelfAssessment>();
            ReviewedSelfAssessments = new HashSet<SelfAssessment>();
        }

        // Declaring attributes of the Colleague table as properties.
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public int ColleagueID { get; set; }

        //[StringLength(20)]
        //public string Password { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Display(Name = "Full Name")]
        public string FullName
        {
            get { return FirstName + ", " + LastName; }
        }

        [Column(TypeName = "date")]
        public DateTime? DOB { get; set; }

        [Required]
        public EmploymentType EmploymentType { get; set; }

        [Required]
        public ColleagueType ColleagueType { get; set; }

        [Required]
        public ColleagueRegion ColleagueRegion { get; set; }

        public Gender Gender { get; set; }

        [StringLength(50)]
        public string Position { get; set; }

        [StringLength(50)]
        public string Nationality { get; set; }

        [StringLength(50)]
        public string Country { get; set; }

        [StringLength(50)]
        public string City { get; set; }

        //[StringLength(50)]
        //public string Email { get; set; }

        //[StringLength(20)]
        //public string MobileNumber { get; set; }

        [StringLength(20)]
        public string HomeNumber { get; set; }

        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        //public virtual ProbationaryColleague ProbationaryColleague { get; set; }

        public virtual ICollection<Assignment> CreatedAssignments { get; set; }

        public virtual ICollection<Assignment> ReceivedAssignment { get; set; }

        public virtual ICollection<Assignment> InspectionAssignment { get; set; }

        public virtual ICollection<ExtensionSubmission> ExtensionSubmissions { get; set; }

        public virtual ICollection<ExtensionRequest> ExtensionRequests { get; set; }

        public virtual ICollection<ProgressReview> ApprovedProgressReviews { get; set; }

        public virtual ICollection<ProgressReview> EvaluatedProgressReviews { get; set; }

        public virtual ICollection<ProgressReview> CreatedProgressReviews { get; set; }

        public virtual ICollection<SelfAssessment> ApprovedSelfAssessments { get; set; }

        public virtual ICollection<SelfAssessment> ReviewedSelfAssessments { get; set; }
    }

    public enum Gender
    {
        Female,

        Male
    }

    public enum EmploymentType
    {
        [Display(Name = "Full-timer")]
        FullTimer,

        [Display(Name = "Part-timer")]
        PartTimer,

        [Display(Name = "Intern")]
        Intern
    }

    public enum ColleagueType
    {
        [Display(Name = "HR Associate")]
        HRAssociate,

        [Display(Name = "Department Head")]
        DepartmentHead,

        [Display(Name = "Line Manager")]
        LineManager,

        [Display(Name = "Probationary Colleague")]
        ProbationaryColleague
    }

    public enum ColleagueRegion
    {
        Jeddah,

        Makkah,

        Madinah,

        Riyadh,

        Dammam
    }
}
