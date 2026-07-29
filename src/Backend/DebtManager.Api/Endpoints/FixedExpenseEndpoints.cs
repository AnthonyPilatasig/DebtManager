using DebtManager.Application.Features.FixedExpenses.Commands.AddFixedExpense;
using DebtManager.Application.Features.FixedExpenses.Commands.UpdateFixedExpense;
using DebtManager.Application.Features.FixedExpenses.Queries.GetFixedExpenses;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace DebtManager.Api.Endpoints;

public static class FixedExpenseEndpoints
{
    public static void MapFixedExpenseEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/fixed-expenses").WithTags("Fixed Expenses");

        group.MapPost("/", async (AddFixedExpenseCommand command, IMediator mediator) =>
        {
            var id = await mediator.Send(command);
            return Results.Created($"/api/fixed-expenses/{id}", id);
        });

        group.MapGet("/{userId}", async (Guid userId, IMediator mediator) =>
        {
            var expenses = await mediator.Send(new GetFixedExpensesQuery(userId));
            return Results.Ok(expenses);
        });
        group.MapPut("/{id}", async (Guid id, UpdateFixedExpenseCommand command, IMediator mediator) =>
        {
            if (id != command.Id) return Results.BadRequest();
            await mediator.Send(command);
            return Results.NoContent();
        });

        group.MapDelete("/{id}", async (Guid id, IMediator mediator) =>
        {
            await mediator.Send(new DebtManager.Application.Features.FixedExpenses.Commands.DeleteFixedExpense.DeleteFixedExpenseCommand(id));
            return Results.NoContent();
        });
    }
}
