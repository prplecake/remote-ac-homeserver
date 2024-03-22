using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RemoteAc.Infrastructure.Context;

namespace RemoteAc.Web.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        // using SQLite
        string connectionString = configuration.GetConnectionString("Default") ??
                                  throw new InvalidOperationException("No default connection string found");
        services.AddDbContext<RemoteAcContext>(c => c.UseSqlite(connectionString));
    }
}
