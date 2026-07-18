using Finora.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Finora.Persistence.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);

            builder.Property(x => x.Description).HasMaxLength(500);
            
            builder.Property(x => x.IconKey).IsRequired().HasMaxLength(50);

            builder.Property(x => x.ColorKey).IsRequired().HasMaxLength(50);

            builder.Property(x => x.DisplayOrder).HasDefaultValue(0);

            builder.Property(x => x.IsSystem).HasDefaultValue(false);

            builder.Property(x => x.IsArchived).HasDefaultValue(false);

            builder.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => x.UserId);
        }
    }
}
