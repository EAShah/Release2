using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Release2.ViewModels
{
    public class CompetencyViewModel
    {
        public int Id { get; set; }

        [StringLength(30)]
        public string CompetencyName { get; set; }
    }
}