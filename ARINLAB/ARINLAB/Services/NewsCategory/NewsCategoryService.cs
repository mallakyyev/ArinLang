using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DAL.Data;
using DAL.Models.Dto.NewsModelDTO;
using Microsoft.EntityFrameworkCore;


namespace ARINLAB.Services.NewsCategory
{
    public class NewsCategoryService : INewsCategoryService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public NewsCategoryService(ApplicationDbContext applicationDbContext, IMapper iMapper)
        {
            _dbContext = applicationDbContext;
            _mapper = iMapper;

        }
        public IEnumerable<NewsCategoryDTO> GetAllNewsCategory()
        {

            string culture = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
            var newsCategories = _dbContext.NewsCategories;
            var result = _dbContext.NewsCategoryTranslates
                .Where(p => p.LanguageCulture == culture).Join(newsCategories, p => p.NewsCategoryId, k => k.Id,
                    (p, k) => new NewsCategoryDTO
                    {
                        Id = k.Id,
                        Name = p.Name
                    });

            return result;
        }

        public async Task CreateNewsCategory(CreateNewsCategoryDTO modelDTO)
        {
            DAL.Models.News.NewsCategory newsCategory = _mapper.Map<DAL.Models.News.NewsCategory>(modelDTO);
            await _dbContext.NewsCategories.AddAsync(newsCategory);
            await _dbContext.SaveChangesAsync();
        }

        public async Task EditNewsCategory(EditNewsCategoryDTO modelDTO)
        {
            DAL.Models.News.NewsCategory newsCategory = _mapper.Map<DAL.Models.News.NewsCategory>(modelDTO);
            _dbContext.NewsCategories.Update(newsCategory);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveNewsCategory(int id)
        {
            DAL.Models.News.NewsCategory newsCategory = await _dbContext.NewsCategories.FindAsync(id);
            _dbContext.NewsCategories.Remove(newsCategory);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<EditNewsCategoryDTO> GetNewsCategoryForEditById(int id)
        {
            var nc = await _dbContext.NewsCategories
               .Include(i => i.NewsCategoryTranslates)
               .SingleOrDefaultAsync(k => k.Id == id);
            EditNewsCategoryDTO editNewsCatDTO = _mapper.Map<EditNewsCategoryDTO>(nc);

            return editNewsCatDTO;
        }

        public async Task<NewsCategoryDTO> GetNewsCategoryByIdAsync(int id)
        {

            string culture = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
            var menu = await _dbContext.NewsCategories.FindAsync(id);
            var translate = await _dbContext.NewsCategoryTranslates
                .Where(p => p.LanguageCulture == culture).SingleOrDefaultAsync(p => p.NewsCategoryId == menu.Id);
            NewsCategoryDTO result = new NewsCategoryDTO
            {
                Id = menu.Id,
                Name = translate.Name

            };
            return result;

        }
    }
}
