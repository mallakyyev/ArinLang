using DAL.Models.News;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace DAL.Data.Configurations.NewsConfiguration
{
    class NewsTranslateConfiguration : IEntityTypeConfiguration<NewsTranslate>
    {
        public void Configure(EntityTypeBuilder<NewsTranslate> builder)
        {

        builder.HasKey(p => p.Id);
        builder.Property(p => p.NewsId).IsRequired();
        builder.Property(p => p.Name).IsRequired();
        builder.Property(p => p.Description).IsRequired(false);
        builder.Property(p => p.LanguageCulture).IsRequired();

        }
    }
}
