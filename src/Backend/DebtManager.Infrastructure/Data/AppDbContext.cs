using System;
using DebtManager.Application.Interfaces;
using DebtManager.Domain.Entities;
using DebtManager.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace DebtManager.Infrastructure.Data;

public class AppDbContext : DbContext, IAppDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Income> Incomes { get; set; }
    public DbSet<Debt> Debts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        // --- SEED DATA ---
        var userId = Guid.Parse("00000000-0000-0000-0000-000000000001");
        var now = DateTimeOffset.Parse("2026-07-23T12:00:00Z"); // Fecha fija para migraciones estables

        modelBuilder
            .Entity<User>()
            .HasData(
                new
                {
                    Id = userId,
                    Email = "test@debtmanager.com",
                    BaseCurrency = "USD",
                    CreatedAt = now,
                    LastModifiedAt = now,
                    IsDeleted = false
                }
            );

        modelBuilder
            .Entity<Income>()
            .HasData(
                new
                {
                    Id = Guid.Parse("10000000-0000-0000-0000-000000000001"),
                    UserId = userId,
                    Description = "Sueldo Fijo Mensual",
                    Amount = 2500.00m,
                    Type = IncomeType.Fixed,
                    IsActive = true,
                    LastModifiedAt = now,
                    IsDeleted = false
                },
                new
                {
                    Id = Guid.Parse("10000000-0000-0000-0000-000000000002"),
                    UserId = userId,
                    Description = "Trabajos Freelance",
                    Amount = 500.00m,
                    Type = IncomeType.Variable,
                    IsActive = true,
                    LastModifiedAt = now,
                    IsDeleted = false
                }
            );

        modelBuilder
            .Entity<Debt>()
            .HasData(
                new
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000001"),
                    UserId = userId,
                    Name = "Tarjeta de Crédito Visa",
                    TotalBalance = 1200.00m,
                    MinimumMonthlyPayment = 60.00m,
                    AnnualInterestRate = 18.5m,
                    DueDay = 15,
                    IsCreditCard = true,
                    CutoffDay = 30,
                    LastModifiedAt = now,
                    IsDeleted = false
                },
                new
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000002"),
                    UserId = userId,
                    Name = "Préstamo Vehicular",
                    TotalBalance = 8500.00m,
                    MinimumMonthlyPayment = 250.00m,
                    AnnualInterestRate = 12.0m,
                    DueDay = 5,
                    IsCreditCard = false,
                    CutoffDay = (int?)null,
                    LastModifiedAt = now,
                    IsDeleted = false
                }
            );

        base.OnModelCreating(modelBuilder);
    }
}
