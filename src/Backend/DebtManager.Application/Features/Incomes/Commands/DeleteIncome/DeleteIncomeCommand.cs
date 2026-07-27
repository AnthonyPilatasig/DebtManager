using System;
using MediatR;

namespace DebtManager.Application.Features.Incomes.Commands.DeleteIncome;

public record DeleteIncomeCommand(Guid IncomeId) : IRequest<bool>;
