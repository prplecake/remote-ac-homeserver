using RemoteAc.Core.Entities;

namespace RemoteAc.Core.Interfaces.Repositories;

public interface IAppStateRepository
{
    Task<AppState> GetAppState();
    AppState SetAppState(AppState updatedAppState);
    void AddAppState(AppState newAppState);
}
