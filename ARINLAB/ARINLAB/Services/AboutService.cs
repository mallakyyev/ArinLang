using DAL.Data;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARINLAB.Services
{
    public class AboutService
    {
        private readonly ApplicationDbContext _dbContext;
        public AboutService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Aboutus GetAboutus()
        {
            try
            {
                return _dbContext.Aboutus.FirstOrDefault();
            }catch(Exception e)
            {
                return null;
            }
        }

        public bool Edit(Aboutus about)
        {
            try
            {
                _dbContext.Aboutus.Update(about);
                _dbContext.SaveChanges();
                return true;
            }catch(Exception e)
            {
                return false;
            }
        }
    }
}
