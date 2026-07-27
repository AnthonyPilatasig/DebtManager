using System;
using System.Collections.Generic;
using DebtManager.Domain.Primitives;

namespace DebtManager.Domain.Entities;

public sealed class User : Entity
{
    public string Email { get; private set; }
    public string BaseCurrency { get; private set; } // ISO 4217, e.g. "USD"
    public DateTimeOffset CreatedAt { get; private set; }

    // Navegación EF Core
    public ICollection<Income> Incomes { get; private set; } = new List<Income>();
    public ICollection<Debt> Debts { get; private set; } = new List<Debt>();

    private User() { } // Para EF Core

    public User(Guid id, string email, string baseCurrency)
        : base(id)
    {
        Email = email;
        BaseCurrency = baseCurrency;
        CreatedAt = DateTimeOffset.UtcNow;
    }
}
