using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Microsoft.Extensions.Hosting;

namespace RemoteAc.Web.Api.Services;

public class UdpListenerService : BackgroundService
{
    private static readonly ILogger _logger = Log.ForContext<UdpListenerService>();
    private UdpClient _client;
    private bool _isPolling;
    private CancellationTokenSource _source;
    private CancellationToken _token;
    private IHostApplicationLifetime _hostApplicationLifetime;
    private UdpState _state;
    public UdpListenerService(IHostApplicationLifetime hostApplicationLifetime)
    {
        _hostApplicationLifetime = hostApplicationLifetime;
    }

    public struct UdpState
    {
        public UdpClient c;
        public IPEndPoint ep;
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
            _logger.Error(ex, "Application terminated.");
        }
        finally
        {
            _source?.Dispose();
        }
    }
    private void Recv(IAsyncResult ar)
    {
        IPEndPoint remoteIp = new IPEndPoint(IPAddress.Any, 9876);
        byte[] received = _client.EndReceive(ar, ref remoteIp);
        _client.BeginReceive(Recv, null);
        string message = Encoding.UTF8.GetString(received);
        _logger.Information($"Received: {message}");
        ProcessMessage(message);
    }
    private void ProcessMessage(string message)
    {
        throw new NotImplementedException();
    }
    private void OnShutdown()
    {
        _logger.Information("Shutting down UDP listener service...");
        _isPolling = false;
        _client?.Dispose();
    }
    public override Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.Information("Starting UDP listener service...");
        _client = new UdpClient();
        _client.Client.Bind(new IPEndPoint(IPAddress.Any, 9876));
        _state = new UdpState();
        _state.c = _client;
        _state.ep = listenEndPoint;
        _hostApplicationLifetime.ApplicationStopping.Register(OnShutdown);
        return base.StartAsync(cancellationToken);
    }
}
