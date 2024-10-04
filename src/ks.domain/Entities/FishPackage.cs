using System.ComponentModel.DataAnnotations.Schema;
using ks.domain.Entities;

[Table("fish_package")]
public class FishPackage : BaseEntity
{
    public string Description { get; set; } = string.Empty;
    public List<double> Size { get; set; } = [];
    public int Quantity { get; set; } = 0;
    public double TotalWeight { get; set; }
    public double TotalPrice { get; set; }
    public virtual ICollection<Fish> Fishes { get; set; } = [];

    public virtual OrderLine? OrderLine { get; set; } 
}