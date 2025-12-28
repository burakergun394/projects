using PersonnelTransport.Domain.Common;

namespace PersonnelTransport.Application.Contracts;

/// <summary>
/// Represents a single element in a distance matrix (origin to destination).
/// </summary>
public record DistanceMatrixElement(
    double DistanceMeters,
    double DurationSeconds,
    string Status // "OK", "NOT_FOUND", "ZERO_RESULTS"
);

/// <summary>
/// Result of a Distance Matrix API call.
/// </summary>
public record DistanceMatrixResult(
    List<List<DistanceMatrixElement>> Rows, // Rows[originIndex][destinationIndex]
    List<string> OriginAddresses,
    List<string> DestinationAddresses
);
