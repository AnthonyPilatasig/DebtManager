using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DebtManager.Domain.Entities;
using DebtManager.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DebtManager.Application.Features.Debts.Queries.GetDebtsByUserId;

public class GetDebtsByUserIdQueryHandler : IRequestHandler<GetDebtsByUserIdQuery, List<Debt>>
{
    private readonly IAppDbContext _context;

    public GetDebtsByUserIdQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Debt>> Handle(GetDebtsByUserIdQuery request, CancellationToken cancellationToken)
    {
        return await _context.Debts
            .AsNoTracking()
            .Where(d => d.UserId == request.UserId && !d.IsDeleted)
            .ToListAsync(cancellationToken);
    }
}
