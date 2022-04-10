﻿using AutoMapper;
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

        public ApplicationUserService(UserManager<DAL.Models.ApplicationUser> userManager,RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
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
                                                           Email = p.Email,
                                                           TotalNames = p.Names.Count,
                                                           TotalPhrases = p.WordClauses.Count,
                                                           TotalWords = p.Words.Count,
                                                           TotalWordSentences = p.WordSentences.Count,
                                                           UserName = p.UserName
                                                       });
            return res;
        }

        public async Task<DAL.Models.ApplicationUser> GetUserProfile(string userId)
        {
            var appUser = await _userManager.Users
                .SingleOrDefaultAsync(s => s.Id == userId);
            
            return appUser;
        }
    }
}
