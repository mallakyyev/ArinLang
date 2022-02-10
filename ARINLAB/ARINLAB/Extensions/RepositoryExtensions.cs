
using ARINLAB.Services;
using ARINLAB.Services.ApplicationUser;
using ARINLAB.Services.Email;
using ARINLAB.Services.ImageService;
using ARINLAB.Services.Menu;
using ARINLAB.Services.News;
using ARINLAB.Services.NewsCategory;
using ARINLAB.Services.Pages;
using ARINLAB.Services.Ratings;
using ARINLAB.Services.Search;
using ARINLAB.Services.SessionService;
using ARINLAB.Services.Settings;
using ARINLAB.Services.Statistic;
using ARINLAB.Services.Subscribe;
using DAL.Models;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.DependencyInjection;


namespace ARINLAB.Extensions
{
    public static class RepositoryExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IEmailSender, MailKitEmailSender>();
            services.AddSingleton<IImageService, ImageService>();
            //services.Configure<MailKitEmailSenderOptions>(options =>
            //{
            //    options.Host_Address = "smtp.mail.ru";
            //    options.Host_Port = 465;
            //    options.Host_Username = "tazedowur@mail.ru";
            //    options.Host_Password = "tdta2020";
            //    options.Sender_EMail = "tazedowur@mail.ru";
            //    options.Sender_Name = "ARINLANG";
            //});

            services.AddScoped<IWordServices, WordServices>();
            services.AddScoped<IDictionaryService, DictionaryService>();
            services.AddScoped<FileServices>();
            services.AddTransient<UserDictionary>();
            services.AddScoped<IWordClauseService, WordClauseService>();
            services.AddScoped<ILanguageService, LanguageService>();
            services.AddTransient<IMenuService, MenuService>();
            services.AddTransient<IPagesService, PagesService>();
            services.AddTransient<INewsCategoryService, NewsCategoryService>();
            services.AddTransient<INewsService, NewsService>();
            services.AddTransient<ISettingsService, SettingsService>();
            services.AddTransient<INamesService, NamesService>();
            services.AddScoped<IStatisticsService, StatisticsService>();
            services.AddScoped<ISubscribeService, SubscribeService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddTransient<IApplicationUserService, ApplicationUserService>();
            services.AddTransient<IRatingService, RatingService>();
            services.AddScoped<LogoService>();
            services.AddScoped<ISearchService, SearchService>();
            //Add Scoped Services


            return services;
        }
    }
}
