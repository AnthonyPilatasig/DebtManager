using MediatR;
using System;

namespace DebtManager.Application.Features.Incomes.Commands.AddIncome;

public record AddIncomeCommand(
    Guid UserId,
    string Description,
    decimal Amount,
    int Type // 1 = Fixed, 2 = Variable
) : IRequest<Guid>;
