/* 
	Description: This file declares the class 'Meeting' and its properties 
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

    // Creation of the Meeting table as a class.
    [Table("Meeting")]
    public partial class Meeting
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Meeting()
        {
            ProgressReviews = new ProgressReview();
        }

        // Declaring attributes of the Meeting table as properties.
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MeetingId { get; set; }

        public DateTime? ScheduledDateTime { get; set; }

        [StringLength(10)]
        public string Rescheduled { get; set; }

        public int? Duration { get; set; }

        [StringLength(100)]
        public string Venue { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ProgressReview ProgressReviews { get; set; }
    }
}
