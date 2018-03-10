/* 
	Description: This file declares the class 'Department' and its properties 
                 for the Probation Management System database.
	Author: Elaf Shah/ EAS
	Due date: 27/02/2018
*/

namespace Project._1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    // Creation of the Department table as a class.
    [Table("Department")]
    public partial class Department
    {
        public Department()
        {
            Colleagues = new HashSet<Colleague>();
        }

        // Declaring attributes of the Department table as properties.
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DepartmentId { get; set; }

        [StringLength(30)]
        public string DepartmentName { get; set; }

        public virtual ICollection<Colleague> Colleagues { get; set; }
    }
}
