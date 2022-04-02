using DAL.Data;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
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

        public string GetDictionaryName()
        {
            string culture = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
            string sess = _session.GetString(SessionKeyName);
            if (string.IsNullOrEmpty(sess))
            {
                if(culture == "ar")
                    return _dbContext.Dictionaries.Take(1).ToList()[0].ArabTranslate;
                else if(culture == "tk")
                    return _dbContext.Dictionaries.Take(1).ToList()[0].Language + "ça";
                return _dbContext.Dictionaries.Take(1).ToList()[0].Language;
            }
            var dict = _dbContext.Dictionaries.Find(int.Parse(sess));
            if (culture == "ar") 
                return dict.ArabTranslate;

            if(culture == "tk")
                return dict.Language+"ça";

            return dict.Language;
        }

        public void SetDictionary(int dictId )
        {
            _session.SetString(SessionKeyName, $"{dictId}");
        }
        
}
}
