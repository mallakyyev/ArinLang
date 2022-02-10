using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARINLAB.Areas.Trusted.Utils
{
    public class API
    {
        public static string GetAllWordClauseCategories { get; } = "/api/WordClauseCategoryAPI";
        public static string GetAllWordClauses { get; } = "/api/WordClauseAPI";     
        public static string GetAllWordClauseAudioFiles { get; } = "/api/WordClauseAudioAPI";
        public static string GetAllMenu { get; } = "/api/MenuAPI";
        public static string GetAllNews { get; } = "/api/NewsAPI";
        public static string GetAllNewsCategory { get; } = "/api/NewsAPI/GetAllNewsCategories";
        public static string GetAllPages { get; } = "/api/PagesAPI";
        public static string GetAllNames { get; } = "/api/NamesAPI"; 
        
        public static string GetAllNamesImages { get; set; } = "/api/NamesAPI/GetImage";
        public static string DeleteImage { get; set; } = "/api/NamesAPI";
        public static string GetRandomWords { get; set; } = "/api/WordsAPI/RandomWords"; 
        public static string GetRandomWordClauses{ get; } = "/api/WordClauseAPI/RandomWordClauses";
        public static string GetRandomNames { get; } = "/api/NamesAPI/GetRandomNames";
        public static string GetNamesWithDictId { get; } = "/api/NamesAPI/GetAllNamesWithDict";
        public static string GetAllWordsWithDictId { get; set; } = "/api/WordsAPI/GetAllWordsWithDict";
        public static string GetAllWordClausesWithDictId { get; } = "/api/WordClauseAPI/GetAllWordsClausesWithDict";
    }
}
