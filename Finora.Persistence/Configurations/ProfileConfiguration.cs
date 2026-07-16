using Finora.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Persistence.Configurations
{
    public class ProfileConfiguration : IEntityTypeConfiguration<Profile>
    {
        public void Configure(EntityTypeBuilder<Profile> builder)
        {
            builder.ToTable("Profiles");

            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.UserId).IsUnique();
            
            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(100);

            builder.Property(x => x.LastName).IsRequired().HasMaxLength(100);
            
            builder.Property(x => x.Phone).IsRequired().HasMaxLength(20);

            builder.Property(x => x.Gender).IsRequired();

            builder.Property(x => x.Address).HasMaxLength(500);

            builder.Property(x => x.ProfileImageUrl).HasMaxLength(500);

            builder.HasOne(x => x.User).WithOne(x => x.Profile).HasForeignKey<Profile>(x => x.UserId).OnDelete(DeleteBehavior.Cascade);

        }
    }
}
