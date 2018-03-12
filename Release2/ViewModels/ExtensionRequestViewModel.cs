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
        public ExtNumber ExtNumber { get; set; }

        [Required]
        [Display(Name = "Reason for Extension")]
        public string ExtReason { get; set; }

        public int? ExtRequestStatus { get; set; }

        public DateTime? ExtRequestAuditDate { get; set; }
    }
}