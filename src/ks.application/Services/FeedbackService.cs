﻿using ks.application.Models.Feedbacks;
using ks.application.Models.Fish;
using ks.application.Models.Orders;
using ks.application.Models.Users;
using ks.application.Services.Interfaces;
using ks.domain.Entities;

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

        public async Task<FeedbackViewModel> CreateFeedbackAsync(FeedbackCreateModel feedbackCreateModel, CancellationToken cancellationToken = default)
        {
            var order = await unitOfWork.OrderRepository.FirstOrDefaultAsync(o => o.Id == feedbackCreateModel.OrderId, cancellationToken);
            if (order == null) throw new Exception("Order not found");

            if (order.UserId != feedbackCreateModel.UserId) throw new Exception("User is not associated with this order");

            var orderLine = await unitOfWork.OrderLineRepository.FirstOrDefaultAsync(ol => ol.OrderId == feedbackCreateModel.OrderId && ol.FishId == feedbackCreateModel.FishId, cancellationToken);
            if (orderLine == null) throw new Exception("Fish is not part of this order");

            var feedback = new Feedback
            {
                Rating = feedbackCreateModel.Rating,
                Message = feedbackCreateModel.Message,
                OrderId = feedbackCreateModel.OrderId,
                // Assuming you have navigation properties or foreign keys for User and Fish
                // UserId = feedbackCreateModel.UserId,
                // FishId = feedbackCreateModel.FishId
            };

            await unitOfWork.FeedbackRepository.CreateAsync(feedback, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return unitOfWork.Mapper.Map<FeedbackViewModel>(feedback);
        }
    }
}
