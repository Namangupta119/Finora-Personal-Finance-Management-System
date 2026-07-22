using Finora.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Persistence.Configurations
{
    public class GoalContributionConfiguration : IEntityTypeConfiguration<GoalContribution>
    {
        public void Configure(EntityTypeBuilder<GoalContribution> builder)
        {
            // Table
            builder.ToTable("GoalContributions");

            // Primary Key
            builder.HasKey(x => x.Id);

            // Properties
            builder.Property(x => x.Amount)
                   .HasPrecision(18, 2);

            builder.Property(x => x.Notes)
                   .HasMaxLength(500);

            builder.Property(x => x.IsArchived)
                   .HasDefaultValue(false);

            // Relationship
            builder.HasOne(x => x.Goal)
                   .WithMany(x => x.GoalContributions)
                   .HasForeignKey(x => x.GoalId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Index
            builder.HasIndex(x => x.GoalId);
        }
    }
}
