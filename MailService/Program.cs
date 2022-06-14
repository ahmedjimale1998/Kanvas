using AutoMapper;
using MailService.AsyncDataService;
using MailService.Data;
using MailService.EventProcessor;
using MailService.Interfaces;
using MailService.Profiles;
using MailService.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// RabbiMQ
builder.Services.AddSingleton<IEventProcessor, EventProcessor>();
builder.Services.AddHostedService<MessageBusSubscriber>();


var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MailProfile());
});

IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

if (builder.Environment.IsProduction())
{
    IConfiguration ProductionConfiguration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.Production.json", true, true)
        .Build();

    Console.WriteLine("--> Using kubernetes Postgress Db");
    builder.Services.AddDbContext<MailContext>(options =>
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
    builder.Services.AddDbContext<MailContext>(options =>
    {
        var connString = developmentConfiguration.GetConnectionString("MyConnectionString");
        options.UseNpgsql(connString);
    });
}

// Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "MailService", Version = "v1" });
});

builder.Services.AddTransient<IMailRepository, MailRepository>();

// Add EF services to the services container.

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "MailService");
    });

}
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;



    var context = services.GetRequiredService<MailContext>();
    context.Database.Migrate();
}
app.UseRouting();
app.UseCors("CorsPolicy");
app.UseAuthorization();
app.MapControllers();
app.Run();

// Auto Mapper Configurations
/*builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
*/