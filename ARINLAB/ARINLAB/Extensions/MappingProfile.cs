using AutoMapper;
using DAL.Models;
using DAL.Models.Configs;
using DAL.Models.Dto;
using DAL.Models.Dto.MenuModelDTO;
using DAL.Models.Dto.NamesDTO;
using DAL.Models.Dto.NewsModelDTO;
using DAL.Models.Menu;
using DAL.Models.News;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARINLAB.Extensions
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Language, LanguageDto>().ReverseMap();
            CreateMap<WordDto, Word>().ReverseMap();
            CreateMap<WordSentencesDto, WordSentences>().ReverseMap();
            CreateMap<CreateWordDto, Word>().ReverseMap();
            CreateMap<CreateWordDto, WordSentences>().ReverseMap();
            CreateMap<EditWordDto, Word>().ReverseMap();
            CreateMap<EditWordDto, WordDto>().ReverseMap();
            CreateMap<EditWordSentencesDto, WordSentences>().ReverseMap();
            CreateMap<CreateDictionaryDto, Dictionary>().ReverseMap();
            //CreateMap<CreateAudioFileDto, AudioFile>().ReverseMap();
            CreateMap<CreateWordSentencesDto, WordSentences>().ReverseMap();
            CreateMap<CreateWordClauseDto, WordClause>().ReverseMap();
            CreateMap<EditWordClauseDto, WordClauseDto>().ReverseMap();
            CreateMap<WordClause, WordClauseDto>().ReverseMap();
            CreateMap<WordClauseCategory, CreateWordClauseCategoryDto>().ReverseMap();
            CreateMap<WordClauseCategoryDto, WordClauseCategory>().ReverseMap();
            CreateMap<Language, LanguageDto>().ReverseMap();
            CreateMap<EditWordClauseDto, WordClause>().ReverseMap();
            CreateMap<AudioFileForClause, AudioFileForClauseDto>().ReverseMap();

            CreateMap<CountryDto, Country>().ReverseMap();

           //CreateMap<AudioFile, AudioFileDto>().ReverseMap();

            CreateMap<News, NewsDTO>();
            CreateMap<NewsDTO, News>();
            CreateMap<News, CreateNewsDTO>();
            CreateMap<CreateNewsDTO, News>();
            CreateMap<News, EditNewsDTO>();
            CreateMap<EditNewsDTO, News>();

            CreateMap<NewsTranslate, NewsTranslateDTO>();
            CreateMap<NewsTranslateDTO, NewsTranslate>();

            CreateMap<NewsCategory, NewsCategoryDTO>();
            CreateMap<NewsCategoryDTO, NewsCategory>();
            CreateMap<NewsCategory, CreateNewsCategoryDTO>();
            CreateMap<CreateNewsCategoryDTO, NewsCategory>();
            CreateMap<NewsCategory, EditNewsCategoryDTO>();
            CreateMap<EditNewsCategoryDTO, NewsCategory>();

            CreateMap<NewsCategoryTranslate, NewsCategoryTranslateDTO>();
            CreateMap<NewsCategoryTranslateDTO, NewsCategoryTranslate>();

            CreateMap<Pages, PagesDTO>();
            CreateMap<PagesDTO, Pages>();
            CreateMap<Pages, CreatePagesDTO>();
            CreateMap<CreatePagesDTO, Pages>();
            CreateMap<Pages, EditPageDTO>();
            CreateMap<EditPageDTO, Pages>();

            CreateMap<PagesTranslate, PagesTranslateDTO>();
            CreateMap<PagesTranslateDTO, PagesTranslate>();

            CreateMap<Menu, MenuDTO>();
            CreateMap<MenuDTO, Menu>();
            CreateMap<Menu, CreateMenuDTO>();
            CreateMap<CreateMenuDTO, Menu>();
            CreateMap<Menu, EditMenuDTO>();
            CreateMap<EditMenuDTO, Menu>();

            CreateMap<Menu, MenuHierarchyDTO>();
            CreateMap<MenuHierarchyDTO, Menu>();

            CreateMap<MenuTranslate, MenuTranslateDTO>();
            CreateMap<MenuTranslateDTO, MenuTranslate>();

            CreateMap<Names, NamesDto>().ReverseMap();
            CreateMap<CreateNamesDto, Names>().ReverseMap();
            CreateMap<NameImages, NameImagesDto>().ReverseMap();
            CreateMap<CreateNameImagesDto, NameImages>().ReverseMap();

            CreateMap<Contact, CreateContactDto>().ReverseMap();
            CreateMap<Bag, CreateBagDto>().ReverseMap();


            CreateMap<ApplicationUser, EditUser>().ReverseMap();
        }
    }
}
