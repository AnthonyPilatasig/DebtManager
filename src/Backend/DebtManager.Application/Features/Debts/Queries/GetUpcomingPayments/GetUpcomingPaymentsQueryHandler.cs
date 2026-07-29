using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DebtManager.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DebtManager.Application.Features.Debts.Queries.GetUpcomingPayments;

public class GetUpcomingPaymentsQueryHandler
    : IRequestHandler<GetUpcomingPaymentsQuery, List<UpcomingPaymentDto>>
{
    private readonly IAppDbContext _context;

    public GetUpcomingPaymentsQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<List<UpcomingPaymentDto>> Handle(
        GetUpcomingPaymentsQuery request,
        CancellationToken cancellationToken
    )
    {
        var debts = await _context
            .Debts.Where(d => d.UserId == request.UserId && !d.IsDeleted && d.TotalBalance > 0)
            .ToListAsync(cancellationToken);

        var upcomingPayments = new List<UpcomingPaymentDto>();
        var today = DateTime.UtcNow.Date;

        foreach (var debt in debts)
        {
            // Determine next due date
            var nextDueDate = new DateTime(today.Year, today.Month, debt.DueDay);
            if (nextDueDate < today)
            {
                nextDueDate = nextDueDate.AddMonths(1);
            }

            var daysRemaining = (nextDueDate - today).Days;

            upcomingPayments.Add(
                new UpcomingPaymentDto(
                    debt.Id,
                    debt.Name,
                    debt.MinimumMonthlyPayment,
                    nextDueDate,
                    daysRemaining
                )
            );
        }

        return upcomingPayments.OrderBy(p => p.DaysRemaining).ToList();
    }
}
