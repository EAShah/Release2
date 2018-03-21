/* 
	Description: This file declares the ExtensionRequest ViewModel and its properties 
                 for the ExtensionRequest Controller and views.
	Author:  EAS
*/

using Project._1.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Project._1.Models.ExtensionRequest;
using static Project._1.Models.ExtensionSubmission;


namespace Release2.ViewModels
{
    public class ExtensionRequestViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Extension to month:")]
        public ExtNumbers ExtNumber { get; set; }

        [Required]
        [Display(Name = "Reason for Extension")]
        public string ExtReason { get; set; }

        [Display(Name = "Request Status")]
        public int? ExtRequestStatus { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ExtRequestSubmissionDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ExtRequestAuditDate { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        [Display(Name = "Full Name")]
        public string FullName
        {
            get { return FirstName + ", " + LastName; }
        }

        // Include collection using lists not virtual

        //public int ColleagueId { get; set; }
        //public string Colleague { get; set; }
        //public string ProbationaryColleague { get; set; }

        public List<ProbationaryColleague> ProbationaryColleagues  { get; set; }

        public List<Colleague> Colleagues { get; set; }

        public List<ExtensionSubmission> ExtensionSubmissions { get; set; }
    }
}