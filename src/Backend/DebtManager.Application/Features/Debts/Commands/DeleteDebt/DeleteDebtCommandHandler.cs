using System.Threading;
using System.Threading.Tasks;
using DebtManager.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DebtManager.Application.Features.Debts.Commands.DeleteDebt;

public class DeleteDebtCommandHandler : IRequestHandler<DeleteDebtCommand, bool>
{
    private readonly IAppDbContext _context;

    public DeleteDebtCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteDebtCommand request, CancellationToken cancellationToken)
    {
        var debt = await _context.Debts.FirstOrDefaultAsync(
            d => d.Id == request.Id,
            cancellationToken
        );

        if (debt == null || debt.IsDeleted)
            return false;

        debt.IsDeleted = true;
        debt.LastModifiedAt = System.DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
