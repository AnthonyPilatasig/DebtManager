using System;
using MediatR;

namespace DebtManager.Application.Features.FixedExpenses.Commands.AddFixedExpense;

public record AddFixedExpenseCommand(
    Guid UserId,
    string Name,
    decimal Amount,
    int DueDay
) : IRequest<Guid>;
