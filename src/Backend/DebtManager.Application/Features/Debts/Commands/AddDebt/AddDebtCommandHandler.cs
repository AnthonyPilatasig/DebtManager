using DebtManager.Application.Interfaces;
using DebtManager.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DebtManager.Application.Features.Debts.Commands.AddDebt;

public class AddDebtCommandHandler : IRequestHandler<AddDebtCommand, Guid>
{
    private readonly IAppDbContext _context;

    public AddDebtCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(AddDebtCommand request, CancellationToken cancellationToken)
    {
        var debt = new Debt(
            Guid.NewGuid(),
            request.UserId,
            request.Name,
            request.TotalBalance,
            request.MinimumMonthlyPayment,
            request.AnnualInterestRate,
            request.DueDay,
            request.IsCreditCard,
            request.CutoffDay
        );

        _context.Debts.Add(debt);
        await _context.SaveChangesAsync(cancellationToken);

        return debt.Id;
    }
}
