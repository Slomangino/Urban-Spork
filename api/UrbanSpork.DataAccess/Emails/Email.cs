using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using UrbanSpork.Common.DataTransferObjects;
using UrbanSpork.Common.DataTransferObjects.User;

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
            _mail.To.Add(new MailAddress(forAgg.Email));
            _mail.Subject = "*******Your New Account********";
            //Formatted mail body
            _mail.IsBodyHtml = true;
            string message = "Dear "+ user.FirstName + " " + user.LastName + ", your new account with UrbanSpork has been successfully created";

            _mail.Body = message;
            _smtp.Send(_mail);
        } 

        public void SendPermissionsRequestedMessage(UserAggregate forAgg, List<PermissionAggregate> permissions)
        {
            var requestedPermissionNames = new List<string>();
            foreach (var permission in permissions)
            {
                requestedPermissionNames.Add(permission.Name);
            }
            
            var requests = String.Join(", ", requestedPermissionNames.ToArray());

            //recipient address
            _mail.To.Add(new MailAddress(forAgg.Email));
            _mail.Subject = "*******Request Submitted********";
            //Formatted mail body
            _mail.IsBodyHtml = true;
            string message = "Hello " + forAgg.FirstName + " " + forAgg.LastName + ", the request(s) for your access to the following: "+ requests + ", was successfully submitted. ";
            _mail.Body = message;
            _smtp.Send(_mail);
        }

        public void SendRequestDeniedMessage(UserAggregate forAgg, List<PermissionAggregate> deniedPermissions)
        {
            var PermissionNames = new List<string>();
            foreach (var permission in deniedPermissions)
            {
                PermissionNames.Add(permission.Name);
            }

            var requests = String.Join(", ", PermissionNames.ToArray());

            //recipient address
            _mail.To.Add(new MailAddress(forAgg.Email));
            _mail.Subject = "*******Request Denied********";
            //Formatted mail body
            _mail.IsBodyHtml = true;
            string message = "Hello " + forAgg.FirstName + " " + forAgg.LastName + ", the request(s) for your access to the following: " + requests + ", was denied. ";
            _mail.Body = message;
            _smtp.Send(_mail);
        }

        public void SendPermissionsGrantedMessage(UserAggregate forAgg, List<PermissionAggregate> grantedPermissions)
        {
            var PermissionNames = new List<string>();
            foreach (var permission in grantedPermissions)
            {
                PermissionNames.Add(permission.Name);
            }

            var requests = String.Join(", ", PermissionNames.ToArray());

            //recipient address
            _mail.To.Add(new MailAddress(forAgg.Email));
            _mail.Subject = "*******Request Granted********";
            //Formatted mail body
            _mail.IsBodyHtml = true;
            string message = "Hello " + forAgg.FirstName + " " + forAgg.LastName + ", the request(s) for your access to the following: " + requests + ", was granted. ";
            _mail.Body = message;
            _smtp.Send(_mail);
        }

        public void SendPermissionsRevokedMessage(UserAggregate forAgg, List<PermissionAggregate> revokedPermissions)
        {
            var PermissionNames = new List<string>();
            foreach (var permission in revokedPermissions)
            {
                PermissionNames.Add(permission.Name);
            }

            var permissions = String.Join(", ", PermissionNames.ToArray());

            //recipient address
            _mail.To.Add(new MailAddress(forAgg.Email));
            _mail.Subject = "*******Request Revoked********";
            //Formatted mail body
            _mail.IsBodyHtml = true;
            string message = "Hello " + forAgg.FirstName + " " + forAgg.LastName + ", the request(s) for your access to the following: " + permissions + ", was revoked. ";
            _mail.Body = message;
            _smtp.Send(_mail);
        }

    }
}
