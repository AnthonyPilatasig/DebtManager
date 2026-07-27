using MediatR;
using System;

namespace DebtManager.Application.Features.Users.Queries.Login;

public record LoginUserQuery(string Email) : IRequest<Guid>;
