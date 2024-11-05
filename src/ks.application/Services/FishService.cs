using System.Data.Common;
using ks.application.Models.Fish;
using ks.application.Services.Interfaces;
using ks.application.Utilities;
using ks.domain.Entities;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace ks.application.Services;
public class FishService : IFishService
{
    private readonly IUnitOfWork unitOfWork;
    public FishService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }
    public async Task<FishViewModel?> CreateFishAsync(FishCreateModel model,
        CancellationToken cancellationToken = default)
    {
        var fish = unitOfWork.Mapper.Map<Fish>(model);
        if (fish is not null)
        {
            var result = await unitOfWork.FishRepository.CreateAsync(fish,
                cancellationToken);
            if (await unitOfWork.SaveChangesAsync())
            {
                return unitOfWork
                    .Mapper
                    .Map<FishViewModel>(await unitOfWork.FishRepository.FirstOrDefaultAsync(x => x.Id == result));
            }
            else
            {
                throw new InvalidOperationException("Save Changes Fail");
            }
        }

        return null;
    }

    public async Task<PaginatedList<FishViewModel>?> GetAsync(int? pageSize, string search = "", int pageIndex = 0, CancellationToken cancellationToken = default)
    {
        var resultList = !string.IsNullOrEmpty(search)
            ? await unitOfWork.FishRepository.WhereAsync(x => x.Name.Contains(search, StringComparison.InvariantCultureIgnoreCase)
            && x.IsDeleted == false, cancellationToken)
            : await unitOfWork.FishRepository.WhereAsync(x => x.IsDeleted == false, cancellationToken);
        if (resultList?.Count > 0)
        {
            return PaginatedList<FishViewModel>.Create(
                unitOfWork.Mapper.Map<List<FishViewModel>>(resultList).AsQueryable(),
                pageIndex,
                pageSize ?? resultList.Count);
        }
        else
        {
            return null;
        }
    }

    public async Task<FishViewModel?> GetById(Guid id,
        CancellationToken cancellationToken = default)
    {
        return unitOfWork.Mapper.Map<FishViewModel>(await unitOfWork
            .FishRepository
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken));
    }

    public async Task<bool> RemoveAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var fish = await unitOfWork.FishRepository.FirstOrDefaultAsync(x => x.Id == id);
        if (fish is not null)
        {
            unitOfWork.FishRepository.SoftRemove(fish);
            return await unitOfWork.SaveChangesAsync();
        }
        else throw new Exception("not found fish");
    }

    public async Task<bool> UpdateAsync(Guid id,
        FishUpdateModel model,
        CancellationToken cancellationToken = default)
    {
        var fish = await unitOfWork.FishRepository.FirstOrDefaultAsync(x => x.Id == id,
            cancellationToken);
        if (fish is not null)
        {
            unitOfWork.Mapper.Map(model, fish);
            unitOfWork.FishRepository.Update(fish);
            return await unitOfWork.SaveChangesAsync();
        }
        else throw new Exception("Not found fish");
    }
}