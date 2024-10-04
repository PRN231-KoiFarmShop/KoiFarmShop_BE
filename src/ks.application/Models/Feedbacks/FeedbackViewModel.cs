using ks.application.Models.Fish;
using ks.application.Models.Orders;
using ks.application.Models.Users;

namespace ks.application.Models.Feedbacks
{
    public class FeedbackViewModel
    {
        public Guid Id { get; set; }
        public double Rating { get; set; } = 0;
        public string Message { get; set; } = string.Empty;
        public FishViewModel Fish { get; set; } = new();
        public OrderViewModel Order { get; set; } = new();
        public UserViewModel User { get; set; } = new();

    }
}
