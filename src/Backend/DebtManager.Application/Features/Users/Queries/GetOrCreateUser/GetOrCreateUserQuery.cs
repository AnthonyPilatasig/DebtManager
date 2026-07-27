using MediatR;
using System;

namespace DebtManager.Application.Features.Users.Queries.GetOrCreateUser;

public record GetOrCreateUserQuery(string Email) : IRequest<Guid>;
