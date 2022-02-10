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
        public class LogoConfiguration : IEntityTypeConfiguration<Logo>
        {
            public void Configure(EntityTypeBuilder<Logo> builder)
            {
                builder.HasKey(p => p.Id);
                builder.Property(p => p.Name).IsRequired(false);
                builder.Property(p => p.Link).IsRequired(false);
                builder.Property(p => p.Image).IsRequired(false);              
            }
        }  
}
