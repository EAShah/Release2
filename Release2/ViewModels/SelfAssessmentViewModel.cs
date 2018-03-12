using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Release2.ViewModels
{
    public class SelfAssessmentViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Progress Review Evaluation")]
        public string PREvalDescription { get; set; }

        [Required]
        [Display(Name = "Self Evaluation")]
        public string SelfEvaluation { get; set; }
    }
}