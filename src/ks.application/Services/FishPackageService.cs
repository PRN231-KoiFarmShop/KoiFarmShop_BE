using ks.application.Models.FishPackages;
using ks.application.Services.Interfaces;
using ks.application.Utilities;
using Microsoft.EntityFrameworkCore.Query;

namespace ks.application.Services;
public class FishPackageService : IFishPackageService
{
    private readonly IUnitOfWork unitOfWork;
    public FishPackageService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }
    public async Task<FishPackageViewModel?> CreateAsync(FishPackageCreateModel model,
        CancellationToken cancellationToken = default)
    {
        var fishPack = new FishPackage()
        {
            Description = model.Description,
            Quantity = 0,
            Size = [],
            TotalPrice = 0,
            TotalWeight = 0
        };
        foreach (var item in model.FishIds)
        {
            var fish = await unitOfWork.FishRepository.FirstOrDefaultAsync(x => x.Id == item)
                ?? throw new Exception("Fish Not found with Id: " + item);
            if (fish.FishPackageId != null && fish.FishPackageId != Guid.Empty)
            {
                throw new Exception($"Fish already has package: {item}");
            }
            else
            {
                fish.FishPackageId = fishPack.Id;
                fishPack.Quantity += 1;
                fishPack.TotalPrice += fish.Price;
                fishPack.TotalWeight += fish.Weight;
                fishPack.Size.Add(fish.Size);
                unitOfWork.FishRepository.Update(fish);
            }
        }
        await unitOfWork.FishPackageRepository.CreateAsync(fishPack, cancellationToken);
        if (await unitOfWork.SaveChangesAsync())
        {
            return unitOfWork.Mapper.Map<FishPackageViewModel>(fishPack);
        }
        return null;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var fishPackage = await unitOfWork.FishPackageRepository.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (fishPackage is not null)
        {
            if (fishPackage.Fishes?.Count > 0)
            {
                foreach (var item in fishPackage.Fishes)
                {
                    item.FishPackageId = Guid.Empty;
                }
                unitOfWork.FishRepository.UpdateRange(fishPackage.Fishes.ToList());
            }
            unitOfWork.FishPackageRepository.SoftRemove(fishPackage);
            return await unitOfWork.SaveChangesAsync();
        }
        else throw new Exception("Not found");
    }

    public async Task<PaginatedList<FishPackageViewModel>?> GetAsync(int? pageSize, string search = "", int pageIndex = 0, CancellationToken cancellationToken = default)
    {
        var fishPack = string.IsNullOrEmpty(search)
        ? await unitOfWork.FishPackageRepository.WhereAsync(x => x.IsDeleted == false, cancellationToken)
        : await unitOfWork.FishPackageRepository.WhereAsync(x => x.Description.Contains(search, StringComparison.InvariantCultureIgnoreCase)
            && x.IsDeleted == false, cancellationToken);

        return fishPack?.Count > 0 ?

            PaginatedList<FishPackageViewModel>.Create(unitOfWork.Mapper.Map<List<FishPackageViewModel>>(fishPack).AsQueryable(),
                pageIndex,
                pageSize ?? fishPack.Count)
        : null;

    }

    public async Task<FishPackageViewModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var fishPack = unitOfWork.Mapper.Map<FishPackageViewModel>(await unitOfWork.FishPackageRepository.FirstOrDefaultAsync(x => x.Id == id, cancellationToken));
        return fishPack;
    }

    public Task<bool> UpdateAsync(FishPackageUpdateModel model, CancellationToken cancellationToken = default)
    {
        // Too lazyyy!
        throw new NotImplementedException();
    }
}