namespace CleanArchitecture.Domain.Common;

using System;

public abstract class BaseAuditableEntity<TKey> : BaseEntity<TKey>, IBaseAuditableEntity
    where TKey : IEquatable<TKey>
{
    public string CreatedBy { get; set; }
    public DateTime Created { get; set; }
    public string LastModifiedBy { get; set; }
    public DateTime? LastModified { get; set; }
}
