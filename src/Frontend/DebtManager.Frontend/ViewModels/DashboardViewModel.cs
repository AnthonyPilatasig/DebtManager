using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DebtManager.Frontend.Api;
using System;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace DebtManager.Frontend.ViewModels;

public partial class DashboardViewModel : ViewModelBase
{
    private readonly IDebtManagerApi? _api;

    [ObservableProperty]
    private ObservableCollection<IncomeDto> _incomes = new();

    [ObservableProperty]
    private decimal _freeCashFlow;

    [ObservableProperty]
    private decimal _totalDebt;

    [ObservableProperty]
    private string _riskLevel = "Calculando...";

    [ObservableProperty]
    private string _riskColor = "#A0A0A5";

    [ObservableProperty]
    private string _newIncomeName = string.Empty;

    [ObservableProperty]
    private decimal _newIncomeAmount;

    [ObservableProperty]
    private bool _isLoading = false;

    public DashboardViewModel(IDebtManagerApi? api = null)
    {
        _api = api;
        _ = LoadDashboardDataAsync();
    }

    [RelayCommand]
    private async Task AddIncomeAsync()
    {
        if (_api == null || string.IsNullOrWhiteSpace(NewIncomeName) || NewIncomeAmount <= 0) return;

        IsLoading = true;
        try
        {
            var userId = Services.SessionManager.Instance.CurrentUserId;
            var request = new CreateIncomeRequest(userId, NewIncomeName, NewIncomeAmount, 1); // 1 = Fixed (Sueldo)

            await _api.CreateIncomeAsync(request);

            // Reset form
            NewIncomeName = string.Empty;
            NewIncomeAmount = 0;

            await LoadDashboardDataAsync();
        }
        catch (Exception)
        {
            // Ignorar error temporalmente para MVP
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    private async Task DeleteIncomeAsync(Guid incomeId)
    {
        if (_api == null) return;
        IsLoading = true;
        try
        {
            await _api.DeleteIncomeAsync(incomeId);
            await LoadDashboardDataAsync();
        }
        catch (Exception)
        {
            // Ignore for MVP
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    private async Task LoadDashboardDataAsync()
    {
        if (_api == null) return;

        IsLoading = true;
        try
        {
            var userId = Services.SessionManager.Instance.CurrentUserId;
            var debts = await _api.GetDebtsByUserAsync(userId);
            
            decimal totalBalance = 0;
            decimal totalMinimums = 0;
            foreach(var d in debts) 
            {
                totalBalance += d.TotalBalance;
                totalMinimums += d.MinimumMonthlyPayment;
            }
            TotalDebt = totalBalance;

            var cashFlow = await _api.GetFreeCashFlowAsync(userId);
            FreeCashFlow = cashFlow.FreeCashFlow;

            var incomesList = await _api.GetIncomesByUserAsync(userId);
            Incomes = new ObservableCollection<IncomeDto>(incomesList);
            
            // BR-02: Semáforo de Riesgo (TE = Compromisos / Ingresos * 100)
            var totalIncomes = FreeCashFlow + totalMinimums; 
            if (totalIncomes > 0)
            {
                var te = (totalMinimums / totalIncomes) * 100;
                if (te <= 30)
                {
                    RiskLevel = "SALUDABLE";
                    RiskColor = "#34C759"; // Verde iOS
                }
                else if (te <= 50)
                {
                    RiskLevel = "ALERTA";
                    RiskColor = "#FFCC00"; // Amarillo iOS
                }
                else
                {
                    RiskLevel = "CRÍTICO";
                    RiskColor = "#FF3B30"; // Rojo iOS
                }
            }
            else
            {
                RiskLevel = "SIN DATOS";
                RiskColor = "#A0A0A5";
            }
        }
        catch (Exception)
        {
            RiskLevel = "ERROR DE RED";
            RiskColor = "#A0A0A5";
        }
        finally
        {
            IsLoading = false;
        }
    }
}
