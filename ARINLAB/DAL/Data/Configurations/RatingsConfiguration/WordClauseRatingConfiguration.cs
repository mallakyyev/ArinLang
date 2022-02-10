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
    public class WordClauseRatingConfiguration : IEntityTypeConfiguration<WordClauseRating>
    {
        public void Configure(EntityTypeBuilder<WordClauseRating> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.WordClauseId).IsRequired();
            builder.Property(p => p.Rating);
        }
    }
}
