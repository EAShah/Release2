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
        public Meeting()
        {
            ProgressReviews = new ProgressReview();
        }

        // Declaring attributes of the Meeting table as properties.
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MeetingId { get; set; }

        public DateTime? ScheduledDateTime { get; set; }

        public Boolean Rescheduled { get; set; }

        public DurationTimes Duration { get; set; }

        [StringLength(100)]
        public string Venue { get; set; }

        public virtual ProgressReview ProgressReviews { get; set; }


        public enum DurationTimes
        {
            [Display(Name = "10 Minutes")]
            Ten,

            [Display(Name = "20 Minutes")]
            Twenty,

            [Display(Name = "30 Minutes")]
            Thirty,

            [Display(Name = "40 Minutes")]
            Forty,

            [Display(Name = "50 Minutes")]
            Fifty,

            [Display(Name = "1 Hour")]
            Sixty,

            [Display(Name = "1.5 Hours")]
            Ninety,

            [Display(Name = "2 Hours")]
            OneTwenty

        }
    }
}
