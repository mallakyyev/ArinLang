using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using DAL.Models.Menu;
namespace DAL.Data.Configurations.MenuConfiguration
{
    class MenuTranslateConfiguration : IEntityTypeConfiguration<MenuTranslate>
    {
        public void Configure(EntityTypeBuilder<MenuTranslate> builder)
        {
  
        builder.HasKey(p => p.Id);
        builder.Property(p => p.MenuId).IsRequired();
        builder.Property(p => p.Name).IsRequired();
        builder.Property(p => p.LanguageCulture).IsRequired();

        }
    }
}
