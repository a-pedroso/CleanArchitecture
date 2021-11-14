namespace CleanArchitecture.Domain.Entities;

using CleanArchitecture.Domain.Common;

public class Product : BaseAuditableEntity<long>
{
    public override long Id { get; set; }
    public string Name { get; set; }
    public string Barcode { get; set; }
    public string Description { get; set; }
    public decimal Rate { get; set; }
}
