using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DebtManager.Frontend.Api;
using DebtManager.Frontend.Services;
using System;
using System.Threading.Tasks;

namespace DebtManager.Frontend.ViewModels;

public partial class OnboardingViewModel : ViewModelBase
{
    private readonly IDebtManagerApi? _api;
    private readonly Action? _onAuthenticated;

    [ObservableProperty]
    private string _email = string.Empty;

    [ObservableProperty]
    private bool _isLoading = false;

    [ObservableProperty]
    private string _errorMessage = string.Empty;

    [ObservableProperty]
    private bool _acceptedPrivacyPolicy = false;

    [ObservableProperty]
    private bool _isLoginMode = true;

    [ObservableProperty]
    private string _submitButtonText = "Iniciar Sesión";

    public OnboardingViewModel(IDebtManagerApi? api, Action? onAuthenticated)
    {
        _api = api;
        _onAuthenticated = onAuthenticated;
    }

    [RelayCommand]
    private void ToggleMode()
    {
        IsLoginMode = !IsLoginMode;
        SubmitButtonText = IsLoginMode ? "Iniciar Sesión" : "Crear Cuenta";
        ErrorMessage = string.Empty;
    }

    [RelayCommand]
    private async Task SubmitAsync()
    {
        if (_api == null || string.IsNullOrWhiteSpace(Email)) return;
        
        if (!IsLoginMode && !AcceptedPrivacyPolicy)
        {
            ErrorMessage = "Debes aceptar la política de privacidad para crear tu cuenta.";
            return;
        }

        IsLoading = true;
        ErrorMessage = string.Empty;
        try
        {
            var request = new AuthRequest(Email);
            var response = IsLoginMode 
                ? await _api.LoginAsync(request)
                : await _api.RegisterAsync(request);

            SessionManager.Instance.Login(response.UserId);
            _onAuthenticated?.Invoke();
        }
        catch (Exception)
        {
            if (IsLoginMode)
                ErrorMessage = "Usuario no encontrado. ¿Tal vez querías crear una cuenta?";
            else
                ErrorMessage = "Error al registrar el usuario. Es posible que el correo ya esté en uso.";
        }
        finally
        {
            IsLoading = false;
        }
    }
}
