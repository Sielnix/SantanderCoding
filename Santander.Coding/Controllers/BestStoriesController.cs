using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Santander.Coding.BestStories;

namespace Santander.Coding.Controllers;

[ApiController]
[Route("best-stories")]
public class BestStoriesController : ControllerBase
{
    private readonly IBestStoriesFetcher _bestStoriesFetcher;

    public BestStoriesController(IBestStoriesFetcher bestStoriesFetcher)
    {
        _bestStoriesFetcher = bestStoriesFetcher;
    }

    [HttpGet("items")]
    [ProducesResponseType(typeof(BestStory[]), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBestStories([Range(1, 200)] int limit = 10, CancellationToken ct = default)
    {
        BestStory[] bestStories = await _bestStoriesFetcher.GetBestStories(limit, ct);

        return Ok(bestStories);
    }
}
