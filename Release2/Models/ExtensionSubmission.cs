/* 
	Description: This file declares the class 'ExtensionSubmission' and its properties 
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

    // Creation of the ExtensionSubmission table as a class.
    [Table("ExtensionSubmission")]
    public partial class ExtensionSubmission
    {
        // Declaring attributes of the ExtensionSubmission table as properties.
        [Column(TypeName = "date")]
        public DateTime ExtRequestSubmissionDate { get; set; }

        [Key]
        [Column(Order = 0)]
        // Check if it is none or computed, cant be identity because it gives error while running: cant have multiple identity columns
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ColleagueId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PCID { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ExtRequestId { get; set; }

        public virtual Colleague LMSubmits { get; set; }

        public virtual ExtensionRequest ExtensionRequest { get; set; }

        public virtual ProbationaryColleague ProbationaryColleague { get; set; }
    }
}
