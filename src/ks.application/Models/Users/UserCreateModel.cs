using System.Text.Json.Serialization;
using ks.domain.Enums;
using Newtonsoft.Json;

namespace ks.application.Models.Users;
public class UserCreateModel
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public RoleEnum? Role { get; set; } = RoleEnum.Customer;
    public string? PhoneNumber { get; set; } = string.Empty;
    [JsonProperty("address", NullValueHandling = NullValueHandling.Ignore)]
    public string? Address { get; set; } = string.Empty;
}