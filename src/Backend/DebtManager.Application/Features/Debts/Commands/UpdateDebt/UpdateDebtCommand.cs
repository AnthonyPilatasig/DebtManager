using MediatR;
using System;

namespace DebtManager.Application.Features.Debts.Commands.UpdateDebt;

public record UpdateDebtCommand(
    Guid Id,
    string Name,
    decimal TotalBalance,
    decimal MinimumMonthlyPayment,
    decimal AnnualInterestRate,
    int DueDay) : IRequest<bool>;
