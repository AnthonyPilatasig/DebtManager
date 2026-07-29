using System;
using System.Threading;
using System.Threading.Tasks;
using DebtManager.Application.Interfaces;
using DebtManager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DebtManager.Application.Features.Payments.Commands.AddPayment;

public class AddPaymentCommandHandler : IRequestHandler<AddPaymentCommand, Guid>
{
    private readonly IAppDbContext _context;

    public AddPaymentCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(AddPaymentCommand request, CancellationToken cancellationToken)
    {
        var debt = await _context.Debts.FirstOrDefaultAsync(d => d.Id == request.DebtId, cancellationToken);
        if (debt == null)
            throw new Exception("Debt not found");

        var payment = new Payment(
            Guid.NewGuid(),
            request.DebtId,
            request.Amount,
            request.PaymentDate,
            request.IsExtraordinary
        );

        _context.Payments.Add(payment);

        // Update Debt Balance
        var newBalance = debt.TotalBalance - request.Amount;
        if (newBalance < 0) newBalance = 0;

        debt.UpdateDetails(
            debt.Name,
            newBalance,
            debt.MinimumMonthlyPayment,
            debt.AnnualInterestRate,
            debt.DueDay
        );

        // Clear notification for this month if it's a regular payment
        if (!request.IsExtraordinary)
        {
            var monthStart = new DateTime(request.PaymentDate.Year, request.PaymentDate.Month, 1);
            var monthEnd = monthStart.AddMonths(1).AddDays(-1);
            
            var notification = await _context.Notifications
                .FirstOrDefaultAsync(n => n.UserId == debt.UserId && !n.IsRead && n.CreatedAt >= monthStart && n.CreatedAt <= monthEnd && n.Message.Contains(debt.Name), cancellationToken);

            if (notification != null)
            {
                notification.MarkAsRead();
            }
        }

        await _context.SaveChangesAsync(cancellationToken);

        return payment.Id;
    }
}
