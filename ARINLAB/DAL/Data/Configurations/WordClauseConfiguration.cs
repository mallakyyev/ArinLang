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
    public class WordClauseConfiguration : IEntityTypeConfiguration<WordClause>
    {
        public void Configure(EntityTypeBuilder<WordClause> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.CategoryId).IsRequired();
            builder.Property(p => p.ArabClause).IsRequired();
            builder.Property(p => p.OtherClause).IsRequired();
            builder.Property(p => p.ArabReader).IsRequired();
            builder.Property(p => p.OtherReader).IsRequired();
            builder.Property(p => p.DictionaryId).IsRequired();
            builder.Property(p => p.IsApproved).IsRequired();
            builder.Property(p => p.UserId).IsRequired();
            builder.Property(p => p.Viewed).IsRequired(false);
            builder.Property(p => p.AddedDate).IsRequired().HasDefaultValue(DateTime.Now);
            builder.HasOne(p => p.WordClauseCategory).WithMany(p => p.WordClauses).HasForeignKey(p => p.CategoryId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(p => p.ApplicationUser).WithMany(p => p.WordClauses).HasForeignKey(p => p.UserId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(p => p.Dictionary).WithMany(p => p.WordClauses).HasForeignKey(p => p.DictionaryId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(p => p.WordClauseRatings).WithOne(p => p.WordClause).HasForeignKey(p => p.WordClauseId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
