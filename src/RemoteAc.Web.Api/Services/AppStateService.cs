using RemoteAc.Core.Entities;
using RemoteAc.Core.Interfaces.Repositories;
using RemoteAc.Core.Interfaces.Services;

namespace RemoteAc.Web.Api.Services;

public class AppStateService : IAppStateService
{
    private readonly ILogger _logger = Log.ForContext<AppStateService>();
    private readonly IAppStateRepository _repo;
    public AppStateService(IAppStateRepository appStateRepository)
    {
        _repo = appStateRepository;
    }
    /// <inheritdoc/>
    public AppState GetAppState()
    {
        AppState? result;
        result = _repo.GetAppState().Result;
        if (result is not null) return result;
        // AppState doesn't exist in database, let's create it here
        CreateInitialAppState();
        result = _repo.GetAppState().Result;
        return result;
    }
    /// <inheritdoc/>
    public bool? AcUnitOn
    {
        get => GetAppState().AcUnitOn;
        set
        {
            var state = GetAppState();
            state.AcUnitOn = value;
            SetAppState(state);
        }
    }
    /// <inheritdoc />
    public string? WeatherStation
    {
        get => GetAppState().WeatherStation;
        set
        {
            var state = GetAppState();
            state.WeatherStation = value;
            SetAppState(state);
        }
    }
    /// <inheritdoc />
    public string? WxGridPoints
    {
        get => GetAppState().WxGridPoints;
        set
        {
            var state = GetAppState();
            state.WxGridPoints = value;
            SetAppState(state);
        }
    }
    private void CreateInitialAppState()
    {
        _logger.Information("Creating initial app state");
        AddAppState(new AppState
        { AcUnitOn = false });
    }
    private void AddAppState(AppState state) => _repo.AddAppState(state);
    private void SetAppState(AppState state) => _repo.SetAppState(state);
}
