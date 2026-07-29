using System;
using System.Threading;
using System.Threading.Tasks;
using DebtManager.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DebtManager.Application.Features.FixedExpenses.Commands.DeleteFixedExpense;

public class DeleteFixedExpenseCommandHandler : IRequestHandler<DeleteFixedExpenseCommand>
{
    private readonly IAppDbContext _context;

    public DeleteFixedExpenseCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteFixedExpenseCommand request, CancellationToken cancellationToken)
    {
        var expense = await _context.FixedExpenses.FirstOrDefaultAsync(
            e => e.Id == request.Id,
            cancellationToken
        );
        if (expense != null)
        {
            _context.FixedExpenses.Remove(expense);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
