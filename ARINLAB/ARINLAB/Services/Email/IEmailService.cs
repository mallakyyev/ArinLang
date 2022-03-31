using DAL.Models.Dto.EmailsModelDTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;



namespace ARINLAB.Services.Email
{
    public interface IEmailService
    {
         Task<bool> SendEmail(EmailsDTO emaildto, List<string> emails);
         Task<bool> SendSingleEmailAsync(SingleEmailDTO email);
        
    }

}
