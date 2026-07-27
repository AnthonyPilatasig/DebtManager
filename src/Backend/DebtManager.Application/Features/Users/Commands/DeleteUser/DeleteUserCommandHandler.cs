using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DebtManager.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DebtManager.Application.Features.Users.Commands.DeleteUser;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
{
    private readonly IAppDbContext _context;

    public DeleteUserCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _context
            .Users.Include(u => u.Debts)
            .Include(u => u.Incomes)
            .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

        if (user == null)
            return false;

        // Cascade delete (borrado físico total por GDPR/LOPDP)
        _context.Debts.RemoveRange(user.Debts);
        _context.Incomes.RemoveRange(user.Incomes);
        _context.Users.Remove(user);

        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
