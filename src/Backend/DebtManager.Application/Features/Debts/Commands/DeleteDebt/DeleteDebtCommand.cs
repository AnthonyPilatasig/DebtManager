using System;
using MediatR;

namespace DebtManager.Application.Features.Debts.Commands.DeleteDebt;

public record DeleteDebtCommand(Guid Id) : IRequest<bool>;
