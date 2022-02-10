using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using DAL.Data;
using DAL.Models.Dto.MenuModelDTO;
using DAL.Models.Menu;

namespace ARINLAB.Services.Pages
{
    public class PagesService : IPagesService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public PagesService(ApplicationDbContext applicationDbContext, IMapper iMapper)
        {
            _dbContext = applicationDbContext;
            _mapper = iMapper;

        }

        public async Task CreatePages(CreatePagesDTO modelDTO)
        {
            DAL.Models.Menu.Pages page = _mapper.Map<DAL.Models.Menu.Pages>(modelDTO);
            await _dbContext.Pages.AddAsync(page);
            await _dbContext.SaveChangesAsync();
        }

        public async Task EditPages(EditPageDTO modelDTO)
        {
            DAL.Models.Menu.Pages page = await _dbContext.Pages.FindAsync(modelDTO.Id);
            if (page != null)
            {
                page.IsPublish = modelDTO.IsPublish;
                page.MenuId = modelDTO.MenuId;
                page.PagesTranslates = _mapper.Map<List<PagesTranslate>>(modelDTO.PagesTranslates);
                _dbContext.Pages.Update(page);
                await _dbContext.SaveChangesAsync();
            }                                    
        }

        public IEnumerable<PagesDTO> GetAllPages()
        {
            string culture = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
            var pages = _dbContext.Pages.AsNoTracking();
            var result = _dbContext.PagesTranslates.AsNoTracking()
                .Where(p => p.LanguageCulture == culture).Join(pages, p => p.PagesId, k => k.Id,
                    (p, k) => new PagesDTO
                    {
                        Id = k.Id,
                        MenuId = k.MenuId,
                        Name = p.Name,
                        Description = p.Description,
                        IsPublish = k.IsPublish,
                        MenuIdName = _dbContext.MenuTranslates.AsNoTracking().Where(t => t.LanguageCulture == culture).FirstOrDefault(t => t.MenuId == k.MenuId).Name,
                    });

            return result;
        }

        public IEnumerable<PagesDTO> GetAllIsPublishPages()
        {
            string culture = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
            var pages = _dbContext.Pages.Where(k => k.IsPublish == true).AsNoTracking();
            var result = _dbContext.PagesTranslates.AsNoTracking()
                .Where(p => p.LanguageCulture == culture).Join(pages, p => p.PagesId, k => k.Id,
                    (p, k) => new PagesDTO
                    {
                        Id = k.Id,
                        MenuId = k.MenuId,
                        Name = p.Name,
                        Description = p.Description,
                        IsPublish = k.IsPublish
                    });

            return result;
        }

        public async Task<PagesDTO> GetPageByMenuId(int menuId)
        {
            string culture = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
            var pages = await _dbContext.Pages.AsNoTracking().SingleOrDefaultAsync(k => k.MenuId == menuId);

            var translate = await _dbContext.PagesTranslates.AsNoTracking()
                .Where(p => p.LanguageCulture == culture).SingleOrDefaultAsync(p => p.PagesId == pages.Id);
            PagesDTO result = new PagesDTO
            {
                MenuId = pages.MenuId,
                IsPublish = pages.IsPublish,
                Description = translate.Description,
                Id = pages.Id,
                Name = translate.Name
            };

            return result;
        }

        public async Task RemovePages(int id)
        {
            DAL.Models.Menu.Pages page = await _dbContext.Pages.FindAsync(id);
            _dbContext.Pages.Remove(page);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<EditPageDTO> GetPagesForEditById(int id)
        {
            var pr = await _dbContext.Pages.AsNoTracking()
                .Include(i => i.PagesTranslates).AsNoTracking()
                .SingleOrDefaultAsync(k => k.Id == id);
            EditPageDTO editPagesDTO = _mapper.Map<EditPageDTO>(pr);

            return editPagesDTO;
        }

        public async Task<PagesDTO> GetPageById(int pageId)
        {
            string culture = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
            var pages = await _dbContext.Pages.AsNoTracking().SingleOrDefaultAsync(k => k.Id == pageId);

            var translate = await _dbContext.PagesTranslates.AsNoTracking()
                .Where(p => p.LanguageCulture == culture).SingleOrDefaultAsync(p => p.PagesId == pages.Id);
            PagesDTO result = new PagesDTO
            {
                MenuId = pages.MenuId,
                IsPublish = pages.IsPublish,
                Description = translate.Description,
                Id = pages.Id,
                Name = translate.Name
            };

            return result;
        }
    }
}
