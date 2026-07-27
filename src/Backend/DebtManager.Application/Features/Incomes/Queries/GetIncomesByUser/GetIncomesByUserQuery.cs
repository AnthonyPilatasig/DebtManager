using System;
using System.Collections.Generic;
using DebtManager.Domain.Entities;
using MediatR;

namespace DebtManager.Application.Features.Incomes.Queries.GetIncomesByUser;

public record GetIncomesByUserQuery(Guid UserId) : IRequest<List<Income>>;
