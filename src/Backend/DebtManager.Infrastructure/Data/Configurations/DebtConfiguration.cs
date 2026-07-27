using DebtManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DebtManager.Infrastructure.Data.Configurations;

internal sealed class DebtConfiguration : IEntityTypeConfiguration<Debt>
{
    public void Configure(EntityTypeBuilder<Debt> builder)
    {
        builder.ToTable("Debts");
        builder.HasKey(d => d.Id);
        builder.Property(d => d.Name).IsRequired().HasMaxLength(150);
        builder.Property(d => d.TotalBalance).HasPrecision(18, 4);
        builder.Property(d => d.MinimumMonthlyPayment).HasPrecision(18, 4);
        builder.Property(d => d.AnnualInterestRate).HasPrecision(10, 4);
        
        builder.HasOne(d => d.User)
               .WithMany(u => u.Debts)
               .HasForeignKey(d => d.UserId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
