using Finora.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Finora.Persistence.Configurations
{
    public class GoalCategoryConfiguration : IEntityTypeConfiguration<GoalCategory>
    {
        public void Configure(EntityTypeBuilder<GoalCategory> builder)
        {
            builder.ToTable("GoalCategories");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);

            builder.Property(x => x.Description).HasMaxLength(500);

            builder.Property(x => x.Icon).HasMaxLength(100);

            builder.Property(x => x.IsActive).HasDefaultValue(true);

            builder.HasMany(x => x.Goals).WithOne(x => x.GoalCategory).HasForeignKey(x => x.GoalCategoryId).OnDelete(DeleteBehavior.Restrict);
            
            builder.HasIndex(x => x.Name).IsUnique();
        }
    }
}
