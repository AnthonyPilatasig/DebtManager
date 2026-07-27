using MediatR;
using System.Threading;
using System.Threading.Tasks;
using DebtManager.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DebtManager.Application.Features.Incomes.Commands.DeleteIncome;

public class DeleteIncomeCommandHandler : IRequestHandler<DeleteIncomeCommand, bool>
{
    private readonly IAppDbContext _context;

    public DeleteIncomeCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteIncomeCommand request, CancellationToken cancellationToken)
    {
        var income = await _context.Incomes.FirstOrDefaultAsync(i => i.Id == request.IncomeId, cancellationToken);
        
        if (income == null)
            return false;

        income.IsDeleted = true;
        await _context.SaveChangesAsync(cancellationToken);
        
        return true;
    }
}
