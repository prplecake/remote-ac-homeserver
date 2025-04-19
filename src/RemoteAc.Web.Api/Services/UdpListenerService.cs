using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RemoteAc.Core.Entities;
using RemoteAc.Core.Interfaces.Services;

namespace RemoteAc.Web.Api.Services;

public class UdpListenerService : BackgroundService
{
    private const int PORT = 9876;
    private static readonly ILogger Logger = Log.ForContext<UdpListenerService>();
    private readonly IHostApplicationLifetime _hostApplicationLifetime;

    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    private readonly IServiceProvider _serviceProvider;
    private UdpClient _client;
    private bool _isPolling;
    private CancellationTokenSource _source;
    private UdpState _state;
    private CancellationToken _token;

    public UdpListenerService(IHostApplicationLifetime hostApplicationLifetime, IServiceProvider serviceProvider)
    {
        _hostApplicationLifetime = hostApplicationLifetime;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _isPolling = true;
        try
        {
            _source = new CancellationTokenSource();
            _token = _source.Token;

            _client.BeginReceive(Recv, _state);
        }
        catch (TaskCanceledException ex)
        {
            Logger.Error(ex, "Application terminated.");
        }
        finally
        {
            _source?.Dispose();
        }
    }
    private void OnShutdown()
    {
        Logger.Information("Shutting down UDP listener service...");
        _isPolling = false;
        _client.Dispose();
    }
    private async Task ProcessMessage(string message)
    {
        Logger.Debug($"Processing: {message}");
        try
        {
            var udpClientMessage = JsonSerializer.Deserialize<UdpClientMessage>(message, _jsonOptions);
            Logger.Debug("Deserialized: {@udpClientMessage}", udpClientMessage);
            if (udpClientMessage is null)
            {
                Logger.Error("Deserialized message is null.");
                return;
            }

            switch (udpClientMessage.MessageType)
            {
                case "REGISTRATION":
                {
                    // Create a scope to use SensorClientService
                    using var scope = _serviceProvider.CreateScope();
                    var sensorClientService = scope.ServiceProvider.GetRequiredService<ISensorClientService>();
                    try
                    {
                        await sensorClientService.AddClient(udpClientMessage);
                    }
                    catch (ArgumentException ex)
                    {
                        Logger.Error(ex, "Sensor client already exists.");
                    }
                    break;
                }
                case "SENSOR_DATA":
                {
                    // Deserialize the message to DhtSensorData
                    var dhtSensorData = JsonSerializer.Deserialize<DhtSensorData>(udpClientMessage.Message, _jsonOptions);
                    Logger.Debug("Deserialized sensor data: {@dhtSensorData}", dhtSensorData);
                    break;
                }
                default:
                    Logger.Error("Unknown message type: {MessageType}", udpClientMessage.MessageType);
                    break;
            }
        }
        catch (JsonException ex)
        {
            Logger.Error(ex, "Failed to deserialize message.");
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to process message.");
        }
    }
    private async void Recv(IAsyncResult ar)
    {
        var remoteIp = new IPEndPoint(IPAddress.Any, 0);
        var recvBytes = _client.EndReceive(ar, ref remoteIp);
        Logger.Debug("Recv bytes from remote client: {RemoteIp}.", remoteIp);
        _client.BeginReceive(Recv, null);
        var message = Encoding.UTF8.GetString(recvBytes);
        Logger.Debug($"Received: {message}");
        await ProcessMessage(message);
    }
    public override Task StartAsync(CancellationToken cancellationToken)
    {
        Logger.Information("Starting UDP listener service...");
        var listenEndPoint = new IPEndPoint(IPAddress.Any, PORT);
        _client = new UdpClient(listenEndPoint);
        _state = new UdpState();
        _state.Client = _client;
        _state.EndPoint = listenEndPoint;
        _hostApplicationLifetime.ApplicationStopping.Register(OnShutdown);
        Logger.Information("UDP listener service started.");
        Logger.Information("Endpoint: {@Endpoint}", listenEndPoint);
        return base.StartAsync(cancellationToken);
    }

    private struct UdpState
    {
        public UdpClient Client;
        public IPEndPoint EndPoint;
    }
}
