using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ARINLAB.Services.Settings
{
    public interface ISettingsService
    {
        IEnumerable<DAL.Models.Settings> GetAllSettings();
        IEnumerable<DAL.Models.Settings> GetAllCashSettings();

        Task CreateSettings(DAL.Models.Settings model);

        Task EditSettings(DAL.Models.Settings model);

        Task RemoveSettings(int id);        
        Task<DAL.Models.Settings> GetSettingsById(int id);
    }
}
