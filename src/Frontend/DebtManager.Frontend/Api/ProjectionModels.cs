using System;
using System.Collections.Generic;

namespace DebtManager.Frontend.Api;

public record ProjectionPlanDto(
    string StrategyName,
    List<DebtDto> OrderedDebts
);

public record GetProjectionsResponseDto(
    decimal FreeCashFlow,
    ProjectionPlanDto SnowballPlan,
    ProjectionPlanDto AvalanchePlan
);
