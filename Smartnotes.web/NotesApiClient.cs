using System.Net.Http.Json;   // GetFromJsonAsync / PostAsJsonAsync live here

// TYPED HTTP CLIENT: a dedicated courier for the SmartNotes API.
// DI hands this class a pre-configured HttpClient (base address, timeout),
// so components never touch URLs or JSON. They just call meaningful methods.
public class NotesApiClient
{
    private readonly HttpClient _http;

    // The HttpClient arrives via constructor injection, exactly like
    // INoteService arrives in NotesController. AddHttpClient<NotesApiClient>
    // in Program.cs wires this up.
    public NotesApiClient(HttpClient http)
    {
        _http = http;
    }

    // GET api/notes -> deserialize the JSON array into List<NoteModel>.
    // Relative URL: "api/notes" is glued onto BaseAddress from config.
    public async Task<List<NoteModel>> GetNotesAsync()
        => await _http.GetFromJsonAsync<List<NoteModel>>("api/notes") ?? new();

    // POST api/notes with a JSON body. On success the API replies
    // 201 Created + the saved note (with its REAL database id).
    // On failure (e.g. validation 400) we return null and let the UI decide.
    public async Task<NoteModel?> CreateNoteAsync(CreateNoteRequest request)
    {
        var response = await _http.PostAsJsonAsync("api/notes", request);

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        return await response.Content.ReadFromJsonAsync<NoteModel>();
    }

    // DELETE api/notes/{id} -> true if the API said 204 No Content.
    public async Task<bool> DeleteNoteAsync(int id)
    {
        var response = await _http.DeleteAsync($"api/notes/{id}");
        return response.IsSuccessStatusCode;
    }
}
