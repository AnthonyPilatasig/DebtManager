using System;
using MediatR;

namespace DebtManager.Application.Features.Payments.Commands.AddPayment;

public record AddPaymentCommand(
    Guid DebtId,
    decimal Amount,
    DateTime PaymentDate,
    bool IsExtraordinary
) : IRequest<Guid>;
