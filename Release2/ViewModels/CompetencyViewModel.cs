/* 
	Description: This file declares the Competency ViewModel and its properties 
                 for the Competency Controller  and views.
	Author:  EAS
*/

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Release2.ViewModels
{
    /// <summary>
    /// Competency view model from Competency model and used by Competency controller
    /// </summary>
    public class CompetencyViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Competency Name")]
        [StringLength(30)]
        public string CompetencyName { get; set; }

        [Required]
        [Range(0,20,"Score must be between 0 and 20")]
        public int? Score { get; set; }

        //public int ReviewId { get; set; }


    }
}