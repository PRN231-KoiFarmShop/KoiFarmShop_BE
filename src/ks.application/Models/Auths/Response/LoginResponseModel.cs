using ks.application.Models.Users;

namespace ks.application.Models.Auths.Response
{
    public class LoginResponseModel
    {
        public string Token { get; set; } = string.Empty;
        public UserViewModel User { get; set; } = new();
    }
}
