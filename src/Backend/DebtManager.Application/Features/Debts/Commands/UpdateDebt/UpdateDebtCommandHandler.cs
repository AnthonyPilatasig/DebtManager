using MediatR;
using System.Threading;
using System.Threading.Tasks;
using DebtManager.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DebtManager.Application.Features.Debts.Commands.UpdateDebt;

public class UpdateDebtCommandHandler : IRequestHandler<UpdateDebtCommand, bool>
{
    private readonly IAppDbContext _context;

    public UpdateDebtCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(UpdateDebtCommand request, CancellationToken cancellationToken)
    {
        var debt = await _context.Debts.FirstOrDefaultAsync(d => d.Id == request.Id, cancellationToken);
        
        if (debt == null || debt.IsDeleted) return false;

        // Validaciones básicas de dominio podrían ir aquí o en FluentValidation
        debt.UpdateDetails(request.Name, request.TotalBalance, request.MinimumMonthlyPayment, request.AnnualInterestRate, request.DueDay);
        debt.LastModifiedAt = System.DateTime.UtcNow;
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
