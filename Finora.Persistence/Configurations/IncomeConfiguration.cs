using Finora.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Finora.Persistence.Configurations
{
    public class IncomeConfiguration : IEntityTypeConfiguration<Income>
    {
        public void Configure(EntityTypeBuilder<Income> builder)
        {
            builder.ToTable("Incomes");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title).IsRequired().HasMaxLength(100);

            builder.Property(x => x.Description).HasMaxLength(500);

            builder.Property(x => x.Amount).HasPrecision(18, 2);

            builder.Property(x => x.IncomeDate).IsRequired();

            builder.Property(x => x.IsArchived).HasDefaultValue(false);

            builder.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
