using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;
using UserService.AsyncDataService;
using UserService.Data;
using UserService.Interfaces;
using UserService.Profiles;
using UserService.Repository;
using UserService.SyncDataServices.Http;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers()
    .AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddHttpClient<IMailDataClient, HttpMailDataClient>();
IdentityModelEventSource.ShowPII = true;
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.Authority = "https://localhost:7225";
    o.Audience = "myresourceapi";
    o.RequireHttpsMetadata = false;
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("PublicSecure", policy => policy.RequireClaim("client_id", "secret_client_id"));
});

// Auto Mapper Configurations
var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new UserProfile());
});

IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

//Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "UserService", Version = "v1" });
});

if (builder.Environment.IsProduction())
{   
    IConfiguration ProductionConfiguration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.Production.json", true, true)
        .Build();

    Console.WriteLine("--> Using kubernetes Postgress Db");
    builder.Services.AddDbContext<UserContext>(options =>
    {
        var connString = ProductionConfiguration.GetConnectionString("MyConnectionString");
        options.UseNpgsql(connString);
    });
}
else
{
    IConfiguration developmentConfiguration = new ConfigurationBuilder()
       .AddJsonFile("appsettings.Development.json", true, true)
      .Build();

    Console.WriteLine("--> Using local docker Postgress Db");
    builder.Services.AddDbContext<UserContext>(options =>
    {
        var connString = developmentConfiguration.GetConnectionString("MyConnectionString");
        options.UseNpgsql(connString);
    });
}




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

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;



    var context = services.GetRequiredService<UserContext>();
    context.Database.Migrate();
}

app.UseRouting();
app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

