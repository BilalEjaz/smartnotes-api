// What we SEND when creating a note. Mirrors the API's CreateNoteDto.
// No Id here: the database assigns ids, the client never invents them.
public class CreateNoteRequest
{
    public string Title { get; set; } = "";
    public string Body { get; set; } = "";
    public bool IsPinned { get; set; }
}
