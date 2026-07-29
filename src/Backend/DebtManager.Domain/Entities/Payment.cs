using System;
using DebtManager.Domain.Primitives;

namespace DebtManager.Domain.Entities;

public sealed class Payment : Entity
{
    public Guid DebtId { get; private set; }
    public decimal Amount { get; private set; }
    public DateTime PaymentDate { get; private set; }
    public bool IsExtraordinary { get; private set; }

    // Navegación
    public Debt Debt { get; private set; }

    private Payment() { }

    public Payment(Guid id, Guid debtId, decimal amount, DateTime paymentDate, bool isExtraordinary)
        : base(id)
    {
        DebtId = debtId;
        Amount = amount;
        PaymentDate = paymentDate;
        IsExtraordinary = isExtraordinary;
    }
}
