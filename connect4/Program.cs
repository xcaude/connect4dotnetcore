// filepath: /c:/Users/thiba/connect4dotnetcore/connect4/Program.cs
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents(); // Ensure this line is correct
builder.Services.AddRazorPages(); // Add this line to register Razor Pages
builder.Services.AddServerSideBlazor(); // Add this line to register Blazor Server

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting(); // Ensure routing is enabled

app.UseEndpoints(endpoints =>
{
    endpoints.MapBlazorHub(); // Add this line to map Blazor Hub
    endpoints.MapFallbackToPage("/_Host"); // Ensure this line points to the correct fallback page
});

app.Run();