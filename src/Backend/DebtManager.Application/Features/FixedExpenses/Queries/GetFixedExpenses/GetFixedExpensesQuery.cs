using System;
using System.Collections.Generic;
using MediatR;

namespace DebtManager.Application.Features.FixedExpenses.Queries.GetFixedExpenses;

public record FixedExpenseDto(Guid Id, string Name, decimal Amount, int DueDay);

public record GetFixedExpensesQuery(Guid UserId) : IRequest<List<FixedExpenseDto>>;
