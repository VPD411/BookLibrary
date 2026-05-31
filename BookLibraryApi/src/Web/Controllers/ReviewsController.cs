using BookLibraryApi.src.Application.Abstractions.Services;
using BookLibraryApi.src.Domain.DTOs;
using BookLibraryApi.src.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookLibraryApi.src.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReviewsController : ControllerBase
{
    private readonly IReviewsService _service;

    public ReviewsController(IReviewsService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<Review>>> GetAll(CancellationToken ct)
    {
        var result = await _service.GetAllAsync(ct);
        return Ok(result);
    }

    [HttpGet("{id::guid}")]
    public async Task<ActionResult<Review>> Get(Guid id, CancellationToken ct)
    {
        var result = await _service.GetByIdAsync(id, ct);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<Review>> Create([FromBody] CreateReviewRequest request, CancellationToken ct)
    {
        var result = await _service.CreateAsync(request, ct);
        return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
    }
}
