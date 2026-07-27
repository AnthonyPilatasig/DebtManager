using System;

namespace DebtManager.Frontend.Api;

public record DebtDto(Guid Id, Guid UserId, string Name, decimal TotalBalance, decimal MinimumMonthlyPayment, decimal AnnualInterestRate, int DueDay, bool IsCreditCard, int? CutoffDay);

public record CreateDebtRequest(Guid UserId, string Name, decimal TotalBalance, decimal MinimumMonthlyPayment, decimal AnnualInterestRate, int DueDay, bool IsCreditCard, int? CutoffDay);
public record CreateDebtResponse(Guid Id);

public record UpdateDebtRequest(Guid Id, string Name, decimal TotalBalance, decimal MinimumMonthlyPayment, decimal AnnualInterestRate, int DueDay);

public record FreeCashFlowResponse(Guid UserId, decimal FreeCashFlow);
