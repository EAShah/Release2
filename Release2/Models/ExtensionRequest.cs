/* 
	Description: This file declares the class 'ExtensionRequest' and its properties 
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
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ExtRequestId { get; set; }

        public int? ExtNumber { get; set; }

        [StringLength(100)]
        public string ExtReason { get; set; }

        public int? ExtRequestStatus { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ExtRequestAuditDate { get; set; }

        public int? HRAuditID { get; set; }

        public virtual Colleague HRAudits { get; set; }

        public virtual ICollection<ExtensionSubmission> ExtensionSubmissions { get; set; }
    }
}
