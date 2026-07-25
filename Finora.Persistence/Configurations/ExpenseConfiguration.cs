using Finora.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Persistence.Configurations
{
    public class ExpenseConfiguration : IEntityTypeConfiguration<Expense>
    {
        public void Configure(EntityTypeBuilder<Expense> builder)
        {
            builder.ToTable("Expenses");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title).IsRequired().HasMaxLength(100);

            builder.Property(x => x.Description).HasMaxLength(500);

            builder.Property(x => x.Amount).HasPrecision(18, 2);

            builder.Property(x => x.ExpenseDate).IsRequired();

            builder.Property(x => x.IsArchived).HasDefaultValue(false);

            builder.HasIndex(x => new
            {
                x.RecurringTransactionId,
                x.RecurringOccurrenceDate
            }).IsUnique().HasFilter("[RecurringTransactionId] IS NOT NULL");

            builder.HasOne(x => x.Category).WithMany().HasForeignKey(x => x.CategoryId).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
