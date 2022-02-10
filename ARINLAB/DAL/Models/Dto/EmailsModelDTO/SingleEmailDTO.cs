using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models.Dto.EmailsModelDTO
{
    public class SingleEmailDTO
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string EmailTo { get; set; }
        public string AdminEmail { get; set; }
        public string Password { get; set; }
    }
}
