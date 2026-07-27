using System;
using System.Collections.Generic;
using DebtManager.Domain.Entities;
using MediatR;

namespace DebtManager.Application.Features.CashFlow.Queries.GetProjections;

public record ProjectionPlan(string StrategyName, List<Debt> OrderedDebts);

public record GetProjectionsResponse(
    decimal FreeCashFlow,
    ProjectionPlan SnowballPlan,
    ProjectionPlan AvalanchePlan
);

public record GetProjectionsQuery(Guid UserId) : IRequest<GetProjectionsResponse>;
