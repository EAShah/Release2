using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using static Project._1.Models.SelfAssessment;

namespace Release2.ViewModels
{
    public class SelfAssessmentViewModel
    {
        public int Id { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Progress Review Evaluation")]
        public string PREvalDescription { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Self Evaluation")]
        public string SelfEvaluation { get; set; }

        [Display(Name = "Assessment Status")]
        public Status AssessmentStatus { get; set; }

        [Display(Name = "Assessment Submission Date")]
        [Column(TypeName = "date")]
        public DateTime? SASubmissionDate { get; set; }

        public int? CreationPCId { get; set; }

        [Display(Name = "Probationary Colleague")]
        public string CreationPC { get; set; }
    }
}