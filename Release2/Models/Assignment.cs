/* 
	Description: This file declares the class 'Assignment' and its properties
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

    // Creation of the Assignment table as a class.
    [Table("Assignment")]
    public partial class Assignment
    {
        // Declaring attributes of the Assignment table as properties.
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AssignmentId { get; set; }

        [Column(TypeName = "date")]
        public DateTime? AssignmentDate { get; set; }

        public AssignStatus AssignmentStatus { get; set; }

        [Column(TypeName = "date")]
        public DateTime? AssignmentInspectionDate { get; set; }

        public int HRAssignID { get; set; }

        public int LMAssignID { get; set; }

        public int? LMInspectID { get; set; }

        public int PCID { get; set; }

        public virtual Colleague HRAssigns { get; set; }

        public virtual Colleague LMAssigned { get; set; }

        public virtual Colleague LMInspects { get; set; }

        public virtual ProbationaryColleague ProbationaryColleague { get; set; }


        public enum AssignStatus
        {
            Pending,

            Approved,

            Rejected
        }
    }

    
}
