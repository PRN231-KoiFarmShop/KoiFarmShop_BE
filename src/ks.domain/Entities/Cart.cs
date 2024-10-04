namespace ks.domain.Entities;
public class Cart : BaseEntity
{
    public List<Fish> Fishes { get; set; } = [];
    public List<FishPackage> FishPackages { get; set; } = [];
    public Guid UserId { get; set; }
}