using DAL.Models.Dto.NamesDTO;
using DAL.Models.ResponceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARINLAB.Services
{
    public interface INamesService 
    {
        public Task<NamesDto> GetNameByIdAsync(int id);
        public Task<NamesDto> GetNameByIdAsync(string userId, int id);
        public List<NamesDto> GetAllNames();
        public List<NamesDto> GetAllNames(string userId);
        public Task<Responce> DeleteNameAsync(int id);
        public Task<Responce> EditNameAsync(NamesDto name);
        public Task<Responce> CreateNameAsync(CreateNamesDto name);
        public Task<List<NameImagesDto>> GetAllNamesImagesByNameIdAsync(int id);
        public Task<NameImagesDto> GetNameImageByImageIdAsync(int id);
        public Task<Responce> DeleteImageforNameAsync(int id);
        public Task<Responce> CreateImageforNameAsync(CreateNameImagesDto image);

        public  Task<Responce> ApproveImage(int image_id, bool approve);

        public List<NamesDto> GetRandom_N_Names(int n);

        public List<NamesDto> GetAllNamesWithDictId(int id);

        public Task<Responce> SetNameImageForShare(int nameId, string ImageLocation);
        public Task<NamesDto> IncreaseViewed(int namesId);
        public Task<bool> IncreaseViewedImage(int namesImageId);
        public Task<bool> DeleteVoice(int id);
    }
}
