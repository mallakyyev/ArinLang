using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using DAL.Models.Menu;
namespace DAL.Data.Configurations.MenuConfiguration
{
    public class MenuConfiguration : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Link).IsRequired(false);
            builder.Property(p => p.Order).IsRequired();
            builder.Property(p => p.IsPublish).IsRequired();
            builder.Property(p => p.ParentId).IsRequired(false);
            builder.HasOne(p => p.ParentMenu).WithMany(p => p.Menus).HasForeignKey(p => p.ParentId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(p => p.Pages).WithOne(p => p.Menu).HasForeignKey<Pages>(a => a.MenuId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(p => p.MenuTranslates).WithOne(p => p.Menu).HasForeignKey(p => p.MenuId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
