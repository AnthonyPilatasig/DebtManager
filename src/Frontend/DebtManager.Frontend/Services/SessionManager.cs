using System;

namespace DebtManager.Frontend.Services;

/// <summary>
/// Mantiene el estado de autenticación en la memoria local (MVP).
/// </summary>
public class SessionManager
{
    private static SessionManager? _instance;
    public static SessionManager Instance => _instance ??= new SessionManager();

    public Guid CurrentUserId { get; private set; } = Guid.Empty;

    public bool IsAuthenticated => CurrentUserId != Guid.Empty;

    private SessionManager() { }

    public void Login(Guid userId)
    {
        CurrentUserId = userId;
    }

    public void Logout()
    {
        CurrentUserId = Guid.Empty;
    }
}
