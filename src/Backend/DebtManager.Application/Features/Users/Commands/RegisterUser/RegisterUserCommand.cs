using System;
using MediatR;

namespace DebtManager.Application.Features.Users.Commands.RegisterUser;

public record RegisterUserCommand(string Email) : IRequest<Guid>;
