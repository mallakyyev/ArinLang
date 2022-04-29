using ARINLAB.Services.ImageService;
using ARINLAB.Services.SessionService;
using AutoMapper;
using DAL.Data;
using DAL.Models;
using DAL.Models.Dto;
using DAL.Models.ResponceModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ARINLAB.Services
{
    public class WordClauseService : IWordClauseService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly UserManager<DAL.Models.ApplicationUser> _useManager;
        private readonly IImageService _fileServices;
        private readonly IDictionaryService _dictionaryService;
        private readonly UserDictionary _userDicts;
        private Random rnd = new Random();
        public WordClauseService(ApplicationDbContext dbContext, IMapper mapper, 
                                UserManager<DAL.Models.ApplicationUser> userManager, IImageService fileServices,
                                UserDictionary userDictionary, IDictionaryService dictionaryService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _useManager = userManager;
            _fileServices = fileServices;
            _userDicts = userDictionary;
            _dictionaryService = dictionaryService;
        }
        public async Task<Responce> CreateWordClause(CreateWordClauseDto model)
        {
            try
            {
                var m = _mapper.Map<WordClause>(model);
                m.AddedDate = DateTime.Now;
                var res = await _dbContext.WordClauses.AddAsync(m);
                
                await _dbContext.SaveChangesAsync();
                return ResponceGenerator.GetResponceModel(true, "", res.Entity);
            }catch(Exception e)
            {
                return ResponceGenerator.GetResponceModel(false, e.Message, null);
            }
        }

        public async Task<Responce> CreateWordClauseCategory(CreateWordClauseCategoryDto model)
        {
            try
            {
                var res = await _dbContext.WordClauseCategories.AddAsync(_mapper.Map<WordClauseCategory>(model));
                await _dbContext.SaveChangesAsync();
                return ResponceGenerator.GetResponceModel(true, "", res);
            }
            catch (Exception e)
            {
                return ResponceGenerator.GetResponceModel(false, e.Message, null);
            }
        }

        public async Task<Responce> DeleteWordClause(int id)
        {
            try
            {
                var res = await _dbContext.WordClauses.FindAsync(id);
                if (res != null)
                {
                    _dbContext.WordClauses.Remove(res);
                    await _dbContext.SaveChangesAsync();
                    return ResponceGenerator.GetResponceModel(true, "", res);
                }
                return ResponceGenerator.GetResponceModel(false, "", res);
            }catch(Exception e)
            {
                return ResponceGenerator.GetResponceModel(false, "", null);
            }
        }

        public async Task<Responce> DeleteWordClauseCategory(int id)
        {
            try
            {
                var res = await _dbContext.WordClauseCategories.FindAsync(id);
                if (res != null)
                {
                    _dbContext.WordClauseCategories.Remove(res);
                    await _dbContext.SaveChangesAsync();
                    return ResponceGenerator.GetResponceModel(true, "", res);
                }
                return ResponceGenerator.GetResponceModel(false, "", res);
            }
            catch (Exception e)
            {
                return ResponceGenerator.GetResponceModel(false, "", null);
            }
        }

        public async Task<Responce> EditWordClause(EditWordClauseDto model)
        {
            try
            {                
                var res = _mapper.Map<WordClause>(model);
                res.AddedDate = DateTime.Now;
                _dbContext.WordClauses.Update(res);
                await _dbContext.SaveChangesAsync();
                return ResponceGenerator.GetResponceModel(true, "", res);
            }
            catch(Exception e)
            {
                return ResponceGenerator.GetResponceModel(false, "", null);
            }
        }

        public async Task<Responce> EditWordClauseCategory(WordClauseCategoryDto model)
        {
            try
            {
                var res = _mapper.Map<WordClauseCategory>(model);
                _dbContext.WordClauseCategories.Update(res);
                await _dbContext.SaveChangesAsync();
                return ResponceGenerator.GetResponceModel(true, "", res);
            }
            catch (Exception e)
            {
                return ResponceGenerator.GetResponceModel(false, "", null);
            }
        }

        public List<WordClauseDto> GetAllWordClauseByDictionaryId(int id)
        {
            var res = _dbContext.WordClauses.Where(p => p.DictionaryId == id);
            if (res != null)
            {
                return _mapper.Map<List<WordClauseDto>>(res);
            }
            else
                return null;
        }

        public List<WordClauseDto> GetAllWordClauseById_and_DictionaryId(int id, int dictId)
        {
            var res = _dbContext.WordClauses.Select(p => p.Id == id && p.DictionaryId == dictId);
            if(res != null)
            {
                return _mapper.Map<List<WordClauseDto>>(res);
            }return null;
        }

        public List<WordClauseCategoryDto> GetAllWordClauseCategories()
        {
            string culture = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
            var res = _dbContext.WordClauseCategories.Include(p=>p.WordClauseCategoryTranslates);
            if (res != null)
            {

                var res1 = _mapper.Map<List<WordClauseCategoryDto>>(res);
                foreach(var item in res1)
                {
                    item.CategoryName = _dbContext.WordClauseCategoryTranslates.FirstOrDefault(p => p.LanguageCulture == culture && p.WordClauseCategoryId == item.Id)?.CategoryName;
                    item.ParenCategoryName = _dbContext.WordClauseCategoryTranslates.SingleOrDefault(p => p.LanguageCulture == culture && p.WordClauseCategoryId == item.ParentCategoryId)?.CategoryName ;
                    if (item.ParenCategoryName == null)
                        item.ParenCategoryName = "";
                }
                return res1;
            }
            return null;
        }

        public List<WordClauseCategoryDto> GetAllWordClauseCategoriesByDictID(int id)
        {
            string culture = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
            var res = _dbContext.WordClauseCategories.Include(p => p.WordClauseCategoryTranslates.FirstOrDefault(p => p.LanguageCulture == culture));            
            if (res != null)
            {
                var res1 = res.Select(p => p.Id == id);
                if(res1!=null && res1.Count() > 0)
                    return _mapper.Map<List<WordClauseCategoryDto>>(res1);
            }
            return null;
        }

        public List<WordClauseCategoryDto> GetAllWordClauseCategoriesById_and_DictId(int id, int dictId)
        {
            string culture = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
            var res = _dbContext.WordClauseCategories.Include(p => p.WordClauseCategoryTranslates.FirstOrDefault(p => p.LanguageCulture == culture));
            if (res != null)
            {
                var res1 = res.Select(p => p.Id == id);
                if (res1 != null && res1.Count() > 0)
                    return _mapper.Map<List<WordClauseCategoryDto>>(res1);
            }
            return null;
        }

        public async Task<List<WordClauseDto>> GetAllWordClausesAsync()
        {
            string culture = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
            var res = _dbContext.WordClauses;
            List<WordClauseDto> result = new List<WordClauseDto>();
            if (res != null)
            {
                foreach (var clause in res) {
                    var userName = _useManager.FindByIdAsync(clause.UserId)?.Result.Email;
                    var dto = _mapper.Map<WordClauseDto>(clause);
                    dto.UserName = userName==null?"":userName;

                    var catName = _dbContext.WordClauseCategories.Include(p => p.WordClauseCategoryTranslates).FirstOrDefault(p => p.Id == clause.CategoryId);
                    dto.CategoryName = catName == null ? "" : catName.WordClauseCategoryTranslates.FirstOrDefault(p => p.LanguageCulture == culture)?.CategoryName;

                    var dict = await _dbContext.Dictionaries.FindAsync(clause.DictionaryId);
                    dto.DictionaryName = dict == null ? "" : dict.Language;
                    result.Add(dto);
                }
                return result;
            }
            return null;
        }

        public async Task<List<WordClauseDto>> GetAllWordClausesbyUserAsync(string userId)
        {
            
            string culture = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
            var res = _dbContext.WordClauses.Where(p => p.UserId == userId);
            List<WordClauseDto> result = new List<WordClauseDto>();
            if (res != null)
            {
                foreach (var clause in res)
                {
                    var userName = _useManager.FindByIdAsync(clause.UserId)?.Result.Email;
                    var dto = _mapper.Map<WordClauseDto>(clause);
                    dto.UserName = userName == null ? "" : userName;

                    var catName = _dbContext.WordClauseCategories.Include(p => p.WordClauseCategoryTranslates).FirstOrDefault(p => p.Id == clause.CategoryId);
                    dto.CategoryName = catName == null ? "" : catName.WordClauseCategoryTranslates.FirstOrDefault(p => p.LanguageCulture == culture)?.CategoryName;

                    var dict = await _dbContext.Dictionaries.FindAsync(clause.DictionaryId);
                    dto.DictionaryName = dict == null ? "" : dict.Language;
                    result.Add(dto);
                }
                return result;
            }
            return null;
        }


        public List<AudioFileForClauseDto> GetAudioFileForClausebyID(int id, bool approve)
        {
            var res = _dbContext.AudioFileForClauses.Where(p => p.ClauseId == id && p.IsApproved == approve);
            if (res != null)
            {
                return _mapper.Map<List<AudioFileForClauseDto>>(res);
            }
            return null;
        }

        public List<AudioFileForClauseDto> GetAudioFileForClausebyID(int id)
        {
            var res = _dbContext.AudioFileForClauses.Where(p => p.ClauseId == id);
            if(res != null)
            {
                return _mapper.Map<List<AudioFileForClauseDto>>(res);
            }
            return null;
        }



        public async Task<EditWordClauseDto> GetWordClauseByIdAsync(int id)
        {            
            try
            {
                var res = await _dbContext.WordClauses.FindAsync(id);
                
                if (res != null)
                {
                    var result = _mapper.Map<EditWordClauseDto>(res);
                    string culture = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
                    var category = _dbContext.WordClauseCategoryTranslates.FirstOrDefault(p => p.WordClauseCategoryId == res.CategoryId                                                                                             && p.LanguageCulture == culture);
                    if (category != null)
                    {
                        result.CategoryName = category.CategoryName;
                    }
                    return result;
                }
                
                return null;
            }catch(Exception e)
            {
                return null;
            }
        }

        public async Task<WordClauseCategoryDto> GetWordClauseCategoryByIdAsync(int id)
        {
            string culture = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
            var res = await _dbContext.WordClauseCategories.Include(p => p.WordClauseCategoryTranslates).SingleOrDefaultAsync(p => p.Id == id);
            if (res != null)
            {
                return _mapper.Map<WordClauseCategoryDto>(res);
            }
            return null;
        }

        public async Task<Responce> CreateAudiFileForClause(CreateAudioFileForClauseDto model)
        {
            if (model.ArabVoiceFile == null && model.OtherVoiceFile == null)
            {
                return ResponceGenerator.GetResponceModel(false,"", null);
            }
            AudioFileForClause file = new AudioFileForClause
            {
                ClauseId = model.ClauseId
            };

            if (model.ArabVoiceFile != null)
                file.ArabVoice = await _fileServices.UploadImage(model.ArabVoiceFile, SD.clausesFilePath);
            if (model.OtherVoiceFile != null)
                file.OtherVoice = await _fileServices.UploadImage(model.OtherVoiceFile, SD.clausesFilePath);
            try
            {
                var res = await _dbContext.AudioFileForClauses.AddAsync(file);
                await _dbContext.SaveChangesAsync();
                return ResponceGenerator.GetResponceModel(true, "", model);
            }catch(Exception e) {
                _fileServices.DeleteImage(file.ArabVoice);
                _fileServices.DeleteImage(file.OtherVoice);
                return ResponceGenerator.GetResponceModel(false, e.Message, model);
            }           
        }

        public async Task<Responce> DeleteVoice(int id)
        {
            var res = await _dbContext.AudioFileForClauses.FindAsync(id);
            if (res != null)
            {
                try
                {
                    _fileServices.DeleteImage(res.ArabVoice);
                    _fileServices.DeleteImage(res.OtherVoice);
                    _dbContext.AudioFileForClauses.Remove(res);
                    await _dbContext.SaveChangesAsync();
                    return ResponceGenerator.GetResponceModel(true, "", null);
                }
                catch (Exception e)
                {
                    return ResponceGenerator.GetResponceModel(false, e.Message, null);
                }
            }
            return ResponceGenerator.GetResponceModel(true, "", null);
        }

        public async Task<Responce> ApproveVoice(int id, bool approve)
        {
            var res = await _dbContext.AudioFileForClauses.FindAsync(id);
            try
            {
                if (res != null)
                {
                    res.IsApproved = approve;
                    _dbContext.AudioFileForClauses.Update(res);
                    await _dbContext.SaveChangesAsync();
                    return ResponceGenerator.GetResponceModel(true, "", null);
                }
                return ResponceGenerator.GetResponceModel(false, "", null);
            }
            catch(Exception e)
            {
                return ResponceGenerator.GetResponceModel(false, e.Message, null);
            }
        }

        public List<WordClauseDto> GetRandomWordClauses(int n)
        {
            try
            {
                var dictId = _userDicts.GetDictionaryId();
                string culture = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
                var res = _dbContext.WordClauses.Where(p => p.DictionaryId == dictId && p.IsApproved == true).Include(p => p.AudioFiles.Where(p => p.IsApproved == true)).Include(p => p.WordClauseCategory).ThenInclude(p => p.WordClauseCategoryTranslates).ToList();
                _dictionaryService.Shuffle(res);
                res = res.Take(n).ToList();
                //string DictName = _dbContext.Dictionaries.Find(dictId)?.Language;
                if (res != null)
                {
                    var r = _mapper.Map<List<WordClauseDto>>(res);                    
                    foreach(var item in r)
                    {
                        item.CategoryName = res.Where(p => p.Id == item.Id).First().WordClauseCategory.WordClauseCategoryTranslates.FirstOrDefault(p => p.LanguageCulture == culture).CategoryName;
                        var audio = res.FirstOrDefault(p => p.Id == item.Id)?.AudioFiles;
                        if (audio != null && audio.Count > 0) {
                            item.ArabVoice = audio.ToArray()[0].ArabVoice;
                            item.OtherVoice = audio.ToArray()[0].OtherVoice;
                        }
                    }
                    return r;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<WordClauseDto> GetAllWordClausesWithDictId(int id)
        {
            string culture = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
            var res = _dbContext.WordClauses.Where(p => p.DictionaryId == id && p.IsApproved == true).Include(p => p.AudioFiles.Where(p => p.IsApproved == true));
            List<WordClauseDto> result = new List<WordClauseDto>();
            if (res != null)
            {
                foreach (var clause in res)
                {
                    var userName = _useManager.FindByIdAsync(clause.UserId)?.Result.Email;
                    var dto = _mapper.Map<WordClauseDto>(clause);
                    dto.UserName = userName == null ? "" : userName;

                    var catName = _dbContext.WordClauseCategories.Include(p => p.WordClauseCategoryTranslates).FirstOrDefault(p => p.Id == clause.CategoryId);
                    dto.CategoryName = catName == null ? "" : catName.WordClauseCategoryTranslates.FirstOrDefault(p => p.LanguageCulture == culture)?.CategoryName;

                    var audio = res.FirstOrDefault(p => p.Id == clause.Id)?.AudioFiles;
                    if (audio != null && audio.Count > 0)
                    {
                        dto.ArabVoice = audio.ToArray()[0].ArabVoice;
                        dto.OtherVoice = audio.ToArray()[0].OtherVoice;
                    }
                    result.Add(dto);
                }
                return result;
            }
            return null;
        }

        public async Task<EditWordClauseDto> IncreaseViewed(int wordClauseId)
        {
            var word = await _dbContext.WordClauses.FindAsync(wordClauseId);
            int dictId = _userDicts.GetDictionaryId();
            if (word.DictionaryId != dictId)
                return null;
            if (word != null)
            {
                if (word.Viewed == null)
                    word.Viewed = 0;
                word.Viewed += 1;
                _dbContext.WordClauses.Update(word);
                await _dbContext.SaveChangesAsync();
                var result = _mapper.Map<EditWordClauseDto>(word);
                string culture = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
                var category = _dbContext.WordClauseCategoryTranslates.FirstOrDefault(p => p.WordClauseCategoryId == word.CategoryId && p.LanguageCulture == culture);
                if (category != null)
                {
                    result.CategoryName = category.CategoryName;
                }
                return result;
            }
            return null;
        }
    }
}
