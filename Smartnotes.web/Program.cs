using Smartnotes.web.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Where does the API live? In CONFIG, never hardcoded in C#.
// Fail fast at startup if it is missing (better than a mystery crash later).
var apiBaseUrl = builder.Configuration["ApiBaseUrl"]
    ?? throw new InvalidOperationException("ApiBaseUrl is missing from configuration.");

// TYPED CLIENT registration. This does two jobs at once:
// 1. Registers NotesApiClient in DI so components can @inject it.
// 2. Hands it an HttpClient managed by IHttpClientFactory (the "bike-share
//    depot") so we never exhaust sockets by new-ing HttpClients ourselves.
builder.Services.AddHttpClient<NotesApiClient>(client =>
{
    client.BaseAddress = new Uri(apiBaseUrl);
    client.Timeout = TimeSpan.FromSeconds(10);   // never wait forever on a dead API
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
