/* 
	Description: This file declares the class 'Competency' and its properties 
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

    // Creation of the Competency table as a class.
    [Table("Competency")]
    public partial class Competency
    {
        public Competency()
        {
            PerformanceCriterions = new HashSet<PerformanceCriterion>();
        }

        // Declaring attributes of the Competency table as properties.
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CompetencyId { get; set; }

        [StringLength(30)]
        public string CompetencyName { get; set; }

        public virtual ICollection<PerformanceCriterion> PerformanceCriterions { get; set; }
    }
}
