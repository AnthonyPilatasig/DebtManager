using System;
using System.Threading.Tasks;
using DebtManager.Domain.Entities;
using DebtManager.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace DebtManager.Infrastructure.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        // 1. Evitar duplicidad comprobando si ya existe el usuario principal
        var userId = Guid.Parse("00000000-0000-0000-0000-000000000001");

        if (await context.Users.AnyAsync(u => u.Id == userId))
        {
            return; // Ya se ha ejecutado el seed, no hacemos nada para no duplicar datos
        }

        var now = DateTime.UtcNow;
        var startDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        // 2. Insertar Usuario
        var user = new User(userId, "test@debtmanager.com", "USD");
        context.Users.Add(user);

        // 3. Insertar Ingresos
        var income1 = new Income(
            Guid.Parse("10000000-0000-0000-0000-000000000001"),
            userId,
            "Sueldo Fijo Mensual",
            2500.00m,
            IncomeType.Fixed
        );
        var income2 = new Income(
            Guid.Parse("10000000-0000-0000-0000-000000000002"),
            userId,
            "Trabajos Freelance",
            500.00m,
            IncomeType.Variable
        );
        context.Incomes.AddRange(income1, income2);

        // 4. Insertar Deudas
        var creditCardId = Guid.Parse("20000000-0000-0000-0000-000000000001");
        var carLoanId = Guid.Parse("20000000-0000-0000-0000-000000000002");

        var cc = new Debt(
            creditCardId,
            userId,
            "Tarjeta de Crédito Visa",
            1200.00m,
            60.00m,
            18.5m,
            15,
            true,
            24,
            startDate,
            30
        );
        var loan = new Debt(
            carLoanId,
            userId,
            "Préstamo Vehicular",
            8500.00m,
            250.00m,
            12.0m,
            5,
            false,
            60,
            startDate,
            null
        );
        context.Debts.AddRange(cc, loan);

        // 5. Insertar Pagos
        var payment = new Payment(
            Guid.Parse("30000000-0000-0000-0000-000000000001"),
            carLoanId,
            250.00m,
            new DateTime(2026, 2, 5, 0, 0, 0, DateTimeKind.Utc),
            false
        );
        context.Payments.Add(payment);

        // 6. Insertar Gastos Fijos
        var expense1 = new FixedExpense(
            Guid.Parse("40000000-0000-0000-0000-000000000001"),
            userId,
            "Arriendo",
            400.00m,
            1
        );
        var expense2 = new FixedExpense(
            Guid.Parse("40000000-0000-0000-0000-000000000002"),
            userId,
            "Comida (Supermercado)",
            300.00m,
            5
        );
        context.FixedExpenses.AddRange(expense1, expense2);

        // 7. Insertar Metas
        var goal1 = new Goal(
            Guid.Parse("50000000-0000-0000-0000-000000000001"),
            userId,
            "Comprar PS5 (Contado)",
            GoalType.Savings,
            500.00m,
            new DateTime(2026, 12, 1, 0, 0, 0, DateTimeKind.Utc),
            null
        );
        var goal2 = new Goal(
            Guid.Parse("50000000-0000-0000-0000-000000000002"),
            userId,
            "Laptop Nueva (Crédito)",
            GoalType.Credit,
            1500.00m,
            null,
            125.00m
        );
        context.Goals.AddRange(goal1, goal2);

        // 8. Guardar todo
        await context.SaveChangesAsync();
    }
}
