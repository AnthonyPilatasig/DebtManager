using System;
using System.Threading;
using System.Threading.Tasks;
using DebtManager.Application.Interfaces;
using DebtManager.Domain.Entities;
using MediatR;

namespace DebtManager.Application.Features.Goals.Commands.AddGoal;

public class AddGoalCommandHandler : IRequestHandler<AddGoalCommand, Guid>
{
    private readonly IAppDbContext _context;

    public AddGoalCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(AddGoalCommand request, CancellationToken cancellationToken)
    {
        var goal = new Goal(
            Guid.NewGuid(),
            request.UserId,
            request.Name,
            request.Type,
            request.TargetAmount,
            request.TargetDate,
            request.EstimatedMonthlyPayment
        );

        _context.Goals.Add(goal);
        await _context.SaveChangesAsync(cancellationToken);

        return goal.Id;
    }
}
