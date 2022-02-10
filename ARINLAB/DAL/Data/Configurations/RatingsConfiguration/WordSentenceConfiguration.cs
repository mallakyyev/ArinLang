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
    public class WordSentenceConfiguration : IEntityTypeConfiguration<WordSentenceRating>
    {
        public void Configure(EntityTypeBuilder<WordSentenceRating> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.WordSentenceId).IsRequired();
            builder.Property(p => p.Rating);
        }
    }
}
