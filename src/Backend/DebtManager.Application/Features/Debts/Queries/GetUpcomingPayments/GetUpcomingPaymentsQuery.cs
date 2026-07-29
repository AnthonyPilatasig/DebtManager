using System;
using System.Collections.Generic;
using MediatR;

namespace DebtManager.Application.Features.Debts.Queries.GetUpcomingPayments;

public record UpcomingPaymentDto(
    Guid DebtId,
    string DebtName,
    decimal Amount,
    DateTime DueDate,
    int DaysRemaining
);

public record GetUpcomingPaymentsQuery(Guid UserId) : IRequest<List<UpcomingPaymentDto>>;
