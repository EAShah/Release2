/* 
	Description: This file declares the class 'PerformanceCriterion' and its properties 
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

    // Creation of the PerformanceCriterion table as a class.
    [Table("PerformanceCriterion")]
    public partial class PerformanceCriterion
    {
        // Declaring attributes of the PerformanceCriterion table as properties.
        public int? Grade { get; set; }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CompetencyID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ReviewID { get; set; }

        public virtual Competency Competency { get; set; }

        public virtual ProgressReview ProgressReview { get; set; }
    }
}
