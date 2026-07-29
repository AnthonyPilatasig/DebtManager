using System;
using MediatR;

namespace DebtManager.Application.Features.FinancialAdvisor.Queries.GetFeasibility;

public record FeasibilityResponse(
    bool IsFeasible,
    decimal FreeCashFlow,
    string AdviceMessage,
    string ColorStatus // "Verde", "Amarillo", "Rojo"
);

public record GetFeasibilityQuery(Guid UserId, Guid GoalId) : IRequest<FeasibilityResponse>;
