using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using DAL.Models.Menu;

namespace DAL.Data.Configurations.MenuConfiguration { 
    class PagesTranslateConfiguration : IEntityTypeConfiguration<PagesTranslate>
    {
        public void Configure(EntityTypeBuilder<PagesTranslate> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.PagesId).IsRequired();
            builder.Property(p => p.Name).IsRequired();
            builder.Property(p => p.LanguageCulture).IsRequired();
            builder.Property(p => p.Description).IsRequired(false);
        }
    }
}
