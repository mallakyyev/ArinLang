using AutoMapper;
using DAL.Data;
using DAL.Models;
using DAL.Models.Dto;
using DAL.Models.ResponceModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ARINLAB.Services
{
    public class DictionaryService : IDictionaryService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        public DictionaryService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public Responce CreateDictionary(CreateDictionaryDto createModel)
        {
            Responce result = new Responce();
            try
            {
                var newDict = _mapper.Map<Dictionary>(createModel);
                var data = _dbContext.Dictionaries.Add(newDict);
                _dbContext.SaveChanges();
                if (data.IsKeySet)
                {
                    return ResponceGenerator.GetResponceModel(true, "", data.Entity);
                }else
                {
                    return ResponceGenerator.GetResponceModel(false, "Could not add Dictionary", newDict);
                }

            }catch(Exception e)
            {
               return ResponceGenerator.GetResponceModel(false, e.Message, null);
            }
        }

        public async Task<Responce> DeleteDictionaryAsync(int id)
        {
            try
            {
                var result = await _dbContext.Dictionaries.FindAsync(id);
                if(result != null)
                {
                    var data = _dbContext.Dictionaries.Remove(result);
                    await _dbContext.SaveChangesAsync();
                    return ResponceGenerator.GetResponceModel(true, "", data);
                }
                else
                {
                    return ResponceGenerator.GetResponceModel(true, "No Data Found", null);
                }
            }catch(Exception e)
            {
                return ResponceGenerator.GetResponceModel(false, e.Message, null);
            }
        }

        public Responce EditDictionary(Dictionary edit)
        {
            Responce result = new Responce();
            try
            {              
                var data = _dbContext.Dictionaries.Update(edit);
                _dbContext.SaveChanges();
                return ResponceGenerator.GetResponceModel(true, "", data.Entity);                
            }
            catch (Exception e)
            {
                return ResponceGenerator.GetResponceModel(false, e.Message, null);
            }
        }

        public Responce GetAllDictionaries()
        {
            var result = _dbContext.Dictionaries;
            return ResponceGenerator.GetResponceModel(true, "", new List<Dictionary>(result));
        }

        public DAL.Models.Dictionary GetDictionary(int id)
        {
            var res = _dbContext.Dictionaries.Find(id);
            if (res != null)
                return res;
            return null;
        }

        public string GetDictionaryNameById(int id)
        {
            var dict = _dbContext.Dictionaries.Find(id);
            string culture = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
            if (dict != null)
            {
                if (culture == "ar")
                    return dict.ArabTranslate;
                else
                    return dict.Language;
            }
            return "";
        }

        public void Shuffle<T>(IList<T> list)
        {
            Random rng = new Random(DateTime.UtcNow.Millisecond);
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
