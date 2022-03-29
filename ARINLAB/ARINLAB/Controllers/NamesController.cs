﻿using ARINLAB.Models;
using ARINLAB.Services;
using ARINLAB.Services.ImageService;
using ARINLAB.Services.News;
using ARINLAB.Services.Ratings;
using ARINLAB.Services.SessionService;
using AutoMapper;
using DAL.Models.Dto.NamesDTO;
using DAL.Models.Dto.NewsModelDTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;

namespace ARINLAB.Controllers
{
    public class UserDictionaryModel
    {
        public int DictionaryId { get; set; }
    }
    public class NamesController : Controller
    {
        private readonly UserDictionary _userDictionary;
        private readonly INamesService _nameService;
        private readonly Services.IDictionaryService _dictService;
        private readonly IRatingService _ratingServices;
        private readonly IImageService _imageService;
        private readonly IMapper _mapper;
        private readonly INewsService _newsService;
        public NamesController(UserDictionary userDict, INamesService namesService, 
                            Services.IDictionaryService dictionaryService, IMapper mapper,
                            IRatingService ratingServices, IImageService imageService,
                            INewsService newsService)
        {
            _nameService = namesService;
            _dictService = dictionaryService;
            _userDictionary = userDict;
            _ratingServices = ratingServices;
            _imageService = imageService;
            _mapper = mapper;
            _newsService = newsService;
        }
       

        public IActionResult Indexall()
        {
            var res = _userDictionary.GetDictionaryId();
            UserDictionaryModel model = new UserDictionaryModel();
            if((int)res == -1)
            {
                model.DictionaryId = 1;
            }
            else
            {
                model.DictionaryId = (int)res;
            }
            return View(model);
        }

        public async Task<IActionResult> DetailsAsync(int id, bool ratingResult = false)
        {
            //var res = await _nameService.GetNameByIdAsync(id);
            var res = await _nameService.IncreaseViewed(id);
            if (res == null)
                return RedirectToAction("Indexall");
            
            //if (string.IsNullOrEmpty(res.ImageForShare))
            //{
                res.ImageForShare = _imageService.CreateImageForExport(res.ArabName, res.OtherName);
               // await _nameService.EditNameAsync(_mapper.Map<NamesDto>(res));
            //}
            NamesImagesViewModel model = new();
            model.Id = id;
            model.ArabName = res.ArabName;
            model.OtherName = res.OtherName;
            model.DictName = res.DictionaryName;
            model.Viewed = res.Viewed;
            var file = await _nameService.GetAllNamesImagesByNameIdAsync(id);
            ViewBag.Rating = _ratingServices.GetRatingForName(id);
            ViewBag.Model = model;
            ViewBag.RatingResult = ratingResult;
            ViewBag.ExportImage = res.ImageForShare;
            ViewBag.News = (List<NewsDTO>)(_newsService.GetFourPublishNews().ToList());

            return View(file);
        }

        public async Task<IActionResult> Share(int id)
        {
            try
            {
                var res = await _nameService.GetNameByIdAsync(id);
                if (res == null)
                    return RedirectToAction("Indexall");
                if (string.IsNullOrEmpty(res.ImageForShare))
                {
                    res.ImageForShare = _imageService.CreateImageForExport(res.ArabName.Reverse()+"", res.OtherName);
                    await _nameService.EditNameAsync(_mapper.Map<NamesDto>(res));
                }
                return Redirect($"~{res.ImageForShare}");
            }
            catch (Exception e)
            {
                return RedirectToAction("Indexall");
            }            
        }

        
        public async Task<IActionResult> SetRatingAsync(float Rating, int NameId)
        {
            var responce = await _ratingServices.SetRatingForNameAsync(Rating, NameId);
            try
            {
                var res = await _nameService.GetNameByIdAsync(NameId);

                if (res != null)
                {
                    NamesImagesViewModel model = new();
                    model.Id = NameId;
                    model.ArabName = res.ArabName;
                    model.OtherName = res.OtherName;
                    model.DictName = res.DictionaryName;
                    var file = await _nameService.GetAllNamesImagesByNameIdAsync(NameId);
                    ViewBag.Rating = _ratingServices.GetRatingForName(NameId);
                    ViewBag.Model = model;
                    ViewBag.RatingResult = responce.IsSuccess;
                    return RedirectToAction("Details", new { id = NameId, ratingResult = responce.IsSuccess });
                }
                else
                {
                    return RedirectToAction("Details", new { id = NameId, ratingResult = responce.IsSuccess });
                }
            }
            catch (Exception e)
            {
                return RedirectToAction("Indexall");
            }
        }

        public IActionResult NameImageView(int imageId, string imageUrl)
        {
            _nameService.IncreaseViewedImage(imageId);
            return Redirect($"~/images/Names/{imageUrl}");

        }
    }
}
