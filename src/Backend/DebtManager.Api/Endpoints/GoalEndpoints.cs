using DebtManager.Application.Features.FinancialAdvisor.Queries.GetFeasibility;
using DebtManager.Application.Features.Goals.Commands.AddGoal;
using DebtManager.Application.Features.Goals.Queries.GetGoals;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace DebtManager.Api.Endpoints;

public static class GoalEndpoints
{
    public static void MapGoalEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/goals").WithTags("Goals and Financial Advisor");

        group.MapPost(
            "/",
            async (AddGoalCommand command, IMediator mediator) =>
            {
                var id = await mediator.Send(command);
                return Results.Created($"/api/goals/{id}", id);
            }
        );

        group.MapGet(
            "/{userId}",
            async (Guid userId, IMediator mediator) =>
            {
                var goals = await mediator.Send(new GetGoalsQuery(userId));
                return Results.Ok(goals);
            }
        );

        group.MapGet(
            "/{userId}/feasibility/{goalId}",
            async (Guid userId, Guid goalId, IMediator mediator) =>
            {
                var feasibility = await mediator.Send(new GetFeasibilityQuery(userId, goalId));
                return Results.Ok(feasibility);
            }
        );
    }
}
