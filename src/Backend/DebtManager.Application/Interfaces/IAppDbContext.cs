using DebtManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace DebtManager.Application.Interfaces;

public interface IAppDbContext
{
    DbSet<User> Users { get; }
    DbSet<Income> Incomes { get; }
    DbSet<Debt> Debts { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
