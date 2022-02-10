using DAL.Data;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARINLAB.Services.SessionService
{
    public  class UserDictionary
    {
        private const string SessionKeyName = "_Dictionary";
        private const int DefaultDictionary = -1;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext _dbContext;
        private readonly ISession _session;
        public UserDictionary(IHttpContextAccessor httpContextAccessor, ApplicationDbContext dbContext)
        {
            _httpContextAccessor = httpContextAccessor;
            _session = _httpContextAccessor.HttpContext.Session;
            _dbContext = dbContext;
        }
        public  int GetDictionaryId()
        {            
            if (!string.IsNullOrEmpty(_session.GetString(SessionKeyName)))
            {
                try
                {
                    return int.Parse(_session.GetString(SessionKeyName));

                }catch(Exception e)
                {
                    try
                    {
                        return _dbContext.Dictionaries.Take(1).ToList()[0].Id;
                    }catch(Exception e1)
                    {
                        return -1;
                    }
                }
            }
            try
            {
                return _dbContext.Dictionaries.Take(1).ToList()[0].Id;
            }
            catch (Exception e1)
            {
                return -1;
            }
        }

        public void SetDictionary(int dictId)
        {
            _session.SetString(SessionKeyName, $"{dictId}");
        }
        
}
}
