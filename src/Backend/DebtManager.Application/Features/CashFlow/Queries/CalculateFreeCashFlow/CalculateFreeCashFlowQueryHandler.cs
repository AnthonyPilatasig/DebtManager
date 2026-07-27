using DebtManager.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DebtManager.Application.Features.CashFlow.Queries.CalculateFreeCashFlow;

public class CalculateFreeCashFlowQueryHandler : IRequestHandler<CalculateFreeCashFlowQuery, decimal>
{
    private readonly IAppDbContext _context;

    public CalculateFreeCashFlowQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<decimal> Handle(CalculateFreeCashFlowQuery request, CancellationToken cancellationToken)
    {
        var incomes = await _context.Incomes
            .Where(i => i.UserId == request.UserId && !i.IsDeleted && i.IsActive)
            .ToListAsync(cancellationToken);

        var debts = await _context.Debts
            .Where(d => d.UserId == request.UserId && !d.IsDeleted)
            .ToListAsync(cancellationToken);

        var totalIncome = incomes.Sum(i => i.Amount);
        var totalFixedDebt = debts.Sum(d => d.MinimumMonthlyPayment);

        return totalIncome - totalFixedDebt; // Flujo de Caja Libre
    }
}
