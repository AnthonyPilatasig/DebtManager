using DebtManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DebtManager.Infrastructure.Data.Configurations;

internal sealed class IncomeConfiguration : IEntityTypeConfiguration<Income>
{
    public void Configure(EntityTypeBuilder<Income> builder)
    {
        builder.ToTable("Incomes");
        builder.HasKey(i => i.Id);
        builder.Property(i => i.Description).IsRequired().HasMaxLength(150);
        builder.Property(i => i.Amount).HasPrecision(18, 4);
        
        builder.HasOne(i => i.User)
               .WithMany(u => u.Incomes)
               .HasForeignKey(i => i.UserId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
