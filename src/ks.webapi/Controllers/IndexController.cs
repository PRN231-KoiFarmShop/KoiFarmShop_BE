using ks.application;
using Microsoft.AspNetCore.Mvc;

namespace ks.webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IndexController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        public IndexController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<IActionResult> SeedData()
        {
            await unitOfWork.SeedData();
            return Ok();
        }
    }
}
