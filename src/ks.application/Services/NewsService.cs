using ks.application.Models.News;
using ks.application.Services.Interfaces;
using ks.application.Utilities;

namespace ks.application.Services
{
    public class NewsService : INewsService
    {
        private readonly IUnitOfWork unitOfWork;
        public NewsService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
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
    }
}
