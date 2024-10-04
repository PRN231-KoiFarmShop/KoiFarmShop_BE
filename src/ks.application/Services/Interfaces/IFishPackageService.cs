using ks.application.Models.FishPackages;
using ks.application.Utilities;

namespace ks.application.Services.Interfaces;
public interface IFishPackageService
{
    Task<FishPackageViewModel?> CreateAsync(FishPackageCreateModel model,
        CancellationToken cancellationToken = default);
    Task<PaginatedList<FishPackageViewModel>?> GetAsync(
        int? pageSize,
        string search = "",
        int pageIndex = 0,
        CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(FishPackageUpdateModel model,
        CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid id,
        CancellationToken cancellationToken = default);
    Task<FishPackageViewModel?> GetByIdAsync(Guid id,
        CancellationToken cancellationToken = default);
}