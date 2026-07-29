using System;
using DebtManager.Domain.Primitives;

namespace DebtManager.Domain.Entities;

public sealed class Debt : Entity
{
    public Guid UserId { get; private set; }
    public string Name { get; private set; }
    public decimal TotalBalance { get; private set; }
    public decimal MinimumMonthlyPayment { get; private set; }
    public decimal AnnualInterestRate { get; private set; } // % APR
    public int DueDay { get; private set; }
    public bool IsCreditCard { get; private set; }
    public int? CutoffDay { get; private set; } // Día de corte para TC, mejor precisión
    public int TotalQuotas { get; private set; } // Número de meses / plazo total
    public DateTime StartDate { get; private set; } // Fecha de inicio de la deuda

    // Navegación
    public User User { get; private set; }
    public ICollection<Payment> Payments { get; private set; } = new List<Payment>();

    private Debt() { } // EF Core

    public Debt(
        Guid id,
        Guid userId,
        string name,
        decimal totalBalance,
        decimal minimumMonthlyPayment,
        decimal annualInterestRate,
        int dueDay,
        bool isCreditCard,
        int totalQuotas,
        DateTime startDate,
        int? cutoffDay = null
    )
        : base(id)
    {
        UserId = userId;
        Name = name;
        TotalBalance = totalBalance;
        MinimumMonthlyPayment = minimumMonthlyPayment;
        AnnualInterestRate = annualInterestRate;
        DueDay = dueDay;
        IsCreditCard = isCreditCard;
        TotalQuotas = totalQuotas;
        StartDate = startDate;
        CutoffDay = cutoffDay;
    }

    public void UpdateDetails(
        string name,
        decimal totalBalance,
        decimal minimumMonthlyPayment,
        decimal annualInterestRate,
        int dueDay
    )
    {
        Name = name;
        TotalBalance = totalBalance;
        MinimumMonthlyPayment = minimumMonthlyPayment;
        AnnualInterestRate = annualInterestRate;
        DueDay = dueDay;
    }
}
