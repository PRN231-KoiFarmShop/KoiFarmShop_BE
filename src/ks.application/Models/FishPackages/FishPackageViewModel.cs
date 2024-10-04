using ks.application.Models.Fish;

namespace ks.application.Models.FishPackages;
public class FishPackageViewModel
{
    public Guid Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public double TotalWeight { get; set; }
    public double TotalPrice { get; set; }
    public List<double> Size { get; set; } = [];
    public int Quantity { get; set; } = 0;
    public List<FishViewModel> Fishes { get; set; } = [];
}