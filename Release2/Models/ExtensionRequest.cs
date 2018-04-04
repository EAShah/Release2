/* 
	Description: This file declares the class 'ExtensionRequest' and its properties 
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

    // Creation of the ExtensionRequest table as a class.
    [Table("ExtensionRequest")]
    public partial class ExtensionRequest
    {
        //public ExtensionRequest()
        //{
        //    ExtensionSubmissions = new HashSet<ExtensionSubmission>();
        //}

        // Declaring attributes of the ExtensionRequest table as properties.
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ExtRequestId { get; set; }

        public ExtNumbers ExtNumber { get; set; }

        [StringLength(500)]
        public string ExtReason { get; set; }

        public RequestStatus ExtRequestStatus { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ExtRequestSubmissionDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ExtRequestAuditDate { get; set; }

        public int? HRAuditId { get; set; }

        public virtual Colleague HRAudits { get; set; }

        public virtual Colleague LMSubmits { get; set; }

        public int LMSubmitId { get; set; }

        public virtual ProbationaryColleague ExtendedPC { get; set; }

        public int ExtendedPCId { get; set; }

       

        public enum RequestStatus
        {
            Pending,

            Approved,

            Rejected
        }

        public enum ExtNumbers
        {
            [Display(Name = "Level 1")]
            One,

            [Display(Name = "Level 2")]
            Two,

            [Display(Name = "Level 3")]
            Three,

            [Display(Name = "Level 4")]
            Four,

            [Display(Name = "Level 5")]
            Five,

            [Display(Name = "Level 6")]
            Six
        }
    }
}
