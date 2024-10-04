using System.ComponentModel.DataAnnotations.Schema;

namespace ks.domain.Entities;
[Table("payment")]
public class Payment : BaseEntity
{
    public double Amount { get; set; }
    public bool IsCompleted { get; set; }
    public string PaymentType { get; set; } = string.Empty;
    public Guid OrderId { get; set; }
    public virtual Order Order { get; set; } = new();
}