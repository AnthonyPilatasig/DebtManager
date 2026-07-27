using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DebtManager.Application.Interfaces;
using DebtManager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DebtManager.Application.Features.Incomes.Queries.GetIncomesByUser;

public class GetIncomesByUserQueryHandler : IRequestHandler<GetIncomesByUserQuery, List<Income>>
{
    private readonly IAppDbContext _context;

    public GetIncomesByUserQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Income>> Handle(
        GetIncomesByUserQuery request,
        CancellationToken cancellationToken
    )
    {
        return await _context
            .Incomes.AsNoTracking()
            .Where(i => i.UserId == request.UserId && !i.IsDeleted)
            .ToListAsync(cancellationToken);
    }
}
