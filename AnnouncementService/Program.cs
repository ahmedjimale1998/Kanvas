using AnnouncementService;
using AnnouncementService.Data;
using AnnouncementService.Interfaces;
using AnnouncementService.Repository;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// Auto Mapper Configurations
var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new AnnouncementProfile());
});

IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


//Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "AnnouncementService", Version = "v1" });
});

if (builder.Environment.IsProduction())
{
    IConfiguration ProductionConfiguration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json", true, true)
        .Build();

    Console.WriteLine("--> Using kubernetes Postgress Db");
    builder.Services.AddDbContext<AnnouncementContext>(options =>
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
    builder.Services.AddDbContext<AnnouncementContext>(options =>
    {
        var connString = developmentConfiguration.GetConnectionString("MyConnectionString");
        options.UseNpgsql(connString);
    });
}

builder.Services.AddTransient<IAnnouncementRepository, AnnouncementRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "AnnouncementService");
    });
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;



    var context = services.GetRequiredService<AnnouncementContext>();
    context.Database.Migrate();
}


app.UseRouting();
app.UseCors("CorsPolicy");
app.UseAuthorization();
app.MapControllers();
app.Run();
