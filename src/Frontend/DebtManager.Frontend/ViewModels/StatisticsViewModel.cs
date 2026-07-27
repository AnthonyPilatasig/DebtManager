using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DebtManager.Frontend.Api;
using DebtManager.Frontend.Services;

namespace DebtManager.Frontend.ViewModels;

public partial class StatisticsViewModel : ViewModelBase
{
    private readonly IDebtManagerApi? _api;

    [ObservableProperty]
    private decimal _freeCashFlow;

    [ObservableProperty]
    private ObservableCollection<DebtDto> _snowballPlan = new();

    [ObservableProperty]
    private ObservableCollection<DebtDto> _avalanchePlan = new();

    [ObservableProperty]
    private bool _isLoading = false;

    public StatisticsViewModel(IDebtManagerApi? api = null)
    {
        _api = api;
        Task.Run(LoadProjectionsAsync);
    }

    [RelayCommand]
    private async Task LoadProjectionsAsync()
    {
        if (_api == null)
            return;

        IsLoading = true;
        try
        {
            var userId = SessionManager.Instance.CurrentUserId;
            var projections = await _api.GetProjectionsAsync(userId);

            FreeCashFlow = projections.FreeCashFlow;

            SnowballPlan = new ObservableCollection<DebtDto>(projections.SnowballPlan.OrderedDebts);
            AvalanchePlan = new ObservableCollection<DebtDto>(
                projections.AvalanchePlan.OrderedDebts
            );
        }
        catch (Exception)
        {
            // Ignorar para MVP en caso de red
        }
        finally
        {
            IsLoading = false;
        }
    }
}
