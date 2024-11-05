using ks.application.Models.Fish;
using ks.application.Models.Users;
using ks.application.Services.Interfaces;
using ks.application.Utilities;
using ks.domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ks.application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;
        public UserService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<UserViewModel?> CreateUserAsync(UserCreateModel model, CancellationToken cancellationToken = default)
        {
            var user = unitOfWork.Mapper.Map<User>(model);
            if (user is not null)
            {
                var result = await unitOfWork.UserRepository.CreateAsync(user,
                    cancellationToken);
                if (await unitOfWork.SaveChangesAsync())
                {
                    return unitOfWork
                        .Mapper
                        .Map<UserViewModel>(await unitOfWork.UserRepository.FirstOrDefaultAsync(x => x.Id == result));
                }
                else
                {
                    throw new InvalidOperationException("Save Changes Fail");
                }
            }

            return null;
        }

        public async Task<PaginatedList<UserViewModel>?> GetAsync(int? pageSize, string search = "", int pageIndex = 0, CancellationToken cancellationToken = default)
        {
            var resultList = !string.IsNullOrEmpty(search)
        ? await unitOfWork.UserRepository.WhereAsync(x =>
              (x.FullName.Contains(search, StringComparison.InvariantCultureIgnoreCase) ||
               x.PhoneNumber.Contains(search, StringComparison.InvariantCultureIgnoreCase) ||
               x.Email.Contains(search, StringComparison.InvariantCultureIgnoreCase))
               && x.IsDeleted == false, cancellationToken)
        : await unitOfWork.UserRepository.WhereAsync(x => x.IsDeleted == false, cancellationToken);

            if (resultList?.Count > 0)
            {
                return PaginatedList<UserViewModel>.Create(
                    unitOfWork.Mapper.Map<List<UserViewModel>>(resultList).AsQueryable(),
                    pageIndex,
                    pageSize ?? resultList.Count);
            }
            else
            {
                return null;
            }
        }

        public async Task<UserViewModel?> GetById(Guid id, CancellationToken cancellationToken = default)
        {
            return unitOfWork.Mapper.Map<UserViewModel>(await unitOfWork
            .UserRepository
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken));
        }

        public async Task<bool> RemoveAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var user = await unitOfWork.UserRepository.FirstOrDefaultAsync(x => x.Id == id);
            if (user is not null)
            {
                unitOfWork.UserRepository.SoftRemove(user);
                return await unitOfWork.SaveChangesAsync();
            }
            else throw new Exception("not found user");
        }

        public async Task<bool> UpdateAsync(Guid id, UserUpdateModel model, CancellationToken cancellationToken = default)
        {
            var user = await unitOfWork.UserRepository.FirstOrDefaultAsync(x => x.Id == id,
            cancellationToken);
            if (user is not null)
            {
                unitOfWork.Mapper.Map(model, user);
                unitOfWork.UserRepository.Update(user);
                return await unitOfWork.SaveChangesAsync();
            }
            else throw new Exception("Not found user");
        }
    }
}
