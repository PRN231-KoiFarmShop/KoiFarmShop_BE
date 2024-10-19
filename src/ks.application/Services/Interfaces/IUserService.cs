using ks.application.Models.Fish;
using ks.application.Models.Users;
using ks.application.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ks.application.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserViewModel?> CreateUserAsync(UserCreateModel model,
    CancellationToken cancellationToken = default);
        Task<PaginatedList<UserViewModel>?> GetAsync(int? pageSize,
            string search = "",
            int pageIndex = 0,
            CancellationToken cancellationToken = default);
        Task<bool> RemoveAsync(Guid id,
            CancellationToken cancellationToken = default);
        Task<UserViewModel?> GetById(Guid id,
            CancellationToken cancellationToken = default);
        Task<bool> UpdateAsync(Guid id,
            UserUpdateModel model,
            CancellationToken cancellationToken = default);
    }
}
