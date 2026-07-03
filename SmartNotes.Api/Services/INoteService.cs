public interface INoteService
{
    Task<IEnumerable<Note>> GetAllNotesAsync();
    Task<Note?> GetNoteByIdAsync(int id);
    Task<Note> CreateNoteAsync(CreateNoteDto dto);
    Task<bool> DeleteNoteAsync(int id);
    Task<bool> UpdateNoteAsync(int id, CreateNoteDto dto);
}