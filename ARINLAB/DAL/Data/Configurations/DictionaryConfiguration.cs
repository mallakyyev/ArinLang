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
    public class DictionaryConfiguration : IEntityTypeConfiguration<Dictionary>
    {
        public void Configure(EntityTypeBuilder<Dictionary> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Language).IsRequired();
            builder.Property(p => p.ArabTranslate).IsRequired();
            builder.HasMany(p => p.Words).WithOne(p => p.Dictionary).HasForeignKey(p => p.DictionaryId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
