using System;
using System.Threading;
using System.Threading.Tasks;
using DebtManager.Application.Interfaces;
using DebtManager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DebtManager.Application.Features.Users.Commands.RegisterUser;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Guid>
{
    private readonly IAppDbContext _context;

    public RegisterUserCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await _context.Users.FirstOrDefaultAsync(
            u => u.Email == request.Email,
            cancellationToken
        );

        if (existingUser != null)
            throw new InvalidOperationException("El correo ya está registrado.");

        var newUser = new User(Guid.NewGuid(), request.Email, "USD");
        _context.Users.Add(newUser);
        await _context.SaveChangesAsync(cancellationToken);

        return newUser.Id;
    }
}
