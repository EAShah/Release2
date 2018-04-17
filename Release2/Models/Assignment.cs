/* 
	Description: This file declares the class 'Assignment' and its properties
                 for the Probation Management System database.
	Author: EAS
*/

namespace Project._1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Net;
    using System.Net.Mail;

    // Creation of the Assignment table as a class.
    [Table("Assignment")]
    public partial class Assignment
    {
        // Declaring attributes of the Assignment table as properties.
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AssignmentId { get; set; }

        [Column(TypeName = "date")]
        public DateTime? AssignmentDate { get; set; }

        public AssignStatus AssignmentStatus { get; set; }

        [Column(TypeName = "date")]
        public DateTime? AssignmentInspectionDate { get; set; }

        public int HRAssignId { get; set; }

        public int LMAssignId { get; set; }

        public int? LMInspectId { get; set; }

        public int PCId { get; set; }

        public int DepartmentId { get; set; }

        public virtual Department Department { get; set; }

        public virtual Colleague HRAssigns { get; set; }

        public virtual Colleague LMAssigned { get; set; }

        public virtual Colleague LMInspects { get; set; }

        public virtual ProbationaryColleague ProbationaryColleague { get; set; }


        public enum AssignStatus
        {
            Pending,

            Approved,

            Rejected
        }


        public void SendEmail(string recipientmail, string frommail, string subject, string message1)
        {
            var body = "<p>Email From: {0} </p><p>Message:</p><p>{1}</p>";
            var message = new MailMessage(); 
            message.To.Add(new MailAddress("recipient@mail.com"));
            message.From = new MailAddress(frommail);
            message.Subject = subject;
            message.Body = string.Format(body, frommail, message);
            message.IsBodyHtml = true;

            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = "user@outlook.com",  // replace with valid value
                    Password = "password"  // replace with valid value
                };
                smtp.Credentials = credential;
                smtp.Host = "smtp-mail.outlook.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.SendMailAsync(message);
            }
        }
    }
}
