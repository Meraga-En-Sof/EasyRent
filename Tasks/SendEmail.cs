using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;

namespace KigooProperties.Tasks
{
    public class SendEmail
    {
        public async static Task SendMailAsyncgg(Wamds message)
        {
            #region formatter
            string text = string.Format("{0}: {1}", message.Subject, message.Body);

            #endregion

            MailMessage msg = new MailMessage();
            msg.From = new MailAddress("noreply@RentEasy.com");
            msg.To.Add(new MailAddress(message.Destination));
            msg.Subject = message.Subject;
            msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(message.Body, null, MediaTypeNames.Text.Html));
            msg.IsBodyHtml = true;
            //  msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(html, null, MediaTypeNames.Text.Html));

            SmtpClient smtpClient = new SmtpClient("RentEasy.com", Convert.ToInt32(587));
            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("noreply@Remteasy", "@RentEasy{Password");
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = credentials;
            smtpClient.EnableSsl = false;
            smtpClient.Send(msg);
        }

    }

    public class Wamds
    {

        public string Destination { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }
    }
}
