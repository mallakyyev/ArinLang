using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models.Email
{
    public class Emails
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public bool SendedToEntrepreneur { get; set; }
        public bool SendedToOrganization{ get; set; }
        public bool SendedToSubscribers { get; set; }
    }
}
