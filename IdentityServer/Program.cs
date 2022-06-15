using IdentityServer;
using Microsoft.IdentityModel.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddIdentityServer()
            .AddDeveloperSigningCredential()
            .AddOperationalStore(options =>
            {
                options.EnableTokenCleanup = true;
                options.TokenCleanupInterval = 30; // interval in seconds
            })
            .AddInMemoryApiResources(Config.GetApiResources())
            .AddInMemoryClients(Config.GetClients());
IdentityModelEventSource.ShowPII = true;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseIdentityServer();
app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
