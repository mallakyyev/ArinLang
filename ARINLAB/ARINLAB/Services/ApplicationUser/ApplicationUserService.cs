using ARINLAB.Models;
using AutoMapper;
using DAL.Data;
using DAL.Models.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARINLAB.Services.ApplicationUser
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly UserManager<DAL.Models.ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _dbContext;

        public ApplicationUserService(UserManager<DAL.Models.ApplicationUser> userManager,
                                      RoleManager<IdentityRole> roleManager, IMapper mapper,
                                      ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _dbContext = context;
        }     

        public async Task DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
            }
        }       
      
        public IEnumerable<DAL.Models.ApplicationUser> GetAllUsers()
        {
            
            var appUsers = _userManager.Users.AsNoTracking().Where(o=>o.UserName!="Admin").OrderBy(o => o.FirstName);
            //var users = _mapper.ProjectTo<ApplicationUserDTO>(appUsers).AsQueryable();
            return appUsers;//users;
        }

        public IEnumerable<UserStatistics> GetAllUserstatistics()
        {
            var res = _userManager.Users.AsNoTracking().Include(word => word.Words)
                                                       .Include(phrases => phrases.WordClauses)
                                                       .Include(names => names.Names)
                                                       .Include(sent => sent.WordSentences)
                                                       .Select(p => new UserStatistics
                                                       {
                                                           Id = p.Id,
                                                           Email = p.Email,
                                                           TotalNames = p.Names.Count,
                                                           TotalPhrases = p.WordClauses.Count,
                                                           TotalWords = p.Words.Count,
                                                           TotalWordSentences = p.WordSentences.Count,
                                                           UserName = p.UserName
                                                       });
            return res;
        }

        private enum Table
        {
            Words = 1,
            WordSentences = 2,
            Phases = 3,
            Names = 4,
        }

        private UserStats GetStat(StatPeriod period, string userId)
        {
            UserStats result = new UserStats();
            if (period == StatPeriod.Monthly)
            {               
                        var wordsm = _dbContext.Words.Where(w => w.UserId == userId)
                                    .GroupBy(g => new { g.AddedDate.Year, g.AddedDate.Month },
                                                (key, group) => new TestStat()
                                                {
                                                    Year = key.Year,
                                                    Month = key.Month,
                                                    Count = group.Count()
                                                });
                        wordsm = wordsm.OrderBy(p => p.Year).ThenBy(p => p.Month);
                        foreach (var r in wordsm)
                        {
                            result.Words_X.Add($"{r.Year}-{r.Month}");
                            result.Words_Y.Add(r.Count);
                        }                                               
                  
                        var namesm = _dbContext.Names.Where(w => w.UserId == userId)
                                    .GroupBy(g => new { g.AddedDate.Year, g.AddedDate.Month },
                                                                (key, group) => new TestStat()
                                                                {
                                                                    Year = key.Year,
                                                                    Month = key.Month,
                                                                    Count = group.Count()
                                                                });
                namesm = namesm.OrderBy(p => p.Year).ThenBy(p => p.Month);
                foreach (var r in namesm)
                        {
                            result.Names_X.Add($"{r.Year}-{r.Month}");
                            result.Names_Y.Add(r.Count);
                        }
                                        
                            var wsm = _dbContext.WordSentences.Where(w => w.UserId == userId)
                                    .GroupBy(g => new { g.AddedDate.Year, g.AddedDate.Month },
                                                (key, group) => new TestStat()
                                                {
                                                    Year = key.Year,
                                                    Month = key.Month,
                                                    Count = group.Count()
                                                });
                wsm = wsm.OrderBy(p => p.Year).ThenBy(p => p.Month);
                foreach (var r in wsm)
                        {
                            result.WordSent_X.Add($"{r.Year}-{r.Month}");
                            result.WordSent_Y.Add(r.Count);
                        }
                                         
                        var wcm = _dbContext.WordClauses.Where(w => w.UserId == userId)
                                    .GroupBy(g => new { g.AddedDate.Year, g.AddedDate.Month },
                                                                (key, group) => new TestStat()
                                                                {
                                                                    Year = key.Year,
                                                                    Month = key.Month,
                                                                    Count = group.Count()
                                                                });
                wcm = wcm.OrderBy(p => p.Year).ThenBy(p => p.Month);
                foreach (var r in wcm)
                        {
                            result.Phrases_X.Add($"{r.Year}-{r.Month}");
                            result.Phrases_Y.Add(r.Count);
                        }
                        return result;                                                 
            }

            if(period == StatPeriod.Yearly)
            {               
                        var wordsy = _dbContext.Words.Where(w => w.UserId == userId)
                                    .GroupBy(g => new { g.AddedDate.Year },
                                                (key, group) => new TestStat()
                                                {
                                                    Year = key.Year,                                                   
                                                    Count = group.Count()
                                                });
                wordsy = wordsy.OrderBy(p => p.Year);
                foreach (var r in wordsy)
                        {
                            result.Words_X.Add($"{r.Year}");
                            result.Words_Y.Add(r.Count);
                        }                       
                   
                        var namesy = _dbContext.Names.Where(w => w.UserId == userId)
                                    .GroupBy(g => new { g.AddedDate.Year },
                                                                (key, group) => new TestStat()
                                                                {
                                                                    Year = key.Year,                                                                  
                                                                    Count = group.Count()
                                                                });
                namesy = namesy.OrderBy(p => p.Year);
                foreach (var r in namesy)
                        {
                            result.Names_X.Add($"{r.Year}");
                            result.Names_Y.Add(r.Count);
                        }                       
                    
                        var wsy = _dbContext.WordSentences.Where(w => w.UserId == userId)
                                .GroupBy(g => new { g.AddedDate.Year },
                                            (key, group) => new TestStat()
                                            {
                                                Year = key.Year,                                               
                                                Count = group.Count()
                                            });
                wsy = wsy.OrderBy(p => p.Year);
                foreach (var r in wsy)
                        {
                            result.WordSent_X.Add($"{r.Year}");
                            result.WordSent_Y.Add(r.Count);
                        }
                                          
                        var wcy = _dbContext.WordClauses.Where(w => w.UserId == userId).OrderBy(p => p.AddedDate)
                                    .GroupBy(g => new { g.AddedDate.Year },
                                                                (key, group) => new TestStat()
                                                                {
                                                                    Year = key.Year,                                                                   
                                                                    Count = group.Count()
                                                                });
                wcy = wcy.OrderBy(p => p.Year);
                foreach (var r in wcy)
                        {
                            result.Phrases_X.Add($"{r.Year}");
                            result.Phrases_Y.Add(r.Count);
                        }
                        return result;                   
            }

           
                    var words = _dbContext.Words.Where(w => w.UserId == userId)
                                .GroupBy(g => new { g.AddedDate.Year, g.AddedDate.Month, g.AddedDate.Day },
                                            (key, group) => new TestStat()
                                            {
                                                Year = key.Year,
                                                Month = key.Month,
                                                Day = key.Day,
                                                Count = group.Count()
                                            });
            words = words.OrderBy(p => p.Year).ThenBy(p => p.Month).ThenBy(p => p.Day);
            foreach (var r in words)
                    {
                        result.Words_X.Add($"{r.Year}-{r.Month}-{r.Day}");
                        result.Words_Y.Add(r.Count);
                    }
                   

              
                    var names = _dbContext.Names.Where(w => w.UserId == userId)
                                .GroupBy(g => new { g.AddedDate.Year, g.AddedDate.Month, g.AddedDate.Day },
                                                            (key, group) => new TestStat()
                                                            {
                                                                Year = key.Year,
                                                                Month = key.Month,
                                                                Day = key.Day,
                                                                Count = group.Count()
                                                            });
            names = names.OrderBy(p => p.Year).ThenBy(p => p.Month).ThenBy(p => p.Day);
            foreach (var r in names)
                    {
                        result.Names_X.Add($"{r.Year}-{r.Month}-{r.Day}");
                        result.Names_Y.Add(r.Count);
                    }
                   

               
                    var ws = _dbContext.WordSentences.Where(w => w.UserId == userId)
                            .GroupBy(g => new { g.AddedDate.Year, g.AddedDate.Month, g.AddedDate.Day },
                                        (key, group) => new TestStat()
                                        {
                                            Year = key.Year,
                                            Month = key.Month,
                                            Day = key.Day,
                                            Count = group.Count()
                                        });
            ws = ws.OrderBy(p => p.Year).ThenBy(p => p.Month).ThenBy(p => p.Day);
            foreach (var r in ws)
                    {
                        result.WordSent_X.Add($"{r.Year}-{r.Month}-{r.Day}");
                        result.WordSent_Y.Add(r.Count);
                    }
                   
                    var wc = _dbContext.WordClauses.Where(w => w.UserId == userId).OrderBy(p => p.AddedDate)
                                .GroupBy(g => new { g.AddedDate.Year, g.AddedDate.Month, g.AddedDate.Day },
                                                            (key, group) => new TestStat()
                                                            {
                                                                Year = key.Year,
                                                                Month = key.Month,
                                                                Day = key.Day,
                                                                Count = group.Count()
                                                            });
                    foreach (var r in wc)
                    {
                        result.Phrases_X.Add($"{r.Year}-{r.Month}-{r.Day}");
                        result.Phrases_Y.Add(r.Count);
                    }
                    return result;                           
        }

        public async Task<UserStats> GetUserStatistics(string userId, StatPeriod period)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return null;

            return GetStat(period, userId);                           
        }

        public async Task<DAL.Models.ApplicationUser> GetUserProfile(string userId)
        {
            var appUser = await _userManager.Users
                .SingleOrDefaultAsync(s => s.Id == userId);
            
            return appUser;
        }
    }
}
