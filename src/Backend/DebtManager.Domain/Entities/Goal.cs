using System;
using DebtManager.Domain.Primitives;

namespace DebtManager.Domain.Entities;

public enum GoalType
{
    Savings, // Ahorrar para comprar de contado
    Credit   // Comprar ahora, pagar en cuotas
}

public class Goal : Entity
{
    public Guid UserId { get; private set; }
    public string Name { get; private set; }
    public GoalType Type { get; private set; }
    public decimal TargetAmount { get; private set; }
    public DateTime? TargetDate { get; private set; }
    public decimal? EstimatedMonthlyPayment { get; private set; } // Solo para tipo Credit

    public User User { get; private set; }

    private Goal() { } // EF Core

    public Goal(Guid id, Guid userId, string name, GoalType type, decimal targetAmount, DateTime? targetDate, decimal? estimatedMonthlyPayment) : base(id)
    {
        Id = id;
        UserId = userId;
        Name = name;
        Type = type;
        TargetAmount = targetAmount;
        TargetDate = targetDate;
        EstimatedMonthlyPayment = estimatedMonthlyPayment;
    }

    public void UpdateDetails(string name, GoalType type, decimal targetAmount, DateTime? targetDate, decimal? estimatedMonthlyPayment)
    {
        Name = name;
        Type = type;
        TargetAmount = targetAmount;
        TargetDate = targetDate;
        EstimatedMonthlyPayment = estimatedMonthlyPayment;
    }
}
