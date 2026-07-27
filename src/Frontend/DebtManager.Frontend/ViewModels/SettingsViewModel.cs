using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DebtManager.Frontend.Api;
using DebtManager.Frontend.Services;
using System;
using System.Threading.Tasks;

namespace DebtManager.Frontend.ViewModels;

public partial class SettingsViewModel : ViewModelBase
{
    private readonly IDebtManagerApi? _api;
    private readonly Action? _onLogout;

    [ObservableProperty]
    private bool _isLoading = false;

    public SettingsViewModel(IDebtManagerApi? api, Action? onLogout)
    {
        _api = api;
        _onLogout = onLogout;
    }

    [RelayCommand]
    private async Task DeleteAccountAsync()
    {
        if (_api == null) return;
        
        IsLoading = true;
        try
        {
            var userId = SessionManager.Instance.CurrentUserId;
            await _api.DeleteUserAsync(userId);
            
            // Destruir sesión y volver a Onboarding
            SessionManager.Instance.Logout();
            _onLogout?.Invoke();
        }
        catch (Exception)
        {
            // Ignorar para MVP en caso de error de red
        }
        finally
        {
            IsLoading = false;
        }
    }
}
