using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class EmailManager : IEmailManager
    {
        /// <summary>
        /// Bobby Thorne
        /// 3/4/2017
        /// 
        /// Holds the string for the subject line for a request
        /// username email
        /// </summary>
        /// <returns></returns>
        private string requestUsernameSubject()
        {
            return "SnackOverFlow Account Username Request";
        }

        /// <summary>
        ///  Bobby Thorne
        /// 3/4/2017
        /// 
        /// Holds the string for the body for a request
        /// username email
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        private string requestUsernameEmailBody(string username)
        {

            return "Here is your SnackOverFlow Account username.\n\n" + username + " \n\n Have a great day!";
        }

        /// <summary>
        /// Bobby Thorne
        /// 3/4/2017
        /// 
        /// Sends the email for requestUsernameEmail
        /// </summary>
        /// <param name="email"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public string sendRequestUsernameEmail(string email, string username)
        {

            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("snackoverflow2017@gmail.com");
                mail.To.Add(email);
                mail.Subject = requestUsernameSubject();
                mail.Body = requestUsernameEmailBody(username);

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("snackoverflow2017@gmail.com", "SnackTime");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
                return "message sent";
            }
            catch (Exception)
            {
                throw new Exception("Email unable to send");
            }
        }

    }
}
