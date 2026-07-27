using System;
using System.Collections.Generic;
using DebtManager.Domain.Entities;
using MediatR;

namespace DebtManager.Application.Features.Debts.Queries.GetDebtsByUserId;

public record GetDebtsByUserIdQuery(Guid UserId) : IRequest<List<Debt>>;
