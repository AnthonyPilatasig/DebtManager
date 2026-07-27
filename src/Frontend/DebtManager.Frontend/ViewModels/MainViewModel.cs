using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DebtManager.Frontend.Api;
using DebtManager.Frontend.Services;
using System;

namespace DebtManager.Frontend.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    private readonly IDebtManagerApi? _api;

    [ObservableProperty]
    private ViewModelBase _currentPage;

    [ObservableProperty]
    private bool _isPaneOpen = true;

    [ObservableProperty]
    private bool _isAuthenticated = false;

    public MainViewModel(IDebtManagerApi api)
    {
        _api = api;
        
        // Empezar en Onboarding
        _currentPage = new OnboardingViewModel(_api, OnUserAuthenticated);
        IsPaneOpen = false; // Ocultar sidebar
    }

    public MainViewModel() 
    { 
        _currentPage = new OnboardingViewModel(null, () => {});
    }

    private void OnUserAuthenticated()
    {
        IsAuthenticated = true;
        IsPaneOpen = true; // Mostrar sidebar
        CurrentPage = new DashboardViewModel(_api);
    }

    [RelayCommand]
    private void NavigateToDashboard() => CurrentPage = new DashboardViewModel(_api);

    [RelayCommand]
    private void NavigateToDebts() => CurrentPage = new DebtsViewModel(_api);

    [RelayCommand]
    private void NavigateToStatistics() => CurrentPage = new StatisticsViewModel(_api);

    [RelayCommand]
    private void NavigateToSettings() => CurrentPage = new SettingsViewModel(_api, Logout);

    [RelayCommand]
    private void TogglePane() => IsPaneOpen = !IsPaneOpen;

    [RelayCommand]
    private void Logout()
    {
        SessionManager.Instance.Logout();
        IsAuthenticated = false;
        IsPaneOpen = false;
        CurrentPage = new OnboardingViewModel(_api, OnUserAuthenticated);
    }
}
