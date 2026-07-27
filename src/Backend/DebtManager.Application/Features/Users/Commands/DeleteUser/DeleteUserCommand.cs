using System;
using MediatR;

namespace DebtManager.Application.Features.Users.Commands.DeleteUser;

public record DeleteUserCommand(Guid UserId) : IRequest<bool>;
