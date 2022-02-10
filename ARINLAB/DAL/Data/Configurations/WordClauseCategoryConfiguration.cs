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
    public class WordClauseCategoryConfiguration : IEntityTypeConfiguration<WordClauseCategory>
    {
        public void Configure(EntityTypeBuilder<WordClauseCategory> builder)
        {
            builder.HasKey(p => p.Id);                        
            builder.Property(p => p.ParentCategoryId).IsRequired();
            builder.HasMany(p => p.WordClauseCategoryTranslates).WithOne(p => p.WordClauseCategory).HasForeignKey(p => p.WordClauseCategoryId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
