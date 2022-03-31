using DAL.Models.Dto;
using DAL.Models.ResponceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARINLAB.Services
{
    public interface IDictionaryService
    {
        public Responce CreateDictionary(CreateDictionaryDto createModel);
        public Responce GetAllDictionaries();
        public Responce EditDictionary(DAL.Models.Dictionary edit);
        public Task<Responce> DeleteDictionaryAsync(int id);
        public string GetDictionaryNameById(int id);
        public DAL.Models.Dictionary GetDictionary(int id);
        public void Shuffle<T>(IList<T> list);
    }
}
