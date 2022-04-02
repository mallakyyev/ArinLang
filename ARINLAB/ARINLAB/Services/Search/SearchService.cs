﻿using ARINLAB.Models;
using AutoMapper;
using DAL.Data;
using DAL.Models.Dto;
using DAL.Models.Dto.NamesDTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ARINLAB.Services.Search
{
    public class SearchService : ISearchService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        public SearchService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<WordClauseDto> SearchClauses(string term, int dictId)
        {
            try
            {
                List<WordClauseDto> result = new List<WordClauseDto>();
                string culture = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;                
                var res = (_dbContext.WordClauses.Where(p => p.IsApproved == true && p.DictionaryId == dictId
                                                                && (p.ArabClause.ToLower().Contains(term.ToLower())
                                                                || p.ArabReader.ToLower().Contains(term.ToLower())
                                                                || p.OtherClause.ToLower().Contains(term.ToLower())
                                                                || p.OtherReader.ToLower().Contains(term.ToLower()))));
                
                foreach (var item in res) {
                    var catName = _dbContext.WordClauseCategories.Include(p => p.WordClauseCategoryTranslates).FirstOrDefault(p => p.Id == item.CategoryId);
                    var dto = _mapper.Map<WordClauseDto>(item);
                    dto.CategoryName = catName == null ? "" : catName.WordClauseCategoryTranslates.FirstOrDefault(p => p.LanguageCulture == culture)?.CategoryName;
                    result.Add(dto);
                }
                return result;
            } catch (Exception e)
            {
                return new List<WordClauseDto>();
            }

        }

        public List<NamesDto> SearchNames(string term, int dictId)
        {
            try
            {
                return _mapper.Map<List<NamesDto>>(_dbContext.Names.Where(p => p.IsApproved == true && p.DictionaryId == dictId 
                                                                         && (p.ArabName.ToLower().Contains(term.ToLower())
                                                                         || p.OtherName.ToLower().Contains(term.ToLower()))));
            }catch(Exception e)
            {
                return new List<NamesDto>();            
            }
        }

        public List<WordDto> SearchWords(string term, int dictId)
        {
            try
            {
                return _mapper.Map<List<WordDto>>(_dbContext.Words.Where(p => p.IsApproved == true && p.DictionaryId == dictId
                                                                  && (p.OtherWord.ToLower().Contains(term.ToLower())
                                                                  || p.ArabWord.ToLower().Contains(term.ToLower()))));
            }catch(Exception e)
            {
                return new List<WordDto>();
            }
        }
    }
}
