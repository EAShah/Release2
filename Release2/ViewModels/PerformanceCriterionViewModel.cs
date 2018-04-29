using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Release2.ViewModels
{
    public class PerformanceCriterionViewModel
    {
        public int ReviewId { get; set; }

        public int? Score { get; set; }

        public int CompetencyId { get; set; }

        [Display(Name = "Competency Name")]
        public string CompetencyName { get; set; }

        [Display(Name = "Scores")]
        public int? TotalScore { get; set; }
    }
}