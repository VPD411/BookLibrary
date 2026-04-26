using AutoMapper;
using BookLibraryApi.src.Application.Abstractions;
using BookLibraryApi.src.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BookLibraryApi.src.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
// http://localhost/api/books
public class BooksController : ControllerBase
{
    private readonly IBooksService _service;
    private readonly IMapper _mapper;

    public BooksController(IBooksService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<BookResponse>>> GetAll(CancellationToken ct)
    {
        var books = await _service.GetAll(ct);
        return Ok(_mapper.Map<List<BookResponse>>(books));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BookResponse?>> GetById(Guid id, CancellationToken ct)
    {
        var book = await _service.GetById(id, ct);
        return Ok(_mapper.Map<BookResponse>(book));
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
    public async Task<ActionResult<BookResponse?>> Update(Guid id, [FromBody] UpdateBookRequest request, CancellationToken ct)
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