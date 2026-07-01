using System.ComponentModel.DataAnnotations;

public class CreateNoteDto
{ 
    [Required]
    [StringLength(100, MinimumLength = 1)]
    public string Title { get; set; } = "";
    
    [Required]
    public string Body { get; set; } = "";
    public bool IsPinned { get; set; }
    public string? Reminder { get; set; }
    
}