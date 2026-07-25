using Finora.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Persistence.Configurations
{
    public class InvestmentConfiguration : IEntityTypeConfiguration<Investment>
    {
        public void Configure(EntityTypeBuilder<Investment> builder)
        {
            builder.ToTable("Investments");

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Type)
                .IsRequired();

            builder.Property(x => x.Quantity)
                .HasPrecision(18, 4)
                .IsRequired();

            builder.Property(x => x.PurchasePrice)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(x => x.CurrentPrice)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(x => x.PurchaseDate)
                .IsRequired();

            builder.Property(x => x.Broker)
                .HasMaxLength(100);

            builder.Property(x => x.Notes)
                .HasMaxLength(1000);

            builder.Property(x => x.IsArchived)
                .HasDefaultValue(false);

            builder.Property(x => x.Symbol).HasMaxLength(20);

            builder.HasIndex(x => x.UserId);

            builder.HasIndex(x => new
            {
                x.UserId,
                x.Type
            });

            builder.HasIndex(x => x.PurchaseDate);
        }
    }
}
