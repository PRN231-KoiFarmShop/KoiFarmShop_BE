using ks.application.Models.Feedbacks;

namespace ks.application.Services.Interfaces
{
    public interface IFeedbackService
    {
        Task<List<FeedbackViewModel>?> GetFeedbackByFishId(Guid fishId,
            CancellationToken cancellationToken = default);
    }
}
