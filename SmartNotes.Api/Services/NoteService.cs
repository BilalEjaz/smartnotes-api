using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

public class NoteService : INoteService
{
    private readonly AppDbContext _db;

    public NoteService(AppDbContext db)
    {
        _db = db;   // chef gets storeroom access injected
    }

    public async Task<IEnumerable<Note>> GetAllNotesAsync() => 
    await _db.Notes.ToListAsync();
    

    public async Task<Note?> GetNoteByIdAsync(int id) => 
     await _db.Notes.FindAsync(id);
    

    public async Task<Note> CreateNoteAsync(CreateNoteDto dto)
    {
        var note = new Note
        {
            Title = Clean(dto.Title),
            Body = Clean(dto.Body),
            IsPinned = dto.IsPinned,
            Reminder = dto.Reminder?.Trim()
        };
        _db.Notes.Add(note);
        await _db.SaveChangesAsync();
        return note;
    }

    public async Task<bool> DeleteNoteAsync(int id)
    {
        var note = await _db.Notes.FindAsync(id);
        if (note is null)
        {
            return false;
        }
        _db.Notes.Remove(note);
        await _db.SaveChangesAsync();
        return true;
    }
    private static string Clean(string s) => Regex.Replace(s.Trim(), @"\s+", " ");
}