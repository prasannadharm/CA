using CA_TechService.Common.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CA_TechService.Data.DataSource.EmailServer
{
    public class SendEmail
    {
        public bool SendMail(string toadd, string subject, string msg)
        {
            EmailEntity objserver = new EmailEntity();
            objserver = new EmailServerDAO().GetEmailServerDetails();
            bool retval = false;
            MailMessage mail = new MailMessage();
            mail.To.Add(toadd);
            mail.From = new MailAddress(objserver.UserName, "CA Website", System.Text.Encoding.UTF8);
            mail.Subject = subject;
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.Body = msg;
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;
            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential(objserver.UserName, objserver.UserPassword);
            client.Port = objserver.Port;
            client.Host = objserver.Host;
            client.EnableSsl = objserver.EnableSsl;
            try
            {
                client.Send(mail);
                retval = true;
            }
            catch (Exception ex)
            {
                Exception ex2 = ex;
                string errorMessage = string.Empty;
                while (ex2 != null)
                {
                    errorMessage += ex2.ToString();
                    ex2 = ex2.InnerException;
                }
                retval = false;
            }

            return retval;
        }
    }
}
