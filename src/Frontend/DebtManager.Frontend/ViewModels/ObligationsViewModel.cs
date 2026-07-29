using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DebtManager.Frontend.Api;
using DebtManager.Frontend.Services;

namespace DebtManager.Frontend.ViewModels;

public partial class ObligationsViewModel : ViewModelBase
{
    private readonly IDebtManagerApi? _api;

    // --- DEBTS ---
    [ObservableProperty]
    private ObservableCollection<DebtDto> _debts = new();

    [ObservableProperty]
    private string _newDebtName = string.Empty;

    [ObservableProperty]
    private string _newTotalBalance = string.Empty;

    [ObservableProperty]
    private string _newMinimumPayment = string.Empty;

    [ObservableProperty]
    private string _newInterestRate = string.Empty;

    [ObservableProperty]
    private string _newDueDay = string.Empty;

    [ObservableProperty]
    private bool _isCreditCard;

    // --- FIXED EXPENSES ---
    [ObservableProperty]
    private ObservableCollection<FixedExpenseDto> _expenses = new();

    [ObservableProperty]
    private string _newExpenseName = string.Empty;

    [ObservableProperty]
    private string _newExpenseAmount = string.Empty;

    [ObservableProperty]
    private string _newExpenseDueDay = string.Empty;

    [ObservableProperty]
    private bool _isLoading;

    [ObservableProperty]
    private string _errorMessage = string.Empty;

    public ObligationsViewModel() { } // For designer

    public ObligationsViewModel(IDebtManagerApi? api)
    {
        _api = api;
        _ = LoadAllObligationsAsync();
    }

    [RelayCommand]
    private async Task LoadAllObligationsAsync()
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
                var debtsTask = _api.GetDebtsByUserAsync(userId);
                var expensesTask = _api.GetFixedExpensesByUserAsync(userId);

                await Task.WhenAll(debtsTask, expensesTask);

                var debtsList = await debtsTask;
                var expensesList = await expensesTask;

                Dispatcher.UIThread.Post(() =>
                {
                    Debts.Clear();
                    foreach (var d in debtsList)
                        Debts.Add(d);

                    Expenses.Clear();
                    foreach (var e in expensesList)
                        Expenses.Add(e);
                });
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = "Error al cargar obligaciones: " + ex.Message;
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    private async Task AddDebtAsync()
    {
        if (_api == null)
            return;
        if (string.IsNullOrWhiteSpace(NewDebtName))
            return;

        if (
            !decimal.TryParse(NewTotalBalance, out var balance)
            || !decimal.TryParse(NewMinimumPayment, out var minPayment)
            || !decimal.TryParse(NewInterestRate, out var interestRate)
            || !int.TryParse(NewDueDay, out var dueDay)
        )
        {
            ErrorMessage = "Datos de deuda inválidos. Revisa los números.";
            return;
        }

        IsLoading = true;
        ErrorMessage = string.Empty;

        try
        {
            var userId = SessionManager.Instance.CurrentUserId;
            var request = new CreateDebtRequest(
                userId,
                NewDebtName,
                balance,
                minPayment,
                interestRate,
                dueDay,
                IsCreditCard,
                null // CutoffDay
            );

            await _api.CreateDebtAsync(request);

            NewDebtName = string.Empty;
            NewTotalBalance = string.Empty;
            NewMinimumPayment = string.Empty;
            NewInterestRate = string.Empty;
            NewDueDay = string.Empty;
            IsCreditCard = false;

            await LoadAllObligationsAsync();
        }
        catch (Exception ex)
        {
            ErrorMessage = "Error al guardar la deuda: " + ex.Message;
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    private async Task AddExpenseAsync()
    {
        if (_api == null)
            return;
        if (string.IsNullOrWhiteSpace(NewExpenseName))
            return;

        if (
            !decimal.TryParse(NewExpenseAmount, out var amount)
            || !int.TryParse(NewExpenseDueDay, out var dueDay)
        )
        {
            ErrorMessage = "Datos de gasto inválidos. Revisa los números.";
            return;
        }

        IsLoading = true;
        ErrorMessage = string.Empty;

        try
        {
            var userId = SessionManager.Instance.CurrentUserId;
            var request = new CreateFixedExpenseRequest
            {
                UserId = userId,
                Name = NewExpenseName,
                Amount = amount,
                DueDay = dueDay
            };

            await _api.CreateFixedExpenseAsync(request);

            NewExpenseName = string.Empty;
            NewExpenseAmount = string.Empty;
            NewExpenseDueDay = string.Empty;

            await LoadAllObligationsAsync();
        }
        catch (Exception ex)
        {
            ErrorMessage = "Error al guardar el gasto fijo: " + ex.Message;
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    private async Task DeleteDebtAsync(Guid id)
    {
        if (_api == null)
            return;
        try
        {
            await _api.DeleteDebtAsync(id);
            await LoadAllObligationsAsync();
        }
        catch (Exception ex)
        {
            ErrorMessage = "Error al eliminar la deuda: " + ex.Message;
        }
    }

    [RelayCommand]
    private async Task DeleteExpenseAsync(Guid id)
    {
        if (_api == null)
            return;
        try
        {
            await _api.DeleteFixedExpenseAsync(id);
            await LoadAllObligationsAsync();
        }
        catch (Exception ex)
        {
            ErrorMessage = "Error al eliminar el gasto: " + ex.Message;
        }
    }
}
