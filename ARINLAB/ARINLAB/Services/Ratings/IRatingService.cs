using DAL.Models.ResponceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARINLAB.Services.Ratings
{
    public interface IRatingService
    {
        public Task<Responce> SetRatingForNameAsync(float rating, int id);
        public Task<Responce> SetRatingForWordAsync(float rating, int id);
        public Task<Responce> SetRatingForWordClauseAsync(float rating, int id);
        public Task<Responce> SetRatingForWordSentenceAsync(float rating, int id);
        public Task<Responce> SetRatingForNameImageAsync(float rating, int id);

        public float GetRatingForName(int nameId);
        public float GetRatingForNameImage(int nameImageId);
        public float GetRatingForWord(int wordId);
        public float GetRatingForWordSentence(int sentenceId);
        public float GetRatingForWordClause(int clauseId);

    }
}
