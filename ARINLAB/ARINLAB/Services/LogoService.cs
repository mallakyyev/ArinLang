using DAL.Data;
using DAL.Models;
using DAL.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARINLAB.Services
{
    public class LogoService
    {
        private readonly ApplicationDbContext _dbContext;
        public LogoService(ApplicationDbContext applicationDb)
        {
            _dbContext = applicationDb;
        }
        public LogoDto GetLogos()
        {
            LogoDto model = new LogoDto();
            var res = new List<Logo>(_dbContext.Logos);
            if (res.Count > 0)
            {
                if (res.Count > 1)
                {
                    model.Id2 = res[1].Id;
                    model.Image2 = res[1].Image;
                    model.Name2 = res[1].Name;
                    model.Link2 = res[1].Link;
                }
                model.Id1 = res[0].Id;
                model.Image1 = res[0].Image;
                model.Name1 = res[0].Name;
                model.Link1 = res[0].Link;
            }
            return model;
        }
    }
}
