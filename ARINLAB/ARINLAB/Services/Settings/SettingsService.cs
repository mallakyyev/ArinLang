using AutoMapper;
using DAL.Data;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace ARINLAB.Services.Settings
{
    public class SettingsService : ISettingsService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;

        public SettingsService(ApplicationDbContext dbContext, IMapper mapper, IMemoryCache memoryCache)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _memoryCache = memoryCache;
        }
        public async  Task CreateSettings(DAL.Models.Settings modelDTO)
        {
            await _dbContext.Settings.AddAsync(modelDTO);
            _dbContext.SaveChanges();
        }

        public async Task EditSettings(DAL.Models.Settings modelDTO)
        {
            _dbContext.Settings.Update(modelDTO);
            await _dbContext.SaveChangesAsync();
        }

        public IEnumerable<DAL.Models.Settings> GetAllSettings()
        {
            var settings = _dbContext.Settings;
            return settings;
        }

        public IEnumerable<DAL.Models.Settings> GetAllCashSettings()
        {
            IEnumerable<DAL.Models.Settings> settings = null;
            if (!_memoryCache.TryGetValue("settings", out settings))
            {
                settings = _dbContext.Settings;
                if (settings != null)
                {
                    _memoryCache.Set("settings", settings,
                        new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));
                }
            }
            return settings;
        }

        public async Task<DAL.Models.Settings> GetSettingsById(int id)
        {
            return await _dbContext.Settings.FindAsync(id);
        }

        public async Task RemoveSettings(int id)
        {
            var delSettings = await _dbContext.Settings.FindAsync(id);
            if (delSettings != null)
            {
                _dbContext.Settings.Remove(delSettings);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
