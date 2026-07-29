using System;

namespace DebtManager.Frontend.Api;

public class CreateFixedExpenseRequest
{
    public Guid UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public int DueDay { get; set; }
}

public class UpdateFixedExpenseRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public int DueDay { get; set; }
}

public class FixedExpenseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public int DueDay { get; set; }
}

public enum GoalType
{
    Savings, // Ahorrar para comprar de contado
    Credit // Comprar ahora, pagar en cuotas
}

public class CreateGoalRequest
{
    public Guid UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public GoalType Type { get; set; }
    public decimal TargetAmount { get; set; }
    public DateTime? TargetDate { get; set; }
    public decimal? EstimatedMonthlyPayment { get; set; }
}

public class GoalDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public GoalType Type { get; set; }
    public decimal TargetAmount { get; set; }
    public DateTime? TargetDate { get; set; }
    public decimal? EstimatedMonthlyPayment { get; set; }
}

public class FeasibilityResponse
{
    public bool IsFeasible { get; set; }
    public decimal FreeCashFlow { get; set; }
    public string AdviceMessage { get; set; } = string.Empty;
    public string ColorStatus { get; set; } = "Verde";
}
