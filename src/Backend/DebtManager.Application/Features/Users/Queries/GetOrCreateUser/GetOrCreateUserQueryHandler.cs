using System;
using System.Threading;
using System.Threading.Tasks;
using DebtManager.Application.Interfaces;
using DebtManager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DebtManager.Application.Features.Users.Queries.GetOrCreateUser;

public class GetOrCreateUserQueryHandler : IRequestHandler<GetOrCreateUserQuery, Guid>
{
    private readonly IAppDbContext _context;

    public GetOrCreateUserQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(
        GetOrCreateUserQuery request,
        CancellationToken cancellationToken
    )
    {
        var normalizedEmail = request.Email.Trim().ToLowerInvariant();

        var existingUser = await _context.Users.FirstOrDefaultAsync(
            u => u.Email == normalizedEmail,
            cancellationToken
        );

        if (existingUser != null)
        {
            return existingUser.Id;
        }

        var newUser = new User(Guid.NewGuid(), normalizedEmail, "USD");
        _context.Users.Add(newUser);
        await _context.SaveChangesAsync(cancellationToken);

        return newUser.Id;
    }
}
