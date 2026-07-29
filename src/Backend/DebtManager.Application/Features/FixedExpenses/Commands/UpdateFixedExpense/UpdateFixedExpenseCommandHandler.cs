using System;
using System.Threading;
using System.Threading.Tasks;
using DebtManager.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DebtManager.Application.Features.FixedExpenses.Commands.UpdateFixedExpense;

public class UpdateFixedExpenseCommandHandler : IRequestHandler<UpdateFixedExpenseCommand>
{
    private readonly IAppDbContext _context;

    public UpdateFixedExpenseCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateFixedExpenseCommand request, CancellationToken cancellationToken)
    {
        var expense = await _context.FixedExpenses.FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
        if (expense == null) throw new Exception("Gasto fijo no encontrado");

        expense.UpdateDetails(request.Name, request.Amount, request.DueDay);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
