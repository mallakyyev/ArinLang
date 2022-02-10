using MimeKit;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;
using MailKit.Security;

using DAL.Data;
using ARINLAB.Services.ApplicationUser;
using DAL.Models.Dto.EmailsModelDTO;

namespace ARINLAB.Services.Email
{
    public class EmailService : IEmailService
    {
        private readonly IApplicationUserService _userService;
        private readonly ApplicationDbContext _dbContext;
        
        public EmailService(IApplicationUserService userService,ApplicationDbContext dbContext)
        {
            _userService = userService;
            _dbContext = dbContext;
        }
        public async Task<bool> SendEmail(EmailsDTO emails)
        {
            //List<string> entrEmail = new List<string>();           
           

            //    var emailMessage = new MimeMessage();
            //    emailMessage.From.Add(new MailboxAddress(emails.Header, emails.AdminEmail));
            //    emailMessage.To.AddRange(entrEmail.Select(p=> new MailboxAddress("",p)) );
            //    emailMessage.Subject = emails.Subject;
            //    emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            //    {
            //        Text = emails.Message
            //    };

            //try
            //{
            //    string smtp = "smtp." + emails.AdminEmail.Substring(emails.AdminEmail.IndexOf("@")+1);
            //    using var client = new SmtpClient();
            //    await client.ConnectAsync(smtp, 587, SecureSocketOptions.StartTls);
            //    client.AuthenticationMechanisms.Remove("XOAUTH2");
            //    await client.AuthenticateAsync(emails.AdminEmail, emails.Password);
            //    await client.SendAsync(emailMessage);
            //    await client.DisconnectAsync(true);
            //}catch(Exception e)
            //{

            //    return false;
            //}
            return true;            
        }

        public  bool SendSingleEmail(SingleEmailDTO emails)
        {

            //var emailMessage = new MimeMessage();
            //emailMessage.From.Add(new MailboxAddress("", emails.AdminEmail));
            //emailMessage.To.Add(new MailboxAddress("",emails.EmailTo));
            //emailMessage.Subject = emails.Subject;
            //emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            //{
            //    Text = emails.Message
            //};

            //try
            //{
            //    string smtp = "smtp." + emails.AdminEmail.Substring(emails.AdminEmail.IndexOf("@") + 1);
            //    using var client = new SmtpClient();
            //    await client.ConnectAsync(smtp, 587, SecureSocketOptions.StartTls);
            //    client.AuthenticationMechanisms.Remove("XOAUTH2");
            //    await client.AuthenticateAsync(emails.AdminEmail, emails.Password);
            //    await client.SendAsync(emailMessage);
            //    await client.DisconnectAsync(true);
            //}
            //catch (Exception e)
            //{
            //    return false;
            //}
            //return true;
            //string _sender = "arinlang@outlook.com";
            //string _password = "YhussY2022";

            SmtpClient client = new SmtpClient("smtp-mail.outlook.com");

            client.Port = 587;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            System.Net.NetworkCredential credentials =
                new System.Net.NetworkCredential(emails.AdminEmail, emails.Password);
            client.EnableSsl = true;
            client.Credentials = credentials;

            MailMessage message = new MailMessage(emails.AdminEmail, emails.EmailTo);
            message.Subject = emails.Subject;
            message.Body = emails.Message;
            try
            {
                client.Send(message);
            }catch(Exception e)
            {
                return false;
            }
            return true;
        }
    }
}
