using System;
using System.Threading;
using System.Threading.Tasks;
using DebtManager.Application.Interfaces;
using DebtManager.Domain.Entities;
using MediatR;

namespace DebtManager.Application.Features.FixedExpenses.Commands.AddFixedExpense;

public class AddFixedExpenseCommandHandler : IRequestHandler<AddFixedExpenseCommand, Guid>
{
    private readonly IAppDbContext _context;

    public AddFixedExpenseCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(
        AddFixedExpenseCommand request,
        CancellationToken cancellationToken
    )
    {
        var expense = new FixedExpense(
            Guid.NewGuid(),
            request.UserId,
            request.Name,
            request.Amount,
            request.DueDay
        );

        _context.FixedExpenses.Add(expense);
        await _context.SaveChangesAsync(cancellationToken);

        return expense.Id;
    }
}
