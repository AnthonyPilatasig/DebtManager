using System;

namespace DebtManager.Domain.Primitives;

public abstract class Entity
{
    public Guid Id { get; protected set; }
    public DateTimeOffset LastModifiedAt { get; set; }
    public bool IsDeleted { get; set; }

    protected Entity(Guid id)
    {
        Id = id;
        LastModifiedAt = DateTimeOffset.UtcNow;
        IsDeleted = false;
    }

    protected Entity()
    {
        LastModifiedAt = DateTimeOffset.UtcNow;
        IsDeleted = false;
    }
}
