using DAL.Models.News;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace DAL.Data.Configurations.NewsConfiguration
{
    class NewsCategoryConfiguration : IEntityTypeConfiguration<NewsCategory>
    {
        public void Configure(EntityTypeBuilder<NewsCategory> builder)
        {
            builder.HasKey(p => p.Id);
            builder.HasMany(p => p.News).WithOne(p => p.NewsCategory).HasForeignKey(p => p.NewsCategoryID).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(p => p.NewsCategoryTranslates).WithOne(p => p.NewsCategory).HasForeignKey(p => p.NewsCategoryId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
