namespace ks.application.Models.News;
public class NewsViewModel
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public List<string> ImageUrl { get; set; } = [];
}