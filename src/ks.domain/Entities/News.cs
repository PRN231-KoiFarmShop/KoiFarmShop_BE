using System.ComponentModel.DataAnnotations.Schema;

namespace ks.domain.Entities;
[Table("news")]
public class News : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty; // Should Store HTML
    public List<string> ImageUrl { get; set; } = [];
}