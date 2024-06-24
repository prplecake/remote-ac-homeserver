using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RemoteAc.Infrastructure.Context;

namespace RemoteAc.Web.Api;

public class Program
{
    private static IConfiguration Configuration { get; } = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json", false, true)
        .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json",
            true, true)
        .AddEnvironmentVariables()
        .Build();
    private static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseContentRoot(Directory.GetCurrentDirectory());
                webBuilder.UseConfiguration(Configuration);
                webBuilder.UseStartup<Startup>();
            })
            .UseSerilog();
    public static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(Configuration)
            .CreateLogger();
        IHost? host = CreateHostBuilder(args).Build();
        ILogger? logger = Log.ForContext<Program>();
        try
        {
            using var scope = host.Services.CreateScope();
            using var context = (RemoteAcContext)scope.ServiceProvider.GetRequiredService(typeof(RemoteAcContext));
            logger.Information("Attempting to apply migrations");
            context.Database.Migrate();
        }
        catch (Exception ex)
        {
            logger.Fatal(ex, "Unable to apply migrations");
        }
        try
        {
            logger.Information("Starting web host");
            host.Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Host terminated unexpectedly");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}
