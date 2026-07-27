using DebtManager.Application.Interfaces;
using DebtManager.Domain.Entities;
using DebtManager.Domain.Enums;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DebtManager.Application.Features.Incomes.Commands.AddIncome;

public class AddIncomeCommandHandler : IRequestHandler<AddIncomeCommand, Guid>
{
    private readonly IAppDbContext _context;

    public AddIncomeCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(AddIncomeCommand request, CancellationToken cancellationToken)
    {
        var income = new Income(
            Guid.NewGuid(),
            request.UserId,
            request.Description,
            request.Amount,
            (IncomeType)request.Type
        );

        _context.Incomes.Add(income);
        await _context.SaveChangesAsync(cancellationToken);

        return income.Id;
    }
}
