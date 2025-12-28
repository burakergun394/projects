using PersonnelTransport.Domain.Common;

namespace PersonnelTransport.Application.Contracts;

/// <summary>
/// Service for calculating routes and distances using external mapping APIs.
/// </summary>
public interface IRoutingService
{
    /// <summary>
    /// Gets a distance matrix between multiple origins and destinations.
    /// </summary>
    /// <param name="origins">List of origin locations.</param>
    /// <param name="destinations">List of destination locations.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Distance matrix with travel times and distances.</returns>
    Task<DistanceMatrixResult> GetDistanceMatrixAsync(
        IReadOnlyList<Location> origins,
        IReadOnlyList<Location> destinations,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets directions from origin to destination with optional waypoints.
    /// </summary>
    /// <param name="origin">Starting location.</param>
    /// <param name="destination">Ending location.</param>
    /// <param name="waypoints">Optional intermediate stops.</param>
    /// <param name="optimizeWaypoints">If true, reorder waypoints for optimal route.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Directions with step-by-step navigation and polyline.</returns>
    Task<DirectionsResult> GetDirectionsAsync(
        Location origin,
        Location destination,
        IReadOnlyList<Location>? waypoints = null,
        bool optimizeWaypoints = false,
        CancellationToken cancellationToken = default);
}
