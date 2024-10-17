using ks.application.Models.News;
using ks.application.Services.Interfaces;
using ks.application.Utilities;
using ks.domain.Entities;

namespace ks.application.Services
{
    public class NewsService : INewsService
    {
        private readonly IUnitOfWork unitOfWork;
        public NewsService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<NewsViewModel> CreateNews(CreateNewsModel model, CancellationToken cancellationToken = default)
        {
                var mapp = unitOfWork.Mapper.Map<News>(model);
                if (mapp != null)
                {
                    var result = await unitOfWork.NewsRepository.CreateAsync(mapp);
                    if(await unitOfWork.SaveChangesAsync())
                    {
                        return unitOfWork
                            .Mapper
                            .Map<NewsViewModel>(await unitOfWork.NewsRepository.FirstOrDefaultAsync(n => n.Id == result));
                    }
                    else
                    {
                        throw new InvalidOperationException("Save Changes Fail");
                    }
                }
            return null;
        }

        public async Task<PaginatedList<NewsViewModel>?> GetAsync(int? pageSize,
            string search = "", int pageIndex = 0,
            CancellationToken cancellationToken = default)
        {
            var resultList = string.IsNullOrEmpty(search)
                ? await unitOfWork.NewsRepository.GetAllAsync(cancellationToken)
                : await unitOfWork.NewsRepository.WhereAsync(x => x.Title.Contains(search, StringComparison.InvariantCultureIgnoreCase),
                    cancellationToken);
            if (resultList?.Count() > 0)
            {
                return PaginatedList<NewsViewModel>.Create(
                    unitOfWork.Mapper.Map<List<NewsViewModel>>(resultList).AsQueryable(),
                    pageIndex,
                    pageSize ?? resultList.Count());
            }
            return null;

        }

        public async Task<NewsViewModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
            => unitOfWork.Mapper.Map<NewsViewModel>
                (await unitOfWork.NewsRepository.FirstOrDefaultAsync(x => x.Id == id, cancellationToken));

        public async Task<bool> RemoveNews(Guid id, CancellationToken cancellationToken=default)
        {
            var exist = await unitOfWork.NewsRepository.FirstOrDefaultAsync(n => n.Id == id, cancellationToken);
            if (exist != null)
            {
                unitOfWork.NewsRepository.SoftRemove(exist);
                return await unitOfWork.SaveChangesAsync();
            }
            else
            {
                throw new Exception("News Not found?");
            }
        }

        public async Task<bool> UpdateNews(Guid id, NewsUpdateModel model, CancellationToken cancellationToken = default)
        {
            var exist = await unitOfWork
                .NewsRepository
                .FirstOrDefaultAsync(n => n.Id == id, cancellationToken);
            if (exist != null)
            {
                unitOfWork.Mapper.Map(model, exist);
                unitOfWork.NewsRepository.Update(exist);
                return await unitOfWork.SaveChangesAsync();
            }
            else
            {
                throw new Exception("News not found");
            }
        }
    }
}
