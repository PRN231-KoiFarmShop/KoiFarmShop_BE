using ks.application.Models.Fish;
using ks.application.Models.Users;
using ks.application.Services;
using ks.application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ks.webapi.Controllers
{
    [ApiController]

    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAsync(
        [FromQuery] int? pageSize,
        [FromQuery] string search = "",
        [FromQuery] int pageIndex = 0)
        {
            var result = await userService.GetAsync(search: search,
                pageSize: pageSize,
                pageIndex: pageIndex,
                cancellationToken: default);
            return result?.Count > 0
                ? Ok(result)
                : throw new Exception("List is empty");
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] UserCreateModel model)
        {
            var result = await userService.CreateUserAsync(model, default);
            return result is not null
                ? Ok(result)
                : throw new Exception("Created Failed");
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] Guid id,
        [FromBody] UserUpdateModel model)
        {
            var result = await userService.UpdateAsync(id, model, default);
            return result
                ? NoContent()
                : throw new Exception("Update Failed");
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await userService.GetById(id, default);
            return result is not null
                ? Ok(result)
                : throw new Exception($"Not found Id: {id}");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Del([FromRoute] Guid id)
        {
            var result = await userService.RemoveAsync(id, default);
            return result
                ? NoContent()
                : throw new Exception("Delete Failed");
        }
    }
}
