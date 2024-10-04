using ks.application.Models.Feedbacks;
using ks.application.Models.Fish;
using ks.application.Models.Orders;
using ks.application.Models.Users;
using ks.application.Services.Interfaces;

namespace ks.application.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IUnitOfWork unitOfWork;
        public FeedbackService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<List<FeedbackViewModel>?> GetFeedbackByFishId(Guid fishId,
            CancellationToken cancellationToken = default)
        {
            var fish = await unitOfWork.FishRepository.FirstOrDefaultAsync(x => x.Id == fishId);
            if (fish is not null && (fish.OrderLine?.Order?.Feedbacks?.Count > 0 || fish.FishPackage?.OrderLine?.Order?.Feedbacks?.Count > 0))
            {
                var feedbacks = fish.OrderLine?.Order.Feedbacks.ToList() ?? [];
                feedbacks.AddRange(fish.FishPackage?.OrderLine?.Order?.Feedbacks.ToList() ?? []);
                var fbRes = unitOfWork.Mapper.Map<List<FeedbackViewModel>>(feedbacks);
                foreach (var fb in fbRes)
                {
                    fb.Order = unitOfWork.Mapper.Map<OrderViewModel>(fish.OrderLine?.Order);
                    fb.Fish = unitOfWork.Mapper.Map<FishViewModel>(fish);
                    fb.User = unitOfWork.Mapper.Map<UserViewModel>(fish?.OrderLine?.Order?.User);
                }
                return fbRes;
            }
            else return null;
        }
    }
}
