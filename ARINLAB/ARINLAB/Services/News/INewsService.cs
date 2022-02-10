
using DAL.Models.Dto.NewsModelDTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ARINLAB.Services.News
{
    public interface  INewsService
    {
        IEnumerable<NewsDTO> GetAllNews();
        IEnumerable<NewsDTO> GetAllPublishNews();
        Task CreateNews(CreateNewsDTO modelDTO);
        Task EditNews(EditNewsDTO modelDTO);
        Task RemoveNews(int id);
        Task RemoveAllNews(int categoryId);
        IEnumerable<NewsDTO> GetNewsByCategory(int categoryID);
        IEnumerable<NewsDTO> SortNewsByDate(bool ascending = false);
        
        IEnumerable<NewsDTO> SortNewsByDate(int categoryId, bool ascending = false);
        IEnumerable<NewsDTO> GetNewsByDate(DateTime date);
        IEnumerable<NewsDTO> GetNewsByDateAndCategory(DateTime date, int categoryId);  
        Task<NewsDTO> GetNewsByIdAsync(int id);
        Task<EditNewsDTO> GetNewsForEditById(int id);
        //IEnumerable<SearchResultModel> SearchByNameAndDesc(string searchText);

        IEnumerable<NewsDTO> GetFourPublishNews();

    }
}
