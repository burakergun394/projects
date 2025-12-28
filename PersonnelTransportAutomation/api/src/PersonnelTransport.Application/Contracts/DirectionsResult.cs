using PersonnelTransport.Domain.Common;

namespace PersonnelTransport.Application.Contracts;

/// <summary>
/// A single step in a route leg.
/// </summary>
public record DirectionStep(
    string HtmlInstructions,
    double DistanceMeters,
    double DurationSeconds,
    Location StartLocation,
    Location EndLocation
);

/// <summary>
/// A leg of the route (origin to first waypoint, waypoint to waypoint, etc.).
/// </summary>
public record DirectionLeg(
    double DistanceMeters,
    double DurationSeconds,
    string StartAddress,
    string EndAddress,
    Location StartLocation,
    Location EndLocation,
    List<DirectionStep> Steps
);

/// <summary>
/// Result of a Directions API call.
/// </summary>
public record DirectionsResult(
    List<DirectionLeg> Legs,
    string OverviewPolyline, // Encoded polyline for map display
    double TotalDistanceMeters,
    double TotalDurationSeconds
);
