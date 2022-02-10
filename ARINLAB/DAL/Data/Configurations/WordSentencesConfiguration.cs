using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data.Configurations
{
    class WordSentencesConfiguration : IEntityTypeConfiguration<WordSentences>
    {
        public void Configure(EntityTypeBuilder<WordSentences> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.ArabSentence).IsRequired();
            builder.Property(p => p.OtherSentence).IsRequired();
            builder.Property(p => p.ArabReader).IsRequired(false);
            builder.Property(p => p.OtherReader).IsRequired(false);
            builder.Property(p => p.IsApproved).IsRequired();
            builder.HasOne(p => p.Word).WithMany(p => p.WordSentences).HasForeignKey(p => p.WordId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(p => p.ApplicationUser).WithMany(p => p.WordSentences).HasForeignKey(p => p.UserId);
            builder.HasMany(p => p.WordSentenceRatings).WithOne(p => p.WordSentence).HasForeignKey(p => p.WordSentenceId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
