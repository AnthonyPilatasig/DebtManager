using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DebtManager.Frontend.Api;
using DebtManager.Frontend.Services;

namespace DebtManager.Frontend.ViewModels;

public partial class GoalsViewModel : ViewModelBase
{
    private readonly IDebtManagerApi? _api;

    [ObservableProperty]
    private ObservableCollection<GoalDto> _goals = new();

    [ObservableProperty]
    private GoalDto? _selectedGoal;

    [ObservableProperty]
    private FeasibilityResponse? _feasibilityResult;

    [ObservableProperty]
    private string _newName = string.Empty;

    [ObservableProperty]
    private int _selectedTypeIndex = 0; // 0 = Ahorro, 1 = Crédito

    [ObservableProperty]
    private string _newTargetAmount = string.Empty;

    [ObservableProperty]
    private DateTimeOffset? _newTargetDate = DateTimeOffset.Now.AddMonths(3);

    [ObservableProperty]
    private string _newEstimatedPayment = string.Empty;

    [ObservableProperty]
    private bool _isLoading;

    [ObservableProperty]
    private string _errorMessage = string.Empty;

    public GoalsViewModel() { } // Para el diseñador

    public GoalsViewModel(IDebtManagerApi? api)
    {
        _api = api;
        _ = LoadGoalsCommand.ExecuteAsync(null);
    }

    [RelayCommand]
    private async Task LoadGoalsAsync()
    {
        if (_api == null)
            return;
        IsLoading = true;
        ErrorMessage = string.Empty;

        try
        {
            var userId = SessionManager.Instance.CurrentUserId;
            if (userId != Guid.Empty)
            {
                var list = await _api.GetGoalsByUserAsync(userId);
                Dispatcher.UIThread.Post(() =>
                {
                    Goals.Clear();
                    foreach (var g in list)
                    {
                        Goals.Add(g);
                    }
                });
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = "Error al cargar metas: " + ex.Message;
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    private async Task AddGoalAsync()
    {
        if (_api == null)
            return;
        if (string.IsNullOrWhiteSpace(NewName))
            return;

        if (!decimal.TryParse(NewTargetAmount, out var targetAmount))
        {
            ErrorMessage = "Costo Total inválido.";
            return;
        }

        var type = SelectedTypeIndex == 0 ? GoalType.Savings : GoalType.Credit;

        decimal? estimatedPayment = null;
        DateTime? targetDate = null;

        if (type == GoalType.Savings)
        {
            if (NewTargetDate == null)
            {
                ErrorMessage = "Selecciona una fecha objetivo para el ahorro.";
                return;
            }
            targetDate = NewTargetDate.Value.UtcDateTime;
        }
        else
        {
            if (!decimal.TryParse(NewEstimatedPayment, out var payment))
            {
                ErrorMessage = "Cuota mensual inválida.";
                return;
            }
            estimatedPayment = payment;
        }

        IsLoading = true;
        ErrorMessage = string.Empty;

        try
        {
            var userId = SessionManager.Instance.CurrentUserId;
            var request = new CreateGoalRequest
            {
                UserId = userId,
                Name = NewName,
                Type = type,
                TargetAmount = targetAmount,
                TargetDate = targetDate,
                EstimatedMonthlyPayment = estimatedPayment
            };

            await _api.CreateGoalAsync(request);

            NewName = string.Empty;
            NewTargetAmount = string.Empty;
            NewEstimatedPayment = string.Empty;

            await LoadGoalsAsync();
        }
        catch (Exception ex)
        {
            ErrorMessage = "Error al guardar la meta: " + ex.Message;
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    private async Task EvaluateGoalAsync(GoalDto goal)
    {
        if (_api == null || goal == null)
            return;

        IsLoading = true;
        ErrorMessage = string.Empty;
        SelectedGoal = goal;
        FeasibilityResult = null;

        try
        {
            var userId = SessionManager.Instance.CurrentUserId;
            var result = await _api.GetFeasibilityAsync(userId, goal.Id);
            Dispatcher.UIThread.Post(() =>
            {
                FeasibilityResult = result;
            });
        }
        catch (Exception ex)
        {
            ErrorMessage = "Error al evaluar meta: " + ex.Message;
        }
        finally
        {
            IsLoading = false;
        }
    }
}
