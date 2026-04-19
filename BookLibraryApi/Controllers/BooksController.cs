using BookLibraryApi.DTOs;
using BookLibraryApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookLibraryApi.Controllers;

[ApiController]
[Route("api/[controller]")]
// http://localhost/api/books
public class BooksController : ControllerBase
{
    private readonly BooksService _service;

    public BooksController(BooksService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<BookResponse>>> GetAll(CancellationToken ct)
    {
        return await _service.GetAll(ct);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BookResponse?>> GetById(Guid id, CancellationToken ct)
    {
        return await _service.GetById(id, ct);
    }

    [HttpPost]
    public async Task<ActionResult<BookResponse?>> Create([FromBody] CreateBookRequest request, CancellationToken ct)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState); // HTTP 400
        }

        var result = await _service.Create(request, ct);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<BookResponse?>> Update(Guid id, [FromBody] CreateBookRequest request, CancellationToken ct)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _service.Update(id, request, ct);

        if (result is null)
        {
            return NotFound(); // HTTP 404
        }

        return Ok(result);

    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id, CancellationToken ct)
    {
        await _service.Delete(id, ct);
        return NoContent();
    }
}