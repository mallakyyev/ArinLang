using DAL.Models.News;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Data.Configurations.NewsConfiguration
{
    class NewsCategoryTranslateConfiguration : IEntityTypeConfiguration<NewsCategoryTranslate>
    {
        public void Configure(EntityTypeBuilder<NewsCategoryTranslate> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.NewsCategoryId).IsRequired();
            builder.Property(p => p.Name).IsRequired();
            builder.Property(p => p.LanguageCulture).IsRequired();
        }
    }
}
