/* 
	Description: This file declares the class 'ExtensionSubmission' and its properties 
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

    // Creation of the ExtensionSubmission table as a class.
    [Table("ExtensionSubmission")]
    public partial class ExtensionSubmission
    {
        // Declaring attributes of the ExtensionSubmission table as properties.
        [Column(TypeName = "date")]
        public DateTime? ExtRequestSubmissionDate { get; set; }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ColleagueID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PCID { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ExtRequestID { get; set; }

        public virtual Colleague LMSubmits { get; set; }

        public virtual ExtensionRequest ExtensionRequest { get; set; }

        public virtual ProbationaryColleague ProbationaryColleague { get; set; }
    }
}
