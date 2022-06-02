namespace ApiGateway
{

public class Program
{
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.AddJsonFile("appsettings.json", true, true);
                config.AddJsonFile("OcelotConfig/ocelot.json", true, true);
            })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();

            })
            .ConfigureLogging(logging => logging.AddConsole());
    }
}
/*"OcelotConfig/ocelot.json"*/

