using Project._1.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Release2.ViewModels
{
    /// <summary>
    /// Progress Review view model from Progress Review model and used by progress review controller
    /// </summary>
    public class ProgressReviewViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Region")]
        public ColleagueRegion ColleagueRegion { get; set; }

        public string Department { get; set; }

        // HOW TO ADD ONLY LM IDs & PC IDs

        [Required]
        [Display(Name = "Report")]
        public string EvalDescription { get; set; }

        [Display(Name = "Scores")]
        public int? TotalGrade { get; set; }

        public int? Grade { get; set; }

        public int CompetencyId { get; set; }
        public string CompetencyName { get; set; }

    }
}