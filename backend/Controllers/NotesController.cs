using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class NotesController : ControllerBase
{
    private readonly NoteRepository _noteRepository;

    public NotesController(NoteRepository noteRepository)
    {
        _noteRepository = noteRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var userId = GetUserId();
        var notes = await _noteRepository.GetAllAsync(userId);
        return Ok(notes);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var userId = GetUserId();
        var note = await _noteRepository.GetByIdAsync(id, userId);

        if (note is null)
            return NotFound("Note not found.");

        return Ok(note);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateNote dto)
    {
        var userId = GetUserId();
        var note = await _noteRepository.CreateAsync(userId, dto);
        return CreatedAtAction(nameof(GetById), new { id = note.Id }, note);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateNote dto)
    {
        var userId = GetUserId();
        var note = await _noteRepository.UpdateAsync(id, userId, dto);

        if (note is null)
            return NotFound("Note not found.");

        return Ok(note);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var userId = GetUserId();
        var deleted = await _noteRepository.DeleteAsync(id, userId);

        if (!deleted)
            return NotFound("Note not found.");

        return NoContent();
    }

    private int GetUserId()
    {
        var userIdClaim = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value
            ?? User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return int.Parse(userIdClaim!);
    }
}
