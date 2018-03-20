using System;
using System.ComponentModel.DataAnnotations;
using static Project._1.Models.ExtensionRequest;

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

        public DateTime? ExtRequestAuditDate { get; set; }

        // Include collection using lists not virtual
        
        //public  ProbationaryColleagueViewModel  { get; set; }

        //public virtual ColleagueViewModel GetColleagueViewModel { get; set; }
    }
}