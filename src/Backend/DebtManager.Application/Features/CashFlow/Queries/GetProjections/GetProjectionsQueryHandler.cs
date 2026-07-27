using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DebtManager.Application.Features.CashFlow.Queries.CalculateFreeCashFlow;
using DebtManager.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DebtManager.Application.Features.CashFlow.Queries.GetProjections;

public class GetProjectionsQueryHandler
    : IRequestHandler<GetProjectionsQuery, GetProjectionsResponse>
{
    private readonly IAppDbContext _context;
    private readonly IMediator _mediator;

    public GetProjectionsQueryHandler(IAppDbContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task<GetProjectionsResponse> Handle(
        GetProjectionsQuery request,
        CancellationToken cancellationToken
    )
    {
        // 1. Obtener FCL
        var freeCashFlow = await _mediator.Send(
            new CalculateFreeCashFlowQuery(request.UserId),
            cancellationToken
        );

        // 2. Obtener deudas activas
        var debts = await _context
            .Debts.Where(d => d.UserId == request.UserId && !d.IsDeleted)
            .ToListAsync(cancellationToken);

        // BR-03: Método Bola de Nieve (Menor saldo a mayor)
        var snowball = debts.OrderBy(d => d.TotalBalance).ToList();

        // BR-03: Método Avalancha (Mayor tasa de interés a menor)
        var avalanche = debts.OrderByDescending(d => d.AnnualInterestRate).ToList();

        return new GetProjectionsResponse(
            FreeCashFlow: freeCashFlow,
            SnowballPlan: new ProjectionPlan("Bola de Nieve", snowball),
            AvalanchePlan: new ProjectionPlan("Avalancha", avalanche)
        );
    }
}
