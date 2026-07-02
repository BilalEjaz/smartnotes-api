using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

[ApiController]
[Route("api/[controller]")]

public class NotesController : ControllerBase
{
    private readonly INoteService _noteService;

   // private readonly AppDbContext _db;

    public NotesController(INoteService noteService)
    {
        _noteService = noteService;
    }
    

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Note>>> GetAll()
    {
        var notes = await _noteService.GetAllNotesAsync();
        return Ok(notes);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Note>> GetById(int id)
    {
     var note = await _noteService.GetNoteByIdAsync(id);

        return note is null ? NotFound() : Ok(note);
    }

    [HttpPost]
    public async Task<ActionResult<Note>> Create(CreateNoteDto dto)
    {

       var note = await _noteService.CreateNoteAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = note.Id }, note);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _noteService.DeleteNoteAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}