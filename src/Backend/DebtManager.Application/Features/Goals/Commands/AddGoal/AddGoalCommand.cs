using System;
using DebtManager.Domain.Entities;
using MediatR;

namespace DebtManager.Application.Features.Goals.Commands.AddGoal;

public record AddGoalCommand(
    Guid UserId,
    string Name,
    GoalType Type,
    decimal TargetAmount,
    DateTime? TargetDate,
    decimal? EstimatedMonthlyPayment
) : IRequest<Guid>;
