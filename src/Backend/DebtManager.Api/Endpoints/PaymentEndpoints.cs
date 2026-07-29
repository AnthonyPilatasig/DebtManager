using System;
using DebtManager.Application.Features.Payments.Commands.AddPayment;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace DebtManager.Api.Endpoints;

public record CreatePaymentResponse(Guid Id);

public static class PaymentEndpoints
{
    public static void MapPaymentEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/payments").WithTags("Payments");

        group
            .MapPost(
                "/",
                async (AddPaymentCommand command, IMediator mediator) =>
                {
                    var paymentId = await mediator.Send(command);
                    return TypedResults.Created($"/api/payments/{paymentId}", new CreatePaymentResponse(paymentId));
                }
            )
            .WithName("AddPayment")
            .WithSummary("Registra un nuevo abono a una deuda")
            .Produces<CreatePaymentResponse>(StatusCodes.Status201Created)
            .ProducesValidationProblem();
    }
}
