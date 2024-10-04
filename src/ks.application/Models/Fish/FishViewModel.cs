namespace ks.application.Models.Fish;
public class FishViewModel
{
    public Guid Id { get; set; }
    public bool? Sex { get; set; } = null;
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public List<string> ImageUrls { get; set; } = [];
    public double Weight { get; set; }
    public double Size { get; set; }
    public string Source { get; set; } = string.Empty;
    public double Price { get; set; }

}