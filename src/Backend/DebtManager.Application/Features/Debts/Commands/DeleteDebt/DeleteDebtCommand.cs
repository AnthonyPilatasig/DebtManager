using MediatR;
using System;

namespace DebtManager.Application.Features.Debts.Commands.DeleteDebt;

public record DeleteDebtCommand(Guid Id) : IRequest<bool>;
