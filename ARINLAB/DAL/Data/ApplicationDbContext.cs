using DAL.Models;
using DAL.Models.Configs;
using DAL.Models.Email;
using DAL.Models.Menu;
using DAL.Models.News;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace DAL.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Settings> Settings { get; set; }
        public DbSet<Dictionary> Dictionaries{ get; set; }
        public DbSet<Word> Words { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<AudioFile> AudioFiles { get; set; }
        public DbSet<WordSentences> WordSentences { get; set; }
        public DbSet<WordClause> WordClauses { get; set; }
        public DbSet<WordClauseCategory> WordClauseCategories { get; set; }
        public DbSet<WordClauseCategoryTranslate> WordClauseCategoryTranslates { get; set; }
        public DbSet<AudioFileForClause> AudioFileForClauses { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuTranslate> MenuTranslates { get; set; }
        public DbSet<Pages> Pages { get; set; }
        public DbSet<PagesTranslate> PagesTranslates { get; set; }

        public DbSet<News> News { get; set; }
        public DbSet<NewsCategory> NewsCategories { get; set; }
        public DbSet<NewsCategoryTranslate> NewsCategoryTranslates { get; set; }
        public DbSet<NewsTranslate> NewsTranslates { get; set; }
        public DbSet<Names> Names { get; set; }
        public DbSet<NameImages> NameImages { get; set; }

        public DbSet<Emails> Emails { get; set; }
        public DbSet<Subscribers> Subscribers { get; set; }

        public DbSet<WordRating> WordRatings { get; set; }
        public DbSet<WordSentenceRating> WordSentenceRatings { get; set; }
        public DbSet<NamesImageRating> NamesImageRatings { get; set; }
        public DbSet<WordClauseRating> WordClauseRatings { get; set; }
        public DbSet<NamesRating> NamesRatings { get; set; }
        public DbSet<Logo> Logos { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public void Detach(ApplicationUser entity)
        {
            this.Entry(entity).State = EntityState.Detached;
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Применение всех конфигурация в сборке
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            base.OnModelCreating(builder);
        }
    }
}
