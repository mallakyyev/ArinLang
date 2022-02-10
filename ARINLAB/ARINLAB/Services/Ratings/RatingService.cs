using DAL.Data;
using DAL.Models;
using DAL.Models.Dto.NamesDTO;
using DAL.Models.ResponceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARINLAB.Services.Ratings
{
    public class RatingService : IRatingService
    {
        private readonly ApplicationDbContext _dbContext;
      

        public RatingService(ApplicationDbContext dbContext)
        {          
            _dbContext = dbContext;
        }
        public float GetRatingForName(int nameId)
        {
            var res = new List<NamesRating>(_dbContext.NamesRatings.Where(p => p.NamesId == nameId));
            if(res != null && res.Count() > 0)
            {
                return (res.Select(p => p.Rating).Sum() / res.Count());
            }
            else
            {
                return 0;
            }
        }

        public float GetRatingForNameImage(int nameImageId)
        {
            var res = new List<NamesImageRating>(_dbContext.NamesImageRatings.Where(p => p.NamesImageId == nameImageId));
            if (res != null && res.Count() > 0)
            {
                return (res.Select(p => p.Rating).Sum() / res.Count());
            }
            else
            {
                return 0;
            }
        }

        public float GetRatingForWord(int wordId)
        {
            var res = new List<WordRating>(_dbContext.WordRatings.Where(p => p.WordId == wordId));
            if (res != null && res.Count() > 0)
            {
                return (res.Select(p => p.Rating).Sum() / res.Count());
            }
            else
            {
                return 0;
            }
        }

        public float GetRatingForWordClause(int clauseId)
        {
            var res = new List<WordClauseRating>(_dbContext.WordClauseRatings.Where(p => p.WordClauseId == clauseId));
            if (res != null && res.Count() > 0)
            {
                return (res.Select(p => p.Rating).Sum() / res.Count());
            }
            else
            {
                return 0;
            }
        }

        public float GetRatingForWordSentence(int sentenceId)
        {
            var res = new List<WordSentenceRating>(_dbContext.WordSentenceRatings.Where(p => p.WordSentenceId == sentenceId));
            if (res != null && res.Count() > 0)
            {
                return (res.Select(p => p.Rating).Sum() / res.Count());
            }
            else
            {
                return 0;
            }
        }

        public async Task<Responce> SetRatingForNameAsync(float rating, int id)
        {
            var res = await _dbContext.Names.FindAsync(id);
            if(res == null)
            {
                return ResponceGenerator.GetResponceModel(false, "", null);
            }

            _dbContext.NamesRatings.Add(new NamesRating { NamesId = id, Rating = rating });
            _dbContext.SaveChanges();
            return ResponceGenerator.GetResponceModel(true, "", null);
        }

        public async Task<Responce> SetRatingForNameImageAsync(float rating, int id)
        {
            var res = await _dbContext.NameImages.FindAsync(id);
            if (res == null)
            {
                return ResponceGenerator.GetResponceModel(false, "", null);
            }

            _dbContext.NamesImageRatings.Add(new NamesImageRating { NamesImageId = id, Rating = rating });
            _dbContext.SaveChanges();
            return ResponceGenerator.GetResponceModel(true, "", null);
        }

        public async Task<Responce> SetRatingForWordAsync(float rating, int id)
        {
            var res = await _dbContext.Words.FindAsync(id);
            if (res == null)
            {
                return ResponceGenerator.GetResponceModel(false, "", null);
            }

            _dbContext.WordRatings.Add(new WordRating { WordId = id, Rating = rating });
            _dbContext.SaveChanges();
            return ResponceGenerator.GetResponceModel(true, "", null);
        }

        public async Task<Responce> SetRatingForWordClauseAsync(float rating, int id)
        {
            var res = await _dbContext.WordClauses.FindAsync(id);
            if (res == null)
            {
                return ResponceGenerator.GetResponceModel(false, "", null);
            }

            _dbContext.WordClauseRatings.Add(new WordClauseRating { WordClauseId = id, Rating = rating });
            _dbContext.SaveChanges();
            return ResponceGenerator.GetResponceModel(true, "", null);
        }

        public async Task<Responce> SetRatingForWordSentenceAsync(float rating, int id)
        {
            var res = await _dbContext.WordSentences.FindAsync(id);
            if (res == null)
            {
                return ResponceGenerator.GetResponceModel(false, "", null);
            }

            _dbContext.WordSentenceRatings.Add(new WordSentenceRating { WordSentenceId = id, Rating = rating });
            _dbContext.SaveChanges();
            return ResponceGenerator.GetResponceModel(true, "", null);
        }
    }
}
