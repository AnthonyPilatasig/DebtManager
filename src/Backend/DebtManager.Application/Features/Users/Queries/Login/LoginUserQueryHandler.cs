using System;
using System.Threading;
using System.Threading.Tasks;
using DebtManager.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DebtManager.Application.Features.Users.Queries.Login;

public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, Guid>
{
    private readonly IAppDbContext _context;

    public LoginUserQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(LoginUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(
            u => u.Email == request.Email,
            cancellationToken
        );

        if (user == null)
            throw new UnauthorizedAccessException("Usuario no encontrado. Por favor regístrate.");

        return user.Id;
    }
}
