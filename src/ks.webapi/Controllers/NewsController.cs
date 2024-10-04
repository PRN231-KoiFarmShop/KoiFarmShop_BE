using ks.application.Models.News;
using ks.application.Models.Response;
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
}