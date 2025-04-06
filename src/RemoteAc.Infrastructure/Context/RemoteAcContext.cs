using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RemoteAc.Core.Entities;

namespace RemoteAc.Infrastructure.Context;

public class RemoteAcContext : DbContext
{
    private readonly IConfiguration _configuration;
    public RemoteAcContext(DbContextOptions<RemoteAcContext> options, IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
    }
    public DbSet<AppState> AppState { get; set; }
    public DbSet<DhtSensorData> DhtSensorData { get; set; }
    public DbSet<SensorClient> SensorClients { get; set; }
    /// <inheritdoc />
    override protected void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(_configuration.GetConnectionString("Default"));
    }
}
