using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]

public class NotesController : ControllerBase
{
    private static readonly List<Note> _notes = new()
    {
        new Note { Id =1, Title = "Groceries" , Body = "Milk and eggs"},
        new Note { Id =2, Title = "workout", Body ="Run 5Km"}
    };

    [HttpGet]
    public ActionResult<IEnumerable<Note>> GetAll()
    {
        return Ok(_notes);
    }

    [HttpGet("{id}")]
    public ActionResult<Note> GetById(int id)
    {
     var note = _notes.FirstOrDefault(n => n.Id == id);
     if(note is null)
        {
            return NotFound();
        }  
        return Ok(note); 
    }

    [HttpPost]
    public ActionResult<Note> Create(Note note)
    {
        note.Id = _notes.Count == 0 ? 1 : _notes.Max(n => n.Id) +1 ;
        _notes.Add(note);
        return CreatedAtAction(nameof(GetById), new {id = note.Id}, note);
    }
    
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var note = _notes.FirstOrDefault(n => n.Id == id);
        if(note is null)
        {
            return NotFound();
        }
        _notes.Remove(note);
        return NoContent();
    }
}