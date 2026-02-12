using System;

namespace Hospital.Domain.PendingEmail.ValueObjects;

public readonly record struct PendingEmailID
{
    public Guid Value { get; }

    private PendingEmailID(Guid value) => Value = value;

    public static PendingEmailID New() => new(Guid.NewGuid());

    public static PendingEmailID From(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("ID уведомления не может быть пустым.", nameof(value));
        return new PendingEmailID(value);
    }
}