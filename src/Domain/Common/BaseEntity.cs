using System;

namespace CleanArchitecture.Domain.Common
{
    public abstract class BaseEntity<TKey> where TKey : IEquatable<TKey>
    {
        public abstract TKey Id { get; set; }
    }
}
