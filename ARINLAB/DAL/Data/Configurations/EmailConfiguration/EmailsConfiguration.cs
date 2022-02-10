using DAL.Models.Email;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace DAL.Data.Configurations.EmailConfiguration
{
    class EmailsConfiguration : IEntityTypeConfiguration<Emails>
    {
        public void Configure(EntityTypeBuilder<Emails> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Subject).IsRequired();
            builder.Property(p => p.Message).IsRequired();
            builder.Property(p => p.SendedToEntrepreneur).IsRequired();
            builder.Property(p => p.SendedToOrganization).IsRequired();
            builder.Property(p => p.SendedToSubscribers).IsRequired();
        }
    }
}
