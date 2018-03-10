using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Release2.ViewModels
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }

        [StringLength(30)]
        [Display(Name ="Department Name")]
        public string DepartmentName { get; set; }
    }
}