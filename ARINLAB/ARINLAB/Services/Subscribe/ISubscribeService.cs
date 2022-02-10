using DAL.Models.Email;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ARINLAB.Services.Subscribe
{
    public interface ISubscribeService
    {
         Task<bool> AddSubscriber(Subscribers subscriber);
        Task DeleteSubscriber(string  id);
        IEnumerable<Subscribers> GetAllSubscribers();
        string GetEmail(string email);
    }
}
