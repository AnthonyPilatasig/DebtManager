using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DebtManager.Frontend.Api;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace DebtManager.Frontend.ViewModels;

public partial class DebtsViewModel : ViewModelBase
{
    private readonly IDebtManagerApi? _api;

    [ObservableProperty]
    private ObservableCollection<DebtDto> _debts = new();

    [ObservableProperty]
    private bool _isLoading = false;

    [ObservableProperty]
    private bool _isFixedExpense = false;

    [ObservableProperty]
    private string _newDebtName = string.Empty;

    [ObservableProperty]
    private decimal _newDebtTotalBalance;

    [ObservableProperty]
    private decimal _newDebtMinimumPayment;

    [ObservableProperty]
    private decimal _newDebtInterestRate;

    public DebtsViewModel(IDebtManagerApi? api = null)
    {
        _api = api;
        _ = LoadDebtsAsync();
    }

    [RelayCommand]
    private async Task AddDebtAsync()
    {
        if (_api == null || string.IsNullOrWhiteSpace(NewDebtName)) return;

        // Si es gasto fijo, ignoramos los valores de saldo y tasa (los forzamos a 0), y solo requerimos cuota mínima
        if (IsFixedExpense && NewDebtMinimumPayment <= 0) return;
        
        // Si es deuda, requerimos saldo total
        if (!IsFixedExpense && NewDebtTotalBalance <= 0) return;

        var balanceToSave = IsFixedExpense ? 0 : NewDebtTotalBalance;
        var interestToSave = IsFixedExpense ? 0 : NewDebtInterestRate;

        IsLoading = true;
        try
        {
            var userId = Services.SessionManager.Instance.CurrentUserId;
            var request = new CreateDebtRequest(
                UserId: userId,
                Name: NewDebtName,
                TotalBalance: balanceToSave,
                MinimumMonthlyPayment: NewDebtMinimumPayment,
                AnnualInterestRate: interestToSave,
                DueDay: 1,
                IsCreditCard: false,
                CutoffDay: null
            );

            await _api.CreateDebtAsync(request);

            // Reset form
            NewDebtName = string.Empty;
            NewDebtTotalBalance = 0;
            NewDebtMinimumPayment = 0;
            NewDebtInterestRate = 0;
            IsFixedExpense = false;

            await LoadDebtsAsync(); // Refrescar lista
        }
        catch (Exception)
        {
            // Manejar error (en un MVP podríamos ignorarlo temporalmente o poner un mensaje)
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    private async Task LoadDebtsAsync()
    {
        if (_api == null) return;

        IsLoading = true;
        try
        {
            var userId = Services.SessionManager.Instance.CurrentUserId;
            var result = await _api.GetDebtsByUserAsync(userId);
            Debts = new ObservableCollection<DebtDto>(result);
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    private async Task DeleteDebtAsync(Guid debtId)
    {
        if (_api == null) return;
        try
        {
            await _api.DeleteDebtAsync(debtId);
            await LoadDebtsAsync(); // Recargar tras borrar
        }
        catch (Exception) { }
    }
}
