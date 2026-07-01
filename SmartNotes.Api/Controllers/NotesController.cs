using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

[ApiController]
[Route("api/[controller]")]

public class NotesController : ControllerBase
{
    private readonly AppDbContext _db;

    public NotesController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Note>>> GetAll()
    {
        var notes = await _db.Notes.ToListAsync();
        return Ok(notes);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Note>> GetById(int id)
    {
     var note = await _db.Notes.FindAsync(id);

        return note is null ? NotFound() : Ok(note);
    }

    [HttpPost]
    public async Task<ActionResult<Note>> Create(CreateNoteDto dto)
    {

        static string Clean(string s) => Regex.Replace(s.Trim(), @"\s+", " "); 
        var note = new Note
        {
            Title = Clean(dto.Title),
            Body = Clean(dto.Body),
            IsPinned = dto.IsPinned,
            Reminder = dto.Reminder?.Trim()
        };
        _db.Notes.Add(note);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new {id = note.Id}, note);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var note = await _db.Notes.FindAsync(id);
        if(note is null)
        {
            return NotFound();
        }
        _db.Notes.Remove(note);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}