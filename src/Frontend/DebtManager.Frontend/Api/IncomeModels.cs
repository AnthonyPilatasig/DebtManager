using System;

namespace DebtManager.Frontend.Api;

public record CreateIncomeRequest(
    Guid UserId,
    string Description,
    decimal Amount,
    int Type // 1 = Fixed, 2 = Variable
);

public record IncomeDto(Guid Id, string Description, decimal Amount, int Type, bool IsActive);
