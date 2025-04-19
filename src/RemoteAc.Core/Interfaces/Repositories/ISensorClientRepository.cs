using RemoteAc.Core.Entities;

namespace RemoteAc.Core.Interfaces.Repositories;

/// <summary>
///     Interface for managing SensorClient entities in the repository.
/// </summary>
public interface ISensorClientRepository
{
    /// <summary>
    ///     Adds a new SensorClient entity to the repository.
    /// </summary>
    /// <param name="client">The SensorClient entity to add.</param>
    void AddClient(SensorClient client);

    /// <summary>
    ///     Retrieves a SensorClient entity by its hostname.
    /// </summary>
    /// <param name="hostname">The hostname of the SensorClient to retrieve.</param>
    /// <returns>
    ///     A task that represents the asynchronous operation. The task result contains the SensorClient entity if found;
    ///     otherwise, null.
    /// </returns>
    Task<SensorClient?> GetClient(string hostname);

    /// <summary>
    ///     Retrieves a SensorClient entity by its ID.
    /// </summary>
    /// <param name="id">The ID of the SensorClient to retrieve.</param>
    /// <returns>
    ///     A task that represents the asynchronous operation. The task result contains the SensorClient entity if found;
    ///     otherwise, null.
    /// </returns>
    Task<SensorClient?> GetClient(int id);
    /// <summary>
    ///     Retrieves a list of all SensorClient entities.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains a list of SensorClient entities.</returns>
    Task<List<SensorClient>> GetClients();

    /// <summary>
    ///     Removes an existing SensorClient entity from the repository.
    /// </summary>
    /// <param name="client">The SensorClient entity to remove.</param>
    void RemoveClient(SensorClient client);
}
