using System;
using MediatR;

namespace DebtManager.Application.Features.Debts.Commands.AddDebt;

public record AddDebtCommand(
    Guid UserId,
    string Name,
    decimal TotalBalance,
    decimal MinimumMonthlyPayment,
    decimal AnnualInterestRate,
    int DueDay,
    bool IsCreditCard,
    int? CutoffDay
) : IRequest<Guid>;
