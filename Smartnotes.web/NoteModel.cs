// The Web app's own picture of a note, filled from the API's JSON.
// Id is new today: the UI needs it to tell the API WHICH note to delete.
public class NoteModel
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string Body { get; set; } = "";
    public bool IsPinned { get; set; }
}
