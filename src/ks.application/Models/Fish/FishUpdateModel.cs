namespace ks.application.Models.Fish;
public class FishUpdateModel
{
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public double Weight { get; set; }
    public List<string> ImageUrls { get; set; } = [];
    public bool? Sex { get; set; }
    public double Size { get; set; }
    public string Source { get; set; } = string.Empty;
    public double Price { get; set; }
}