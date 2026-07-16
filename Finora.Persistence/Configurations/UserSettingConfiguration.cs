using Finora.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Persistence.Configurations
{
    public class UserSettingConfiguration : IEntityTypeConfiguration<UserSetting>
    {
        public void Configure(EntityTypeBuilder<UserSetting> builder)
        {
            builder.ToTable("UserSettings");
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.UserId).IsUnique();
            builder.Property(x => x.Language).IsRequired().HasMaxLength(10);
            builder.Property(x => x.Currency).IsRequired().HasMaxLength(10);
            builder.Property(x => x.TimeZone).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Theme).IsRequired();
            builder.HasOne(x => x.User).WithOne(x => x.UserSetting).HasForeignKey<UserSetting>(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
