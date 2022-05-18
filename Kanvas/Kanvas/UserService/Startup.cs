using Microsoft.EntityFrameworkCore;
using UserService.Data;
using UserService.Interfaces;
using UserService.Repository;

namespace UserService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            /*services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    policy =>
                    {
                        policy.AllowAnyHeader().AllowAnyOrigin();
                    });
            });*/
            services.AddControllers()
                .AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "UserService", Version = "v1" });
            });
            ;
            services.AddDbContext<UserContext>(options =>
            {
                var connString = Configuration.GetConnectionString("MyConnectionString");
                options.UseNpgsql(connString);

            });

            services.AddTransient<IUserRepository, UserRepository>();

            /*services.AddTransient<TweetApplication>();

            services.AddTransient<IEventSender, EventSender>();
            services.AddMassTransit(x =>
            {
                x.AddConsumer<UserFollowedConsumer>();
                x.AddConsumer<UserUnfollowedConsumer>();
                x.AddConsumer<ProfileCreatedConsumer>();

                x.UsingRabbitMq((cfx, cnf) =>
                {
                    cnf.Host(Environment.GetEnvironmentVariable("RabbitMQConnectionString"));

                    cnf.ConfigureEndpoints(cfx);

                });

            });*/
            /*  services.Configure<MassTransitHostOptions>(options =>
              {
                  options.WaitUntilStarted = true;
                  options.StartTimeout = TimeSpan.FromSeconds(30);
                  options.StopTimeout = TimeSpan.FromMinutes(1);
              });*/
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("CorsPolicy");

            app.UseRouting();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "UserService");
            });

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            context.Database.Migrate();
        }
    }
}