using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace Release2.Models
{
    public static class Utilities
    {
        //private void SendVerificationLinkEmail(string emailId, string activationcode, string scheme, string host, string port)
        //{
        //    var varifyUrl = scheme + "://" + host + ":" + port + "/JobSeeker/ActivateAccount/" + activationcode;
        //    var fromMail = new MailAddress("your email id", "welcome mithilesh");
        //    var toMail = new MailAddress(emailId);
        //    var frontEmailPassowrd = "your password";
        //    string subject = "Your account is successfull created";
        //    string body = "<br/><br/>We are excited to tell you that your account is" +
        //" successfully created. Please click on the below link to verify your account" +
        //" <br/><br/><a href='" + varifyUrl + "'>" + varifyUrl + "</a> ";

        //    var smtp = new SmtpClient
        //    {
        //        Host = "smtp.gmail.com",
        //        Port = 587,
        //        EnableSsl = true,
        //        DeliveryMethod = SmtpDeliveryMethod.Network,
        //        UseDefaultCredentials = false,
        //        Credentials = new NetworkCredential(fromMail.Address, frontEmailPassowrd)

        //    };
        //    using (var message = new MailMessage(fromMail, toMail)
        //    {
        //        Subject = subject,
        //        Body = body,
        //        IsBodyHtml = true
        //    })
        //        smtp.Send(message);
        //}

        public static void SendEmail(string recipientmail, string frommail, string subject, string message1)
        {
            var body = $@"<p>Email From: {0} </p><p>Message:</p><p>{1}</p>";
            var message = new MailMessage();
            message.To.Add(new MailAddress("eishah@dah.edu.sa"));
            message.From = new MailAddress(frommail);
            message.Subject = subject;
            message.Body = string.Format(body, frommail, message1);
            message.IsBodyHtml = true;

            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = "pmscareem@gmail.com",  // replace with valid value
                    Password = "qwe!@123"  // replace with valid value
                };
                smtp.Credentials = credential;
                smtp.Host = "smtp-mail.outlook.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.Send(message);
            }
        }
    
}
}