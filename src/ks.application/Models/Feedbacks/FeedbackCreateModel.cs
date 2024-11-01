using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ks.application.Models.Feedbacks
{
    public class FeedbackCreateModel
    {
        public double Rating { get; set; }
        public string Message { get; set; } = string.Empty;
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
        public Guid FishId { get; set; }
    }
}
