using MediatR;
using System;
using System.Collections.Generic;
using DebtManager.Domain.Entities;

namespace DebtManager.Application.Features.Debts.Queries.GetDebtsByUserId;

public record GetDebtsByUserIdQuery(Guid UserId) : IRequest<List<Debt>>;
