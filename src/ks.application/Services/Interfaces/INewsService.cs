using ks.application.Models.News;
using ks.application.Utilities;

namespace ks.application.Services.Interfaces;
public interface INewsService 
{
    Task<PaginatedList<NewsViewModel>?> GetAsync(int? pageSize, string search = "",
        int pageIndex = 0,
        CancellationToken cancellationToken = default);
    Task<NewsViewModel?> GetByIdAsync(Guid id,
        CancellationToken cancellationToken = default);
    Task<NewsViewModel> CreateNews(CreateNewsModel model, CancellationToken cancellationToken = default);
    Task<bool> RemoveNews(Guid id, CancellationToken cancellationToken=default);
    Task<bool> UpdateNews(Guid id, NewsUpdateModel model, CancellationToken cancellationToken = default);
}