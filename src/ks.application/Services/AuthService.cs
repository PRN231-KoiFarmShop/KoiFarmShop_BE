using System.Diagnostics.CodeAnalysis;
using ks.application.Models.Auths.Request;
using ks.application.Models.Auths.Response;
using ks.application.Models.Users;
using ks.application.Services.Interfaces;
using ks.application.Utilities;
using ks.domain.Entities;
using ks.domain.Enums;
using Microsoft.IdentityModel.JsonWebTokens;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure.Internal;

namespace ks.application.Services;
public class AuthService : IAuthService
{
    private readonly IUnitOfWork unitOfWork;
    public AuthService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }
    public async Task<UserViewModel?> CreateUserAsync(UserCreateModel model, CancellationToken cancellationToken = default)
    {
        var user = unitOfWork.Mapper.Map<User>(model);
        if (user is not null)
        {
            var hashPass = model.Password.HashPassword();
            user.HashPassword = hashPass.Password;
            user.Salt = hashPass.Salt;
            var id = await unitOfWork.UserRepository.CreateAsync(user, cancellationToken);
            if (await unitOfWork.SaveChangesAsync())
            {
                return unitOfWork.Mapper.Map<UserViewModel>(
                    await unitOfWork.UserRepository.FirstOrDefaultAsync(x => x.Id == id));
            }
        }
        return null;
    }

    public async Task<LoginResponseModel?> LoginAsync(LoginRequestModel model, CancellationToken cancellationToken = default)
    {
        var user = await unitOfWork.UserRepository.FirstOrDefaultAsync(x => x.Email == model.Email);
        if (user is not null)
        {
            var (Password, Salt) = model.Password.HashPassword(salt: user.Salt);
            if (Password == user.HashPassword)
            {
                return new LoginResponseModel()
                {
                    Token = TokenGenerator.GenerateToken(user: user, role: user.Role.ToString() ?? string.Empty),
                    User = unitOfWork.Mapper.Map<UserViewModel>(user)
                };
            }
        }

        return null;

    }
}