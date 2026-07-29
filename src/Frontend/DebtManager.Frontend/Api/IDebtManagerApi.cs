using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Refit;

namespace DebtManager.Frontend.Api;

public interface IDebtManagerApi
{
    [Get("/api/debts/user/{userId}")]
    Task<List<DebtDto>> GetDebtsByUserAsync(Guid userId);

    [Post("/api/debts")]
    Task<CreateDebtResponse> CreateDebtAsync([Body] CreateDebtRequest request);

    [Put("/api/debts/{id}")]
    Task UpdateDebtAsync(Guid id, [Body] UpdateDebtRequest request);

    [Delete("/api/debts/{id}")]
    Task DeleteDebtAsync(Guid id);

    [Get("/api/cashflow/{userId}")]
    Task<FreeCashFlowResponse> GetFreeCashFlowAsync(Guid userId);

    [Post("/api/users/login")]
    Task<AuthResponse> LoginAsync([Body] AuthRequest request);

    [Post("/api/users/register")]
    Task<AuthResponse> RegisterAsync([Body] AuthRequest request);

    [Post("/api/incomes")]
    Task CreateIncomeAsync([Body] CreateIncomeRequest request);

    [Get("/api/incomes/user/{userId}")]
    Task<List<IncomeDto>> GetIncomesByUserAsync(Guid userId);

    [Delete("/api/incomes/{id}")]
    Task DeleteIncomeAsync(Guid id);

    [Get("/api/cashflow/{userId}/projections")]
    Task<GetProjectionsResponseDto> GetProjectionsAsync(Guid userId);

    [Delete("/api/users/{userId}")]
    Task DeleteUserAsync(Guid userId);

    // Financial Planning & Goals
    [Post("/api/fixed-expenses")]
    Task<Guid> CreateFixedExpenseAsync([Body] CreateFixedExpenseRequest request);

    [Get("/api/fixed-expenses/{userId}")]
    Task<List<FixedExpenseDto>> GetFixedExpensesByUserAsync(Guid userId);

    [Put("/api/fixed-expenses/{id}")]
    Task UpdateFixedExpenseAsync(Guid id, [Body] UpdateFixedExpenseRequest request);

    [Delete("/api/fixed-expenses/{id}")]
    Task DeleteFixedExpenseAsync(Guid id);

    [Post("/api/goals")]
    Task<Guid> CreateGoalAsync([Body] CreateGoalRequest request);

    [Get("/api/goals/{userId}")]
    Task<List<GoalDto>> GetGoalsByUserAsync(Guid userId);

    [Get("/api/goals/{userId}/feasibility/{goalId}")]
    Task<FeasibilityResponse> GetFeasibilityAsync(Guid userId, Guid goalId);
}
