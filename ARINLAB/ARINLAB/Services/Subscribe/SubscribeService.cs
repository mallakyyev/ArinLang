using DAL.Data;
using DAL.Models.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ARINLAB.Services.Subscribe
{
    public class SubscribeService : ISubscribeService
    {
        private readonly ApplicationDbContext _dbContext;
        public SubscribeService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> AddSubscriber(Subscribers subscriber)
        {
            Subscribers find = _dbContext.Subscribers.FirstOrDefault(p => p.Email == subscriber.Email);
            if (find == null)
            {
                _dbContext.Subscribers.Add(subscriber);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            else
                return false;
        }

        public async Task DeleteSubscriber(string id)
        {
            Subscribers d = await _dbContext.Subscribers.FindAsync(id);
            _dbContext.Subscribers.Remove(d);
            await _dbContext.SaveChangesAsync();
        }

        public IEnumerable<Subscribers> GetAllSubscribers()
        {
            return _dbContext.Subscribers;
        }

        public string GetEmail(string email)
        {
            var result = _dbContext.Subscribers.FirstOrDefault(x => x.Email == email);
            if (result != null)
                return result.Id;
            else
                return "";

        }
    }
}
