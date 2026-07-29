using System;
using System.Collections.Generic;
using DebtManager.Domain.Entities;
using MediatR;

namespace DebtManager.Application.Features.Goals.Queries.GetGoals;

public record GoalDto(
    Guid Id,
    string Name,
    GoalType Type,
    decimal TargetAmount,
    DateTime? TargetDate,
    decimal? EstimatedMonthlyPayment
);

public record GetGoalsQuery(Guid UserId) : IRequest<List<GoalDto>>;
