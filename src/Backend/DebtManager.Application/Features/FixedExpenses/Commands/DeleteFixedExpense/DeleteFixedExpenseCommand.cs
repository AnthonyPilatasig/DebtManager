using System;
using MediatR;

namespace DebtManager.Application.Features.FixedExpenses.Commands.DeleteFixedExpense;

public record DeleteFixedExpenseCommand(Guid Id) : IRequest;
