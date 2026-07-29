using System;
using DebtManager.Domain.Primitives;

namespace DebtManager.Domain.Entities;

public class FixedExpense : Entity
{
    public Guid UserId { get; private set; }
    public string Name { get; private set; }
    public decimal Amount { get; private set; }
    public int DueDay { get; private set; }

    public User User { get; private set; }

    private FixedExpense() { } // EF Core

    public FixedExpense(Guid id, Guid userId, string name, decimal amount, int dueDay)
        : base(id)
    {
        Id = id;
        UserId = userId;
        Name = name;
        Amount = amount;
        DueDay = dueDay;
    }

    public void UpdateDetails(string name, decimal amount, int dueDay)
    {
        Name = name;
        Amount = amount;
        DueDay = dueDay;
    }
}
