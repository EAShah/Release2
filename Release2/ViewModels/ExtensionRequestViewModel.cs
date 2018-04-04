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



namespace Release2.ViewModels
{
    /// <summary>
    ///Extension Request View Model from Extension Request model and used by Extension Request controller
    /// </summary>
    public class ExtensionRequestViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Extended To")]
        public ExtNumbers ExtNumber { get; set; }

        [Display(Name = "Reason")]
        [StringLength(500, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 20)]
        public string ExtReason { get; set; }

        [Display(Name = "Extension Request Status")]
        public RequestStatus ExtRequestStatus { get; set; }

        [Display(Name = "Submission Date")]
        [Column(TypeName = "date")]
        public DateTime? ExtRequestSubmissionDate { get; set; }

        [Display(Name = "Audit date")]
        [Column(TypeName = "date")]
        public DateTime? ExtRequestAuditDate { get; set; }

        public int? HRAuditId { get; set; }

        public int LMSubmitId { get; set; }

        public int ExtendedPCId { get; set; }

        [Display(Name = "Audited By")]
        public string HRAudits { get; set; }

        [Display(Name = "Submitted By")]
        public string LMSubmits { get; set; }

        [Display(Name = "Probationary Colleague")]
        public string ExtendedPC { get; set; }

    }
}