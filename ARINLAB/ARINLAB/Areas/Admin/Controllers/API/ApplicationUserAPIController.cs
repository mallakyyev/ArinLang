using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ARINLAB.Services.ApplicationUser;
using ARINLAB.Services.ImageService;
using DAL.Data;
using DAL.Models;
using DAL.Models.Dto;
using DevExtreme.AspNet.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace ARINLAB.Areas.Admin.Controllers.API
{
    [Route("api/[controller]")]
    //[Authorize(Roles = Roles.Admin)]
    [ApiController]
    public class ApplicationUserAPIController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IApplicationUserService _userService;
        private readonly ApplicationDbContext _dbContext;
        private readonly IImageService _imgService;
        public ApplicationUserAPIController(IApplicationUserService userService, UserManager<ApplicationUser> userManager, ApplicationDbContext dbContext, 
            IImageService imageService, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _userService = userService;
            _dbContext = dbContext;
            _imgService = imageService;
            _roleManager = roleManager;
        }

        // GET: api/ApplicationUserAPI
        [HttpGet]
        public async Task<object> Get(DataSourceLoadOptions loadOptions)
        {
            var user = await _userManager.GetUserAsync(User);
            var selectedRoleNames = await _userManager.GetRolesAsync(user);
            if (selectedRoleNames.Contains(Roles.Admin))
                return DataSourceLoader.Load<ApplicationUser>(_userService.GetAllUsers().AsQueryable(), loadOptions);
            else return null;
            
        }

        // GET: api/ApplicationUserAPI
        [HttpGet("UserStatistics")]
        public object UserStatistics(DataSourceLoadOptions loadOptions)
        {            
            return DataSourceLoader.Load<UserStatistics>(_userService.GetAllUserstatistics().AsQueryable(), loadOptions);            
        }
        // DELETE: api/CallBacksAPI/5
        [HttpDelete("{id}")]
        public async Task Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            //if (user.Photo != null)
            //{
            //    _imgService.DeleteImage(user.Photo, "Users");
            //}
            await _userManager.DeleteAsync(user);
        }

    }
}