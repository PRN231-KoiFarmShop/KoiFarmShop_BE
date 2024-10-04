using System.ComponentModel.DataAnnotations.Schema;

namespace ks.domain.Entities;
[Table("order_line")]
public class OrderLine : BaseEntity
{
    public Guid OrderId { get; set; }
    public virtual Order Order { get; set; } = new();
    public Guid? FishId { get; set; }
    public virtual Fish? Fish { get; set; } = new();
    public Guid? FishPackageId { get; set; }
    public virtual FishPackage? FishPackage { get; set; } 
}
