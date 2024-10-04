using ks.application.Models.Auths.Request;
using ks.application.Models.Auths.Response;
using ks.application.Models.Users;

namespace ks.application.Services.Interfaces;
public interface IAuthService
{
    Task<LoginResponseModel?> LoginAsync(LoginRequestModel model,
        CancellationToken cancellationToken = default);
    Task<UserViewModel?> CreateUserAsync(UserCreateModel model,
        CancellationToken cancellationToken = default);

}