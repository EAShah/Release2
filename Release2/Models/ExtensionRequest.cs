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
        public ExtensionRequest()
        {
            ExtensionSubmissions = new HashSet<ExtensionSubmission>();
        }

        // Declaring attributes of the ExtensionRequest table as properties.
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ExtRequestId { get; set; }

        public ExtNumbers ExtNumber { get; set; }

        [StringLength(100)]
        public string ExtReason { get; set; }

        public RequestStatus ExtRequestStatus { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ExtRequestAuditDate { get; set; }

        public int? HRAuditID { get; set; }

        public virtual Colleague HRAudits { get; set; }

        public virtual ICollection<ExtensionSubmission> ExtensionSubmissions { get; set; }


        public enum RequestStatus
        {
            Pending,

            Approved,

            Rejected
        }

        public enum ExtNumbers
        {
            [Display(Name = "1")]
            One,

            [Display(Name = "2")]
            Two,

            [Display(Name = "3")]
            Three,

            [Display(Name = "4")]
            Four,

            [Display(Name = "5")]
            Five,

            [Display(Name = "6")]
            Six
        }
    }
}
