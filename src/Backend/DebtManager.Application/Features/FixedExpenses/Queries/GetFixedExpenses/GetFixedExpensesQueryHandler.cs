using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DebtManager.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DebtManager.Application.Features.FixedExpenses.Queries.GetFixedExpenses;

public class GetFixedExpensesQueryHandler : IRequestHandler<GetFixedExpensesQuery, List<FixedExpenseDto>>
{
    private readonly IAppDbContext _context;

    public GetFixedExpensesQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<List<FixedExpenseDto>> Handle(GetFixedExpensesQuery request, CancellationToken cancellationToken)
    {
        return await _context.FixedExpenses
            .Where(f => f.UserId == request.UserId && !f.IsDeleted)
            .OrderBy(f => f.DueDay)
            .Select(f => new FixedExpenseDto(
                f.Id,
                f.Name,
                f.Amount,
                f.DueDay
            ))
            .ToListAsync(cancellationToken);
    }
}
