using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data.Configurations.RatingsConfiguration
{
    public class WordClauseCategoryTranslatesConfiguration : IEntityTypeConfiguration<WordClauseCategoryTranslate>
    {
        public void Configure(EntityTypeBuilder<WordClauseCategoryTranslate> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.WordClauseCategoryId).IsRequired();
            builder.Property(p => p.LanguageCulture).IsRequired();
        }
    }
}
