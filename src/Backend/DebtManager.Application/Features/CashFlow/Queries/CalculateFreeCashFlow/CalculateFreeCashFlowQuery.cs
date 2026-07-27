using MediatR;
using System;

namespace DebtManager.Application.Features.CashFlow.Queries.CalculateFreeCashFlow;

public record CalculateFreeCashFlowQuery(Guid UserId) : IRequest<decimal>;
