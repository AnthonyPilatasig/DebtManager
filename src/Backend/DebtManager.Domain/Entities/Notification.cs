using System;
using DebtManager.Domain.Primitives;

namespace DebtManager.Domain.Entities;

public sealed class Notification : Entity
{
    public Guid UserId { get; private set; }
    public string Message { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public bool IsRead { get; private set; }

    // Navegación
    public User User { get; private set; }

    private Notification() { }

    public Notification(Guid id, Guid userId, string message, DateTime createdAt)
        : base(id)
    {
        UserId = userId;
        Message = message;
        CreatedAt = createdAt;
        IsRead = false;
    }

    public void MarkAsRead()
    {
        IsRead = true;
    }
}
