﻿using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data.Configurations
{
    public class WordsConfiguration : IEntityTypeConfiguration<Word>
    {
        public void Configure(EntityTypeBuilder<Word> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.DictionaryId).IsRequired();
            builder.Property(p => p.ArabWord).IsRequired();
            builder.Property(p => p.OtherWord).IsRequired();
            builder.Property(p => p.IsApproved).IsRequired();
            builder.HasOne(p => p.ApplicationUser).WithMany(p => p.Words).HasForeignKey(p => p.UserId);
            builder.HasMany(p => p.WordRatings).WithOne(p => p.Word).HasForeignKey(p => p.WordId).OnDelete(DeleteBehavior.Cascade);
            
                     
        }
    }
}
