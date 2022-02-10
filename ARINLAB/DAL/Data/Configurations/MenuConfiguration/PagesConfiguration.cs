using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using DAL.Models.Menu;
namespace DAL.Data.Configurations.MenuConfiguration
{
    class PagesConfiguration : IEntityTypeConfiguration<Pages>
    {
        public void Configure(EntityTypeBuilder<Pages> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.MenuId).IsRequired(false);
            builder.Property(p => p.IsPublish).IsRequired();

            builder.HasMany(p => p.PagesTranslates).WithOne(p => p.Pages).HasForeignKey(p => p.PagesId).OnDelete(DeleteBehavior.Cascade);
        }
            

    }
}

