using System;
using MediatR;

namespace DebtManager.Application.Features.Users.Queries.Login;

public record LoginUserQuery(string Email) : IRequest<Guid>;
