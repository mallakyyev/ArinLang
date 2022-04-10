using MimeKit;
using System;
using System.Collections.Generic;
//using System.Net.Mail;
using System.Threading.Tasks;
using MailKit.Security;

using DAL.Data;
using ARINLAB.Services.ApplicationUser;
using DAL.Models.Dto.EmailsModelDTO;
//using MailKit.Net.Smtp;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using ARINLAB.Models;
using System.Net.Mail;
using Microsoft.AspNetCore.Hosting;
using System.Net.Mime;

namespace ARINLAB.Services.Email
{
    public class EmailService : IEmailService
    {
        private readonly IApplicationUserService _userService;
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<EmailService> _logger;
        private readonly IWebHostEnvironment _appEnvironment;
        public EmailService(IApplicationUserService userService,ApplicationDbContext dbContext, ILogger<EmailService> logger,
                            IWebHostEnvironment appEnvironment)
        {
            _userService = userService;
            _dbContext = dbContext;
            _logger = logger;
            _appEnvironment = appEnvironment;
        }
        public async Task<bool> SendEmail(EmailsDTO emailsDto, List<string> emails)
        {
            List<MailboxAddress> ems = new List<MailboxAddress>();
            
            try
            {
                string AdminEmail = _dbContext.Settings.FirstOrDefault(p => p.Name.Contains("AdminEmail")).Value;
                string Password = _dbContext.Settings.FirstOrDefault(p => p.Name.Contains("AdminEmailPassword")).Value;
                string smtp = "mail." + AdminEmail.Substring(AdminEmail.IndexOf("@") + 1);
                var client = new SmtpClient();
                
               
                    //client.Connect(smtp, 8889, SecureSocketOptions.None);
                    //client.AuthenticationMechanisms.Remove("XOAUTH2");
                    //await client.AuthenticateAsync(AdminEmail, Password);                   
                    var emailMessage = new MailMessage();
                emailMessage.IsBodyHtml = true;
                    emailMessage.From = new MailAddress(AdminEmail);
                foreach (string email in emails)
                {
                    emailMessage.Bcc.Add(new MailAddress( email));
                }
                
                    emailMessage.Subject = emailsDto.Subject;

                //emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
                //{
                //  Text = emailsDto.Message + $"<br/> <br/> Please press the <a href='https://arinlang.com/Unsubscribe'>Link</a> to unsubscribe"
                //};
                string imageFile = _appEnvironment.WebRootPath;
                MailImageModel m = $"{emailsDto.Message}{$"<br/> <br/> Please press the <a href='https://arinlang.com/Unsubscribe'>Link</a> to unsubscribe"}".GetImage();
                AlternateView htmlMail = AlternateView.CreateAlternateViewFromString(m.MessageBody, null, MediaTypeNames.Text.Html);
                for(int i= 0; i < m.ImageSrc.Count;++i)
                {
                    LinkedResource myimage = new LinkedResource($"{imageFile}{m.ImageSrc[i]}", MediaTypeNames.Image.Jpeg);
                    myimage.ContentId = m.Cid[i];
                    htmlMail.LinkedResources.Add(myimage);                    
                }
                emailMessage.BodyEncoding = Encoding.UTF8;
                emailMessage.SubjectEncoding = Encoding.UTF8;
                emailMessage.AlternateViews.Add(htmlMail);
                client.Host = smtp;
                client.Port = 8889;
                client.Credentials = new System.Net.NetworkCredential(AdminEmail, Password);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.EnableSsl = false;
                
                client.Send(emailMessage);
                //await client.SendAsync(emailMessage);
                //await client.DisconnectAsync(true);
            }
            catch(Exception e)
            {
                _logger.LogError( $"{e.Message} \n {e.StackTrace} \n {e.InnerException} \n {e.Data}", e );
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
                using var client = new MailKit.Net.Smtp.SmtpClient();
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
