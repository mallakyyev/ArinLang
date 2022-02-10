using DAL.Models;
using DAL.Models.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ARINLAB.Services
{
    public interface ILanguageService
    {
        IEnumerable<LanguageDto> GetAllLanguage();
        IEnumerable<LanguageDto> GetAllPublishLanguage();

        Task CreateLanguage(CreateLanguageDTO modelDTO);

        Task EditLanguage(LanguageDto modelDTO);

        Task RemoveLanguage(int id);
    }
}
