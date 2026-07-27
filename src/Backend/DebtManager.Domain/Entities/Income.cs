using System;
using DebtManager.Domain.Enums;
using DebtManager.Domain.Primitives;

namespace DebtManager.Domain.Entities;

public sealed class Income : Entity
{
    public Guid UserId { get; private set; }
    public string Description { get; private set; }
    public decimal Amount { get; private set; }
    public IncomeType Type { get; private set; }
    public bool IsActive { get; private set; }

    // Navegación
    public User User { get; private set; }

    private Income() { } // EF Core

    public Income(Guid id, Guid userId, string description, decimal amount, IncomeType type)
        : base(id)
    {
        UserId = userId;
        Description = description;
        Amount = amount;
        Type = type;
        IsActive = true;
    }
}
