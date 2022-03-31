using MimeKit;
using System;
using System.Collections.Generic;
//using System.Net.Mail;
using System.Threading.Tasks;
using MailKit.Security;

using DAL.Data;
using ARINLAB.Services.ApplicationUser;
using DAL.Models.Dto.EmailsModelDTO;
using MailKit.Net.Smtp;
using System.Linq;

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
        public async Task<bool> SendEmail(EmailsDTO emailsDto, List<string> emails)
        {           
            string AdminEmail = _dbContext.Settings.FirstOrDefault(p => p.Name.Contains("AdminEmail")).Value;
            string Password = _dbContext.Settings.FirstOrDefault(p => p.Name.Contains("AdminEmailPassword")).Value;
            List<MailboxAddress> address = new List<MailboxAddress>();
            foreach(string email in emails)
            {
                address.Add(new MailboxAddress("", email));
            }
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("", AdminEmail));
            emailMessage.To.AddRange(address);
            emailMessage.Subject = emailsDto.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = emailsDto.Message
            };

            try
            {
                string smtp = "mail." + AdminEmail.Substring(AdminEmail.IndexOf("@") + 1);
                using var client = new SmtpClient();
                await client.ConnectAsync(smtp, 8889, SecureSocketOptions.None);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                await client.AuthenticateAsync(AdminEmail, Password);
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
            catch (Exception e)
            {
                return false;
            }
            return true;                      
        }

        public async Task<bool> SendSingleEmailAsync(SingleEmailDTO emails)
        {

            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("", emails.AdminEmail));
            emailMessage.To.Add(new MailboxAddress("", emails.EmailTo));
            emailMessage.Subject = emails.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = emails.Message
            };

            try
            {
                string smtp = "mail." + emails.AdminEmail.Substring(emails.AdminEmail.IndexOf("@") + 1);
                using var client = new SmtpClient();
                await client.ConnectAsync(smtp, 8889, SecureSocketOptions.None);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                await client.AuthenticateAsync(emails.AdminEmail, emails.Password);
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
            //string _sender = "arinlang@outlook.com";
            //string _password = "YhussY2022";

            //SmtpClient client = new SmtpClient("mail.arinlang.com");

            //client.Port = 8889;
            //client.DeliveryMethod = SmtpDeliveryMethod.Network;
            //client.UseDefaultCredentials = false;
            //System.Net.NetworkCredential credentials =
            //    new System.Net.NetworkCredential(emails.AdminEmail, emails.Password);
            ////client.EnableSsl = true;
            //client.Credentials = credentials;

            //MailMessage message = new MailMessage(emails.AdminEmail, emails.EmailTo);
            //message.Subject = emails.Subject;
            //message.Body = emails.Message;
            //try
            //{
            //    client.Send(message);
            //}catch(Exception e)
            //{
            //    return false;
            //}
            //return true;
        }
    }
}
