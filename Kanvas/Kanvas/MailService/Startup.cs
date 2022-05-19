using MailService.Data;
using MailService.Interfaces;
using MailService.Repository;
using Microsoft.EntityFrameworkCore;

namespace MailService
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
            services.AddControllers()
                .AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "MailService", Version = "v1" });
            });
            ;
            services.AddDbContext<MailContext>(options =>
            {
                var connString = Configuration.GetConnectionString("MyConnectionString");
                options.UseNpgsql(connString);
            });

            services.AddTransient<IMailRepository, MailRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "MailService");
            });

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

       
        }
    }
}
     /*context.Database.Migrate();*/