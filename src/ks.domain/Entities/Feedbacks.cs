using System.ComponentModel.DataAnnotations.Schema;

namespace ks.domain.Entities;
[Table("feedback")]
public class Feedback : BaseEntity
{
    public double Rating { get; set; } = 0;
    public string Message { get; set; } = string.Empty;
    public virtual Order? Order { get; set; } 
    public Guid OrderId { get; set; } = Guid.Empty;

}