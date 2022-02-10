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
    public class WordRatingConfiguration : IEntityTypeConfiguration<WordRating>
    {
        public void Configure(EntityTypeBuilder<WordRating> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.WordId).IsRequired();
            builder.Property(p => p.Rating);
        }
    }
}
