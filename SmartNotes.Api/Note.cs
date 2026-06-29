public class Note
{
    public int Id { get; set; }
    public string  Title { get; set; } ="";
    public string Body { get; set; } = "";
    public bool IsPinned { get; set; }
    public string? Reminder { get; set; }
    
}