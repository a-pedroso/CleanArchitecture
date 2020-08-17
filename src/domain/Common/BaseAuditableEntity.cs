using System;

namespace CleanArchitecture.Domain.Common
{
    public abstract class BaseAuditableEntity<TKey> : BaseEntity<TKey>, IBaseAuditableEntity
        where TKey : IEquatable<TKey>
    {
        public string CreatedBy { get; set; }
        public DateTime Created { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModified { get; set; }
    }
}
