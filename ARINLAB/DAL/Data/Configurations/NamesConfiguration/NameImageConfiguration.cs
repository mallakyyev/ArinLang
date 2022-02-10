using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data.Configurations.NamesConfiguration
{
    public class NameImageConfiguration : IEntityTypeConfiguration<NameImages>
    {
        public void Configure(EntityTypeBuilder<NameImages> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.ImageUri).IsRequired();
            builder.Property(p => p.NamesId).IsRequired();
            builder.Property(p => p.IsApproved).IsRequired();
            builder.Property(p => p.UserId).IsRequired();
            builder.HasOne(p => p.Names).WithMany(p => p.NameImages).HasForeignKey(p => p.NamesId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(p => p.NamesImageRatings).WithOne(p => p.NameImage).HasForeignKey(p => p.NamesImageId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
