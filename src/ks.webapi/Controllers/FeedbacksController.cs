using ks.application.Models.Feedbacks;
using ks.application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ks.webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeedbacksController : ControllerBase
    {
        private readonly IFeedbackService feedbackService;

        public FeedbacksController(IFeedbackService feedbackService)
        {
            this.feedbackService = feedbackService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] Guid? fishId)
        {
            List<FeedbackViewModel>? result = [];
            if (fishId is not null)
            {
                result = await feedbackService.GetFeedbackByFishId(fishId.Value);
            }
            else
            {
                // ToDo, get All
            }
            return result?.Count > 0
                ? Ok(result)
                : throw new Exception("No feedback available");
        }

        [HttpPost]
        public async Task<IActionResult> CreateFeedback([FromBody] FeedbackCreateModel feedbackCreateModel)
        {
            var feedback = await feedbackService.CreateFeedbackAsync(feedbackCreateModel);
            return CreatedAtAction(nameof(Get), new { fishId = feedback.Fish.Id }, feedback);
        }
    }
}
