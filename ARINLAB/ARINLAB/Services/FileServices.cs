using ARINLAB.Services.ImageService;
using AutoMapper;
using DAL.Data;
using DAL.Models;
using DAL.Models.Dto;
using DAL.Models.ResponceModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARINLAB.Services
{
    public class FileServices
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IImageService _fileService;
        public FileServices(ApplicationDbContext dbContext, IMapper mapper, IImageService imageService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _fileService = imageService;
        }

      

        public async Task<Responce> CreateAudioFileAsync(CreateAudioFileDto newFile)
        {
            try
            {
                var newF = _mapper.Map<AudioFile>(newFile);
                newF.IsApproved = true;
                await _dbContext.AudioFiles.AddAsync(newF);
                await _dbContext.SaveChangesAsync();
                return ResponceGenerator.GetResponceModel(true, "", null);
            }catch(Exception e)
            {
                return ResponceGenerator.GetResponceModel(false, e.Message, newFile);
            }
        }

        public async Task<Responce> CreateClauseAudioFileAsync(CreateAudioFileForClauseDto newFile)
        {
            try
            {
                var newF = _mapper.Map<AudioFileForClause>(newFile);
                newF.IsApproved = true;
                await _dbContext.AudioFileForClauses.AddAsync(newF);
                await _dbContext.SaveChangesAsync();
                return ResponceGenerator.GetResponceModel(true, "", null);
            }
            catch (Exception e)
            {
                return ResponceGenerator.GetResponceModel(false, e.Message, newFile);
            }
        }

        public async Task<Responce> DeleteVoiceFile(int id)
        {
            var res = await _dbContext.AudioFiles.FindAsync(id);
            if(res != null)
            {
                try
                {
                    _fileService.DeleteImage(res.ArabVoice);
                    _fileService.DeleteImage(res.OtherVoice);
                    _dbContext.AudioFiles.Remove(res);
                    await _dbContext.SaveChangesAsync();
                    return ResponceGenerator.GetResponceModel(true, "", null);
                }catch(Exception e)
                {
                    return ResponceGenerator.GetResponceModel(false, e.Message, null);
                }
            }
            return ResponceGenerator.GetResponceModel(true, "", null);
        }

        public async Task<Responce> DeleteClauseVoiceFile(int id)
        {
            var res = await _dbContext.AudioFileForClauses.FindAsync(id);
            if (res != null)
            {
                try
                {
                    _fileService.DeleteImage(res.ArabVoice);
                    _fileService.DeleteImage(res.OtherVoice);
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
    }
}
