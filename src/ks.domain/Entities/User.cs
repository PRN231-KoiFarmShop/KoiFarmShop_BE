using System.ComponentModel.DataAnnotations.Schema;
using ks.domain.Enums;

namespace ks.domain.Entities;
[Table("user")]
public class User : BaseEntity
{
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string HashPassword { get; set; } = string.Empty;
    public RoleEnum Role { get; set; }
    public List<string> Address { get; set; } = [];
    public double Points { get; set; } = 0;
    public byte[]? Salt { get; set; }
    #region Relationship Config
    public UserStatusEnum Status { get; set; } = UserStatusEnum.Active;
    public virtual ICollection<Order> Orders { get; set; } = [];
    #endregion
}