using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models.Dto.EmailsModelDTO
{
    public class EmailsDTO
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }     
        public string Header { get; set; }
        public string AdminEmail { get; set; }
        public string Password { get; set; }
        public bool SendToOrdinary { get; set; }
        public bool SendToApprove { get; set; }
        public bool SendToSubscribers { get; set; }
    }
}
