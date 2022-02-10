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
    public class AudoFileConfiguration : IEntityTypeConfiguration<AudioFile>
    {
        public void Configure(EntityTypeBuilder<AudioFile> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.OtherVoice).IsRequired();
            builder.Property(p => p.ArabVoice).IsRequired();
            builder.HasOne(p => p.Word).WithMany(p => p.AudioFiles).HasForeignKey(p => p.WordId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
