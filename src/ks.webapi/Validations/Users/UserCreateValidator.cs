using FluentValidation;
using ks.application;
using ks.application.Models.Users;
using ks.domain.Enums;

namespace ks.webapi.Validations.Users;
public class UserCreateValidator : AbstractValidator<UserCreateModel>
{
    private readonly IUnitOfWork unitOfWork;
    public UserCreateValidator(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
        RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress();
        
        

    }
}