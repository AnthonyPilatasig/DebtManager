using System;
using MediatR;

namespace DebtManager.Application.Features.FixedExpenses.Commands.UpdateFixedExpense;

public record UpdateFixedExpenseCommand(Guid Id, string Name, decimal Amount, int DueDay) : IRequest;
