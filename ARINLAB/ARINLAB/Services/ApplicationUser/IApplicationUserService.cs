using DAL.Models.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace ARINLAB.Services.ApplicationUser
{
    public interface IApplicationUserService
    {
        Task<DAL.Models.ApplicationUser> GetUserProfile(string userId);
        IEnumerable<DAL.Models.ApplicationUser> GetAllUsers();
        public IEnumerable<UserStatistics> GetAllUserstatistics();
        //IEnumerable<TSTB.DAL.Models.User.ApplicationUser> GetAllInterpreneurAndOrg();
        Task DeleteUser(string id);
    }
}
