using ARINLAB.Services.ImageService;
using ARINLAB.Services.SessionService;
using AutoMapper;
using DAL.Data;
using DAL.Models;
using DAL.Models.Dto.NamesDTO;
using DAL.Models.ResponceModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARINLAB.Services
{
    public class NamesService : INamesService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IImageService _imageService;
        private readonly IMapper _mapper;
        private readonly IDictionaryService _dictionaryService;
        private readonly UserDictionary _userDict;
        private readonly IImageService _fileService;

        public NamesService(ApplicationDbContext applicationDb, IImageService imageService, 
                            IMapper mapper, IDictionaryService dict, UserDictionary userDictionary,
                            IImageService fileService)
        {
            _dbContext = applicationDb;
            _imageService = imageService;
            _mapper = mapper;
            _dictionaryService = dict;
            _userDict = userDictionary;
            _fileService = fileService;
        }

        public async Task<Responce> ApproveImage(int image_id, bool approve)
        {
            var res = await _dbContext.NameImages.FindAsync(image_id);
            if(res != null)
            {
                res.IsApproved = approve;
                _dbContext.NameImages.Update(res);
                await _dbContext.SaveChangesAsync();
                return ResponceGenerator.GetResponceModel(true,"", res);
            }
            return ResponceGenerator.GetResponceModel(false);
        }

        public async Task<Responce> CreateImageforNameAsync(CreateNameImagesDto image)
        {
            NameImages model = new();            
            try
            {
                if(image != null)
                {
                    model.NamesId = image.NamesId;
                    model.UserId = image.UserId;
                    model.IsApproved = true;
                    if (image.ImageUri != null)
                    {
                        model.ImageUri = await _imageService._UploadImage(image.ImageUri, "Names");
                        _dbContext.NameImages.Add(model);
                        _dbContext.SaveChanges();
                        return ResponceGenerator.GetResponceModel(true, "Success", model);
                    }
                }                
            }
            catch(Exception e)
            {
                return ResponceGenerator.GetResponceModel(false, e.Message, image);
            }
            return ResponceGenerator.GetResponceModel(false, "Image is not set", image);
        }

        public async Task<Responce> CreateNameAsync(CreateNamesDto name)
        {
            try {
                var res = _mapper.Map<Names>(name);
                if (name.ArabForm != null)
                    res.ArabVoice = await _fileService.UploadImage(name.ArabForm, SD.NamesPath);
                if (name.OtherForm!= null)
                    res.OtherVoice = await _fileService.UploadImage(name.OtherForm, SD.NamesPath);
                _dbContext.Names.Add(res);
                await _dbContext.SaveChangesAsync();
                return ResponceGenerator.GetResponceModel(true, "Success", name);
            }
            catch(Exception e)
            {
                return ResponceGenerator.GetResponceModel(false, e.Message, name);
            }
        }

        public async Task<Responce> DeleteImageforNameAsync(int id)
        {
            var res = await _dbContext.NameImages.FindAsync(id);
            if (res != null)
            {
                _imageService._DeleteImage(res.ImageUri, "Names");
                _dbContext.NameImages.Remove(res);
                await _dbContext.SaveChangesAsync();
                return ResponceGenerator.GetResponceModel(false, "", res);
            }
            return ResponceGenerator.GetResponceModel(false, $"Does not found the image for word with id={id}", null);
        }

        public async Task<Responce> DeleteNameAsync(int id)
        {
            try
            {
                var res = await _dbContext.Names.FindAsync(id);
                if (res != null)
                { 
                    var images = _dbContext.NameImages.Where(p => p.NamesId == id);
                    _dbContext.Names.Remove(res);
                    await _dbContext.SaveChangesAsync();
                    foreach (var image in images)
                    {
                        _imageService._DeleteImage(image.ImageUri, "Names");
                        _dbContext.NameImages.Remove(image);                        
                    }
                   
                   _fileService.DeleteImage(res.ArabVoice);
                   _fileService.DeleteImage(res.OtherVoice);
                    await _dbContext.SaveChangesAsync();
                    return ResponceGenerator.GetResponceModel(true, "Success", res);
                }
            }catch(Exception e)
            {
                return ResponceGenerator.GetResponceModel(false, e.Message, null);
            }
            return ResponceGenerator.GetResponceModel(false, $"Failed to find Name with id={id}", null);
        }
        public async Task<Responce> EditNameAsync(NamesDto name)
        {
            try
            {
                name.NameImages = null;
                var result = await _dbContext.Names.FindAsync(name.Id);
                if (result != null)
                {
                    result.ArabName = name.ArabName;
                    result.DictionaryId = name.DictionaryId;
                    result.IsApproved = name.IsApproved;
                    result.UserId = name.UserId;
                    result.ImageForShare = name.ImageForShare;
                    result.OtherName = name.OtherName;
                    if (name.ArabForm != null)
                    {
                        _fileService.DeleteImage(result.ArabVoice);
                        result.ArabVoice = await _fileService.UploadImage(name.ArabForm, SD.NamesPath);
                    }

                    if (name.OtherForm != null)
                    {
                        _fileService.DeleteImage(result.OtherVoice);
                        result.OtherVoice = await _fileService.UploadImage(name.OtherForm, SD.NamesPath);
                    }
                    _dbContext.Update(result);
                    _dbContext.SaveChanges();
                    return ResponceGenerator.GetResponceModel(true, "", result);
                }
                return ResponceGenerator.GetResponceModel(false, "", null);
            }
            catch(Exception e)
            {
                return ResponceGenerator.GetResponceModel(false, e.Message, null);
            }
        }

        public List<NamesDto> GetAllNames()
        {
            try
            {
                var res = _mapper.Map<List<NamesDto>>(_dbContext.Names);
                var dicts = new List<Dictionary>((IEnumerable<Dictionary>)_dictionaryService.GetAllDictionaries().Data);
                foreach (var name in res)
                {
                    name.DictionaryName = dicts.SingleOrDefault(p => p.Id == name.DictionaryId)?.Language;
                }
                return res;
            }catch(Exception e)
            {
                return null;
            }
        }
        public List<NamesDto> GetAllNames(string userId)
        {
            try
            {
                var res = _mapper.Map<List<NamesDto>>(_dbContext.Names.Where(p => p.UserId == userId));
                var dicts = new List<Dictionary>((IEnumerable<Dictionary>)_dictionaryService.GetAllDictionaries().Data);
                foreach (var name in res)
                {
                    name.DictionaryName = dicts.SingleOrDefault(p => p.Id == name.DictionaryId)?.Language;
                }
                return res;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public async Task<List<NameImagesDto>> GetAllNamesImagesByNameIdAsync(int id)
        {
            try
            {
                var result = new List<NameImages>(_dbContext.NameImages.Where(p => p.NamesId == id));

                return _mapper.Map<List<NameImagesDto>>(result);
            }catch(Exception e)
            {
                return null;
            }            
        }

        public List<NamesDto> GetAllNamesWithDictId(int id)
        {
            try
            {
                var res = _dbContext.Names.Where(p => p.DictionaryId == id && p.IsApproved == true);
                return _mapper.Map<List<NamesDto>>(res);
            }catch(Exception e)
            {
                return new List<NamesDto>();
            }
        }

        public async Task<NamesDto> GetNameByIdAsync(int id)
        {
            try
            {
                var res = await _dbContext.Names.FindAsync(id);
                if (res != null)
                {
                    var data = _mapper.Map<NamesDto>(res);
                    data.DictionaryName = _dictionaryService.GetDictionaryNameById(data.DictionaryId);
                    return data;
                }
            }catch(Exception e)
            {
                return null;
            }
            return null;
        }

        public async Task<NamesDto> GetNameByIdAsync(string userId, int id)
        {
            try
            {
                var res = await _dbContext.Names.FindAsync(id);                
                if (res != null)
                {
                    if (res.UserId != userId)
                        return null;
                    var data = _mapper.Map<NamesDto>(res);
                    data.DictionaryName = _dictionaryService.GetDictionaryNameById(data.DictionaryId);
                    return data;
                }
            }
            catch (Exception e)
            {
                return null;
            }
            return null;
        }

        public async Task<NameImagesDto> GetNameImageByImageIdAsync(int id)
        {
            try
            {
                var res = await _dbContext.NameImages.FindAsync(id);
                if(res != null)
                {
                    return _mapper.Map<NameImagesDto>(res);
                }
            }catch(Exception e)
            {
                return null;
            }
            return null;
        }

        public List<NamesDto> GetRandom_N_Names(int n)
        {
            try
            {
                var dictId = _userDict.GetDictionaryId();
                Random rnd = new Random(DateTime.UtcNow.Millisecond);
                int rn = rnd.Next();
                var res = _dbContext.Names.Where(p => p.DictionaryId == dictId && p.IsApproved == true).ToList();
                _dictionaryService.Shuffle(res);
                res = res.Take(n).ToList();
                if (res != null)
                {                   
                    var r = _mapper.Map<List<NamesDto>>(res);
                    //foreach(var item in r)
                    //{
                        //item.ArabName = item.ArabName + "__" + item.
                   // }
                    return r;
                }
                else
                {
                    return null;
                }
            }catch(Exception e)
            {
                return null;
            }
        }

        public async Task<Responce> SetNameImageForShare(int nameId, string ImageLocation)
        {
            try
            {
                var res = await _dbContext.Names.FindAsync(nameId);
                if (res != null && string.IsNullOrEmpty(res.ImageForShare))
                {
                    res.ImageForShare = ImageLocation;
                    _dbContext.Names.Update(res);
                    await _dbContext.SaveChangesAsync();
                    return ResponceGenerator.GetResponceModel(true, "", res);
                }
            }catch(Exception e)
            {

            }
            return ResponceGenerator.GetResponceModel(false, "Unknown eror", null);
        }
        public async Task<NamesDto> IncreaseViewed(int namesId)
        {
            var name = await _dbContext.Names.FindAsync(namesId);
            if (name != null)
            {
                if (name.Viewed == null)
                    name.Viewed = 0;
                name.Viewed += 1;
                _dbContext.Names.Update(name);
                await _dbContext.SaveChangesAsync();
                var data = _mapper.Map<NamesDto>(name);
                data.DictionaryName = _dictionaryService.GetDictionaryNameById(data.DictionaryId);
                return data;                
            }
            return null;
        }

        public async Task<bool> IncreaseViewedImage(int namesImageId)
        {
            var nameImage =  _dbContext.NameImages.Find(namesImageId);
            if (nameImage != null)
            {
                if (nameImage.Viewed == null)
                    nameImage.Viewed = 0;
                nameImage.Viewed += 1;
                _dbContext.NameImages.Update(nameImage);
                 _dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteVoice(int id)
        {
            var res = await _dbContext.Names.FindAsync(id);
            if (res == null)
                return false;

            _fileService.DeleteImage(res.ArabVoice);
            res.ArabVoice = null;

            _fileService.DeleteImage(res.OtherVoice);
            res.OtherVoice = null;

            _dbContext.Names.Update(res);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
