namespace CleanArchitecture.Domain.Common;

using System;

public interface IBaseAuditableEntity
{
    public string CreatedBy { get; set; }

    public DateTime Created { get; set; }

    public string LastModifiedBy { get; set; }

    public DateTime? LastModified { get; set; }
}
