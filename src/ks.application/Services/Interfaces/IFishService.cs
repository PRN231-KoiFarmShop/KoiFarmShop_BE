using ks.application.Models.Fish;
using ks.application.Utilities;
using Microsoft.EntityFrameworkCore.Storage;

namespace ks.application.Services.Interfaces;
public interface IFishService
{
    Task<FishViewModel?> CreateFishAsync(FishCreateModel model,
        CancellationToken cancellationToken = default);
    Task<PaginatedList<FishViewModel>?> GetAsync(int? pageSize,
        string search = "",
        int pageIndex = 0,
        CancellationToken cancellationToken = default);
    Task<bool> RemoveAsync(Guid id,
        CancellationToken cancellationToken = default);
    Task<FishViewModel?> GetById(Guid id,
        CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(Guid id,
        FishUpdateModel model,
        CancellationToken cancellationToken = default);
}