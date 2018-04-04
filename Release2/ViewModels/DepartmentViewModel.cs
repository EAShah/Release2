/* 
	Description: This file declares the Department ViewModel and its properties 
                 for the Colleague Department and views.
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
    /// Department view model from Department model and used by Department controller
    /// </summary>
    public class DepartmentViewModel
    {
        public int Id { get; set; }

        [StringLength(30)]
        [Display(Name ="Department Name")]
        public string DepartmentName { get; set; }
    }
}