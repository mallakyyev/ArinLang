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
        public class ApplicationUserCOnfiguration : IEntityTypeConfiguration<ApplicationUser>
        {
            public void Configure(EntityTypeBuilder<ApplicationUser> builder)
            {
                builder.Property(p => p.FirstName).IsRequired(true);
                builder.Property(p => p.FamilyName).IsRequired(false);
                builder.Property(p => p.Gender).IsRequired(true);
                builder.Property(p => p.IsApproved).IsRequired();
                builder.Property(p => p.Accupation).IsRequired(false);
                builder.HasOne(p => p.Country).WithMany(p => p.ApplicationUsers).HasForeignKey(p => p.CountryId);
            }
        }  
}
