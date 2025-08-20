using blazornew.Components;
using blazornew.Service;
using blazornew.Service.IMP;
using Microsoft.AspNetCore.Components.Authorization;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
    options.SerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.SnakeCaseLower;
});

builder.Services.AddRazorComponents()
       .AddInteractiveServerComponents();


var apiBaseUrl = new Uri("https://ecomm-intern-demo.onrender.com/api/");

builder.Services.AddHttpClient<IAuthService, AuthService>(client =>
{
    client.BaseAddress = apiBaseUrl;
});

builder.Services.AddHttpClient<ISignupService, SignupService>(client =>
{
    client.BaseAddress = apiBaseUrl;
});

builder.Services.AddScoped<AuthenticationStateProvider, JwtAuthStateProvider>();

builder.Services.AddLogging();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}



app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
   .AddInteractiveServerRenderMode();

app.Run();
