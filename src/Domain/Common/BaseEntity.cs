namespace CleanArchitecture.Domain.Common;

using System;

public abstract class BaseEntity<TKey> where TKey : IEquatable<TKey>
{
    public abstract TKey Id { get; set; }
}
