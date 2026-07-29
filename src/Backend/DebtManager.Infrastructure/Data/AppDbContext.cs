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
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<FixedExpense> FixedExpenses { get; set; }
    public DbSet<Goal> Goals { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);



        base.OnModelCreating(modelBuilder);
    }
}
