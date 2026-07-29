using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DebtManager.Application.Interfaces;
using DebtManager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DebtManager.Application.Features.FinancialAdvisor.Queries.GetFeasibility;

public class GetFeasibilityQueryHandler : IRequestHandler<GetFeasibilityQuery, FeasibilityResponse>
{
    private readonly IAppDbContext _context;

    public GetFeasibilityQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<FeasibilityResponse> Handle(GetFeasibilityQuery request, CancellationToken cancellationToken)
    {
        // 1. Obtener los datos del usuario
        var incomes = await _context.Incomes
            .Where(i => i.UserId == request.UserId && i.IsActive && !i.IsDeleted)
            .ToListAsync(cancellationToken);

        var debts = await _context.Debts
            .Where(d => d.UserId == request.UserId && !d.IsDeleted)
            .ToListAsync(cancellationToken);

        var fixedExpenses = await _context.FixedExpenses
            .Where(f => f.UserId == request.UserId && !f.IsDeleted)
            .ToListAsync(cancellationToken);

        var goal = await _context.Goals
            .FirstOrDefaultAsync(g => g.Id == request.GoalId && g.UserId == request.UserId && !g.IsDeleted, cancellationToken);

        if (goal == null)
        {
            throw new Exception("Meta no encontrada.");
        }

        // 2. Calcular Flujo de Caja Libre
        var totalIncome = incomes.Sum(i => i.Amount);
        var totalMinimumDebtPayments = debts.Sum(d => d.MinimumMonthlyPayment);
        var totalFixedExpenses = fixedExpenses.Sum(f => f.Amount);

        var freeCashFlow = totalIncome - (totalMinimumDebtPayments + totalFixedExpenses);

        // 3. Evaluar la viabilidad
        bool isFeasible = false;
        string adviceMessage = "";
        string colorStatus = "Verde";

        if (freeCashFlow <= 0)
        {
            return new FeasibilityResponse(
                false,
                freeCashFlow,
                "Tu flujo de caja está en negativo o en cero. No puedes permitirte esta meta actualmente.",
                "Rojo"
            );
        }

        if (goal.Type == GoalType.Savings)
        {
            if (!goal.TargetDate.HasValue)
            {
                throw new Exception("Las metas de ahorro deben tener una fecha objetivo.");
            }

            var monthsToSave = ((goal.TargetDate.Value.Year - DateTime.UtcNow.Year) * 12) + goal.TargetDate.Value.Month - DateTime.UtcNow.Month;
            if (monthsToSave <= 0) monthsToSave = 1;

            var requiredMonthlySavings = goal.TargetAmount / monthsToSave;

            if (freeCashFlow >= requiredMonthlySavings)
            {
                isFeasible = true;
                adviceMessage = $"¡Te alcanza! Necesitas ahorrar {requiredMonthlySavings:C} al mes. Te sobrarán {(freeCashFlow - requiredMonthlySavings):C} para emergencias.";
                colorStatus = "Verde";
            }
            else
            {
                isFeasible = false;
                adviceMessage = $"No te alcanza en el tiempo establecido. Necesitas {requiredMonthlySavings:C} al mes, pero solo te sobran {freeCashFlow:C}.";
                colorStatus = "Amarillo";
            }
        }
        else if (goal.Type == GoalType.Credit)
        {
            if (!goal.EstimatedMonthlyPayment.HasValue)
            {
                throw new Exception("Las metas de crédito deben tener una cuota mensual estimada.");
            }

            var requiredPayment = goal.EstimatedMonthlyPayment.Value;

            if (freeCashFlow >= requiredPayment)
            {
                var remainingMargin = (freeCashFlow - requiredPayment) / totalIncome;
                isFeasible = true;

                if (remainingMargin < 0.10m)
                {
                    adviceMessage = $"Te alcanza para pagar {requiredPayment:C} al mes, pero tu margen de liquidez quedará por debajo del 10%. Es un riesgo alto.";
                    colorStatus = "Amarillo";
                }
                else
                {
                    adviceMessage = $"¡Te alcanza! Puedes asumir la cuota de {requiredPayment:C} y aún tendrás un flujo de caja saludable.";
                    colorStatus = "Verde";
                }
            }
            else
            {
                isFeasible = false;
                adviceMessage = $"Peligro: No puedes asumir una cuota de {requiredPayment:C}. Tu flujo libre es solo {freeCashFlow:C}. Endeudarte asfixiará tus finanzas.";
                colorStatus = "Rojo";
            }
        }

        return new FeasibilityResponse(isFeasible, freeCashFlow, adviceMessage, colorStatus);
    }
}
