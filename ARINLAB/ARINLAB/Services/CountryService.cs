using DAL.Data;
using DAL.Models.Dto;
using DAL.Models.Configs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace ARINLAB.Services
{
    public class CountryService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        public CountryService(ApplicationDbContext applicationDb, IMapper mapper)
        {
            _dbContext = applicationDb;
            _mapper = mapper;
        }

        public async Task<Country> Create(CountryDto country)
        {
            try
            {
                Country cntr = _mapper.Map<Country>(country);
                var res = await _dbContext.Countries.AddAsync(cntr);
                await _dbContext.SaveChangesAsync();
                return res.Entity;
            }catch(Exception e)
            {
                return null;
            }
        }

        public async Task<Country> EditAsync(CountryDto country)
        {
            try
            {
                var res = _dbContext.Countries.Update(_mapper.Map<Country>(country));
                await _dbContext.SaveChangesAsync();
                return res.Entity;
            }catch(Exception e)
            {
                return null;
            }
        }

        public async Task<Country> DeleteAsync(int id)
        {
            var res = await _dbContext.Countries.FindAsync(id);
            if(res != null)
            {
                var r = _dbContext.Countries.Remove(res);
                await _dbContext.SaveChangesAsync();
                return r.Entity;
            }
            return null;
        }
        public List<CountryDto> GetAllCountries()
        {           
            return _mapper.Map<List<CountryDto>>(_dbContext.Countries);
        }

        public async Task<CountryDto> GetCountryById(int id)
        {
            var res = await _dbContext.Countries.FindAsync(id);
            if (res != null)
                return _mapper.Map<CountryDto>(res);
            return null;
        }
    }
}
