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
    public class AudoFileForClauseConfiguration : IEntityTypeConfiguration<AudioFileForClause>
    {
        public void Configure(EntityTypeBuilder<AudioFileForClause> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.OtherVoice).IsRequired();
            builder.Property(p => p.ArabVoice).IsRequired();
            builder.HasOne(p => p.WordClause).WithMany(p => p.AudioFiles).HasForeignKey(p => p.ClauseId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
