using DAL.Models.Email;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Data.Configurations.EmailConfiguration
{
    class SubscribersConfiguration : IEntityTypeConfiguration<Subscribers>
    {
        public void Configure(EntityTypeBuilder<Subscribers> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Email).IsRequired();                       
        }
    }
}
