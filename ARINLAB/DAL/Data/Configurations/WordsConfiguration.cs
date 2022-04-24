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
    public class WordsConfiguration : IEntityTypeConfiguration<Word>
    {
        public void Configure(EntityTypeBuilder<Word> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.DictionaryId).IsRequired();
            builder.Property(p => p.ArabWord).IsRequired();
            builder.Property(p => p.OtherWord).IsRequired();
            builder.Property(p => p.IsApproved).IsRequired();
            builder.Property(p => p.ArabVoice).IsRequired(false);
            builder.Property(p => p.OtherVoice).IsRequired(false);
            builder.Property(p => p.ArabVoice1).IsRequired(false);
            builder.Property(p => p.OtherVoice1).IsRequired(false);
            builder.Property(p => p.ArabVoice2).IsRequired(false);
            builder.Property(p => p.OtherVoice2).IsRequired(false);
            builder.Property(p => p.ArabVoice3).IsRequired(false);
            builder.Property(p => p.OtherVoice3).IsRequired(false);
            builder.Property(p => p.ArabVoice4).IsRequired(false);
            builder.Property(p => p.OtherVoice4).IsRequired(false);
            builder.Property(p => p.Viewed).IsRequired(false);
            builder.Property(p => p.AddedDate).IsRequired().HasDefaultValue(DateTime.Now);
            builder.HasOne(p => p.ApplicationUser).WithMany(p => p.Words).HasForeignKey(p => p.UserId);
            builder.HasMany(p => p.WordRatings).WithOne(p => p.Word).HasForeignKey(p => p.WordId).OnDelete(DeleteBehavior.Cascade);
            
                     
        }
    }
}
