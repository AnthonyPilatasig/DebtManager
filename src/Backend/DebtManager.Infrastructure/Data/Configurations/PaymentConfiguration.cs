using DebtManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DebtManager.Infrastructure.Data.Configurations;

internal sealed class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable("Payments");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Amount).HasPrecision(18, 4);

        builder
            .HasOne(p => p.Debt)
            .WithMany(d => d.Payments)
            .HasForeignKey(p => p.DebtId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
