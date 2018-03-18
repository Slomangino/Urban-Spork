using System.Net;
using System.Net.Mail;
using UrbanSpork.Common.DataTransferObjects;

namespace UrbanSpork.DataAccess.Emails
{
    public class Email
    {
        private MailMessage _mail;
        private SmtpClient _smtp;

        public Email()
        {
            _mail = new MailMessage();
            // The important part -- configuring the SMTP client
            _smtp = new SmtpClient();
            _smtp.Port = 587;
            _smtp.EnableSsl = true;
            _smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            _smtp.UseDefaultCredentials = false;

            //sender's address
            _mail.From = new MailAddress("UrbanSporkTesting@gmail.com");
            _smtp.Credentials = new NetworkCredential(_mail.From.ToString(), "Urban123");
            _smtp.Host = "smtp.gmail.com";
            

        }

        public void SendUserCreatedMessage(UserDTO user)
        {           
           
            //recipient address
            _mail.To.Add(new MailAddress("tjhansen4821@eagle.fgcu.edu"));
            _mail.Subject = "*******Your New Account********";
            //Formatted mail body
            _mail.IsBodyHtml = true;
            string message = "Dear "+ user.FirstName + " " + user.LastName + ", your new account with UrbanSpork has been successfully created";

            _mail.Body = message;
            _smtp.Send(_mail);
        } 

        
    }
}
