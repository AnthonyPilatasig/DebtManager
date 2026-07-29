using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DebtManager.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DebtManager.Application.Features.Goals.Queries.GetGoals;

public class GetGoalsQueryHandler : IRequestHandler<GetGoalsQuery, List<GoalDto>>
{
    private readonly IAppDbContext _context;

    public GetGoalsQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<List<GoalDto>> Handle(
        GetGoalsQuery request,
        CancellationToken cancellationToken
    )
    {
        return await _context
            .Goals.Where(g => g.UserId == request.UserId && !g.IsDeleted)
            .Select(g => new GoalDto(
                g.Id,
                g.Name,
                g.Type,
                g.TargetAmount,
                g.TargetDate,
                g.EstimatedMonthlyPayment
            ))
            .ToListAsync(cancellationToken);
    }
}
