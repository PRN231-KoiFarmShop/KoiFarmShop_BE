using System.ComponentModel.DataAnnotations.Schema;

namespace ks.domain.Entities;
[Table("fish")]
public class Fish : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public List<string>? ImageUrls { get; set; } = [];
    public double Weight { get; set; }
    public double Size { get; set; }
    public bool? Sex { get; set; }
    public string Source { get; set; } = string.Empty;// Come from which farm
    public double Price { get; set; }

    #region Rela Config
    public virtual OrderLine? OrderLine { get; set; }
    public Guid? FishPackageId { get; set; }
    public virtual FishPackage? FishPackage { get; set; }
    #endregion

}