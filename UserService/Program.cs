using Microsoft.EntityFrameworkCore;
using UserService.AsyncDataService;
using UserService.Data;
using UserService.Interfaces;
using UserService.Repository;
using UserService.SyncDataServices.Http;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", true, true)
   .Build();

// Add services to the container.

builder.Services.AddControllers()
    .AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddHttpClient<IMailDataClient, HttpUserDataClient>();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "UserService", Version = "v1" });
});

builder.Services.AddDbContext<UserContext>(options =>
{
    var connString = configuration.GetConnectionString("MyConnectionString");
    options.UseNpgsql(connString);
});

builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddSingleton<IMessageBusClient, MessageBusClient>();

// Add EF services to the services container.

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "UserService");
    });
    
}
app.UseRouting();
app.UseCors("CorsPolicy");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

