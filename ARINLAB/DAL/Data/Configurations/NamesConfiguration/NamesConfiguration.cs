using DAL.Models;
using DAL.Models.Configs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data.Configurations.NamesConfiguration
{
    class NamesConfiguration : IEntityTypeConfiguration<Names>
    {
        public void Configure(EntityTypeBuilder<Names> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.ArabName).IsRequired();
            builder.Property(p => p.OtherName).IsRequired();
            builder.Property(p => p.IsApproved).IsRequired();
            builder.Property(p => p.UserId).IsRequired();
            builder.Property(p => p.ImageForShare).IsRequired(false);
            builder.HasOne(p => p.Dictionary).WithMany(p => p.Names).HasForeignKey(p => p.DictionaryId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(p => p.NamesRatings).WithOne(p => p.Name).HasForeignKey(p => p.NamesId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
