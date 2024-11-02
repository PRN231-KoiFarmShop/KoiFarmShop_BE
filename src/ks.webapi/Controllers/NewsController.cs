using ks.application.Models.News;
using ks.application.Models.Response;
using ks.application.Services;
using ks.application.Services.Interfaces;
using ks.application.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace ks.webapi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class NewsController : ControllerBase
{
    private readonly INewsService newsService;
    public NewsController(INewsService newsService)
    {
        this.newsService = newsService;
    }
    [HttpGet]
    public async Task<IActionResult> Get(int? pageSize,
        string search = "",
        int pageIndex = 0)
    {
        var result = await newsService.GetAsync(pageSize: pageSize,
            pageIndex: pageIndex,
            search: search,
            cancellationToken: default);
        return result is not null
            ? Ok(result)
            : throw new Exception("List is null or empty");
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await newsService.GetByIdAsync(id, default);
        return result is not null
            ? Ok(result)
            : throw new Exception("Not found");
    }
    [HttpPost]
    public async Task<IActionResult>CreateNews([FromBody]CreateNewsModel model)
    {
        var result = await newsService.CreateNews(model, default);
        return result is not null
            ? Ok(result)
            : throw new Exception("Create Failed");
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteNews([FromRoute]Guid id)
    {
        var result = await newsService.RemoveNews(id, default);
        return result
            ? NoContent()
            : throw new Exception("Delete Failed");
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateNews([FromRoute]Guid id, [FromBody] NewsUpdateModel model)
    {
        var result = await newsService.UpdateNews(id, model, default);
        return result
            ? NoContent()
            : throw new Exception("Update Failed");
    }
}