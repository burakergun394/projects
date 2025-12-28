using PersonnelTransport.Application.Contracts;
using PersonnelTransport.Domain.Common;

namespace PersonnelTransport.Application.Routing.Queries.GetDistanceMatrix;

/// <summary>
/// Query to get distance matrix between locations.
/// </summary>
public record GetDistanceMatrixQuery(
    List<LocationDto> Origins,
    List<LocationDto> Destinations
);

/// <summary>
/// DTO for location input.
/// </summary>
public record LocationDto(double Latitude, double Longitude)
{
    public Location ToDomain() => new(Latitude, Longitude);
}
