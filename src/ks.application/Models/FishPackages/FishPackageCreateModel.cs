namespace ks.application.Models.FishPackages;
public class FishPackageCreateModel
{
    public string Description { get; set; } = string.Empty;
    public List<Guid> FishIds { get; set; } = [];
    
}