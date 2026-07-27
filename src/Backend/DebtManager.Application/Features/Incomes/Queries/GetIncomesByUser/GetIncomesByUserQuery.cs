using MediatR;
using System;
using System.Collections.Generic;
using DebtManager.Domain.Entities;

namespace DebtManager.Application.Features.Incomes.Queries.GetIncomesByUser;

public record GetIncomesByUserQuery(Guid UserId) : IRequest<List<Income>>;
